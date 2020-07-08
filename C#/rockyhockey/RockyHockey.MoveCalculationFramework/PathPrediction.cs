using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    public class PathPrediction
    {
        ReaderWriterLock pathPartsLocker;
        ReaderWriterLock currentPositionLocker;

        public PositionCollector collector { get; private set; }
        TimedCoordinate current;
        List<StraightLine> pathParts;
        int pathPartPointer;
        bool validationFlag = true;

        IProgress<List<Vector>> progress;

        public event Action OnInit;

        double batVelocity = 0;

        public PathPrediction(ImageProvider provider = null, IProgress<List<Vector>> progress = null)
        {
            collector = new ImagePositionCollector(provider);
            pathPartsLocker = new ReaderWriterLock();
            currentPositionLocker = new ReaderWriterLock();
            this.progress = progress;
        }

        public PathPrediction(PositionCollector provider, IProgress<List<Vector>> progress = null)
        {
            collector = provider;
            pathPartsLocker = new ReaderWriterLock();
            currentPositionLocker = new ReaderWriterLock();
            this.progress = progress;
        }

        /// <summary>
        /// initializes prediction
        /// </summary>
        public void init()
        {
            List<StraightLine> localpathParts = new List<StraightLine>();
            List<TimedCoordinate> positions = collector.GetPuckPositions();

            if (positions.Any())
            {
                parallelCompare(positions, ref localpathParts);
                localpathParts = predict(localpathParts);

                pathPartsLocker.AcquireWriterLock(int.MaxValue);
                pathParts = localpathParts;
                pathPartPointer = 0;
                pathPartsLocker.ReleaseWriterLock();

                validate();

                OnInit?.Invoke();
            }
        }

        public bool towardsRobot()
        {
            bool retval = false;

            if (pathParts?.Count > 0)
                retval = pathParts.First().Direction.X < 0;

            return retval;
        }

        private void fillPositionListToMinLength(ref List<TimedCoordinate> positions)
        {
            positions = positions.Where(x => x != null).ToList();
            positions = positions.Where(x => x.X != double.NaN && x.Y != double.NaN).ToList();

            int tryCounter = 0;
            while (positions.Count() < 3 && tryCounter++ < 50)
                positions.Add(collector.GetPuckPosition());
        }

        public void finalize()
        {
            validationFlag = false;
        }

        /// <summary>
        /// validates calculated path over time
        /// </summary>
        private void validate()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    validationFlag = true;
                    while (validationFlag)
                    {
                        //get current position
                        TimedCoordinate localCurrent = collector.GetPuckPosition();

                        if (localCurrent == null)
                            continue;

                        currentPositionLocker.AcquireWriterLock(int.MaxValue);
                        current = localCurrent;
                        currentPositionLocker.ReleaseWriterLock();

                        //check if it is on last ckecked line
                        pathPartsLocker.AcquireReaderLock(int.MaxValue);

                        if (pathPartPointer >= pathParts.Count())
                            break;

                        if (!pathParts[pathPartPointer].isOnLine(current, Config.Instance.Tolerance))
                        {
                            pathPartPointer++;
                            if (pathPartPointer >= pathParts.Count())
                                break;

                            //check if it is on next line
                            if (!pathParts[pathPartPointer].isOnLine(current, Config.Instance.Tolerance))
                            {
                                //update path prediction
                                break;
                            }
                        }
                        pathPartsLocker.ReleaseReaderLock();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                pathPartsLocker.ReleaseLock();
                currentPositionLocker.ReleaseLock();

                init();
            });
        }

        /// <summary>
        /// calculates position on the motion prediction at given x and time when the bat reaches the calculated position
        /// </summary>
        public TimedVector getTimeForPosition(double x)
        {
            TimedCoordinate retval = null;
            double length = 0;

            //make local copy so that validation of path does not interfere with calculation
            pathPartsLocker.AcquireReaderLock(int.MaxValue);
            List<StraightLine> localpathParts = new List<StraightLine>(pathParts);
            pathPartsLocker.ReleaseReaderLock();

            //find line on which the requested y position is located
            int a;
            for (a = localpathParts.Count - 1; a > -1; a--)
            {
                retval = new TimedCoordinate(localpathParts[a].reachesX(x), DateTimeOffset.Now.ToUnixTimeMilliseconds());

                if (retval.insideBounds())
                    break;
            }

            //throw error if requested y position is not located on any line in the path
            if (a == -1)
                throw new TimingException();

            //make local copy so that validation of path does not interfere with calculation
            pathPartsLocker.AcquireReaderLock(int.MaxValue);
            TimedCoordinate localCurrent = new TimedCoordinate(current);
            int localPathPartPointer = pathPartPointer;
            pathPartsLocker.ReleaseReaderLock();

            //check if bat has passes line with requested y
            if (a < localPathPartPointer)
                throw new TimingException();
            else if (a == localPathPartPointer)
            {
                //creates vector between requested point and current puck position
                Vector motionVector = new Vector(retval, localCurrent);

                //checks if bat has passed requested position
                if (motionVector.towardsRobot())
                    throw new TimingException();

                //get distance to bat
                length = motionVector.Length;
            }
            else
            {
                //claculate distance to bat
                Coordinate next = retval;
                while (a > localPathPartPointer)
                {
                    Coordinate last = localpathParts[a--].previousImpact();

                    length += new Vector(last, next).Length;

                    next = last;
                }
                length += new Vector(next, localCurrent).Length;
            }

            //calculate when bat will be at requested position
            retval.Timestamp += (long)(length / batVelocity);

            return new TimedVector(new TimedCoordinate(localpathParts[a].previousImpact()), retval);
        }

        /// <summary>
        /// checks if given coordinates are in the same line
        /// </summary>
        /// <param name="detectedPosition">detected positions</param>
        /// <returns>puck motion parts</returns>
        private void parallelCompare(List<TimedCoordinate> detectedPosition, ref List<StraightLine> motionLines)
        {
            int oldLength = detectedPosition.Count();
            if ((detectedPosition = detectedPosition.Where(x => x.X != double.NaN && x.Y != double.NaN).ToList()).Count() != oldLength)
                motionLines = new List<StraightLine>();

            fillPositionListToMinLength(ref detectedPosition);

            //start check if first and last point are on the same line
            Task<CompareData> simpleComparisionTask = comparePositions(detectedPosition[0], detectedPosition[1], detectedPosition.Last());

            List<Task<CompareData>> comparisons = new List<Task<CompareData>>();

            //start evaluation of other points
            for (int a = 1; a < detectedPosition.Count;)
                comparisons.Add(comparePositions(detectedPosition[a - 1], detectedPosition[a++], detectedPosition[a < detectedPosition.Count ? a : detectedPosition.Count - 1]));

            //evaluate results
            parallelAnalysis(comparisons, simpleComparisionTask, ref motionLines);
        }

        /// <summary>
        /// evaluates a set of ComparisonData structs
        /// </summary>
        /// <param name="comparisons">comparison data or tasks</param>
        /// <param name="simpleCheck">comparison data that evaluates first and last position in list</param>
        /// <returns></returns>
        private void parallelAnalysis(List<Task<CompareData>> comparisons, Task<CompareData> simpleCheck, ref List<StraightLine> motionLines)
        {
            simpleCheck.Wait();
            CompareData simpleCheckResult = simpleCheck.Result;

            //check if puck was played over bank or if position are on the same line
            if (simpleCheckResult != null && simpleCheckResult.posOnVecLine)
            {
                VelocityVector velocity = new VelocityVector(simpleCheckResult.vec.TimedStart, simpleCheckResult.pos);
                batVelocity = velocity.Velocity;
                motionLines.Add(new StraightLine(velocity));
            }
            else
            {
                int length = comparisons.Count;
                int impactIndex = length;
                List<CompareData> comparisonData = new List<CompareData>();

                //evaluates the comparison data
                for (int a = 0; a < length; a++)
                {
                    //extract data structs into new Lists
                    Task<CompareData> currentComparisonTask = (Task<CompareData>)(object)comparisons[a];
                    currentComparisonTask.Wait();

                    CompareData currentComparisonResult = currentComparisonTask.Result;

                    if (currentComparisonResult != null)
                    {
                        comparisonData.Add(currentComparisonResult);

                        //checks if an impact on
                        if (!currentComparisonResult.posOnVecLine && impactIndex == length)
                            impactIndex = a;
                    }
                }

                //create motion line from correct vectors
                motionLines.Add(new StraightLine(comparisonData.First().vec.Start, directionMedian(comparisonData.GetRange(0, impactIndex == 0 ? 1: impactIndex))));

                if (impactIndex != length)
                {
                    List<TimedCoordinate> positions = new List<TimedCoordinate>();

                    //calculate impact position on bank
                    positions.Add(getImpactCoordinate(motionLines.Last(), comparisonData[impactIndex].vec));

                    //extract positions after impact on bank
                    for (int a = impactIndex + 1; a < comparisonData.Count; a++)
                        positions.Add(comparisonData[a].vec.TimedEnd);

                    //reevaluate positions
                    parallelCompare(positions, ref motionLines);
                }
            }
        }

        /// <summary>
        /// checks if given coordinates are on the same line
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private Task<CompareData> comparePositions(TimedCoordinate start, TimedCoordinate end, TimedCoordinate position)
        {
            return Task<CompareData>.Factory.StartNew(() =>
            {
                CompareData data = null;

                if (start != null && end != null && position != null)
                {
                    //create struct that holds comparison data
                    data = new CompareData();

                    //create a vector from the first two positions
                    data.vec = new TimedVector(start, end);

                    //create a line out of the vector
                    data.vecLine = new StraightLine(data.vec);

                    //prevents test from failing if the same position is given for start and end
                    if (data.vecLine.Direction != new Coordinate())
                    {
                        //check if third position is on line
                        data.posOnVecLine = data.vecLine.isOnLine(position, Config.Instance.Tolerance);

                        //makes sure an impact on the bank is not ignored due to the allowed jitter
                        data.posOnVecLine = data.posOnVecLine && data.vecLine.reachesX(position.X).insideBounds();
                    }
                    else
                        data.posOnVecLine = true;

                    //store third position in comparison struct
                    data.pos = position;
                }

                return data;
            });
        }


        private Coordinate directionMedian(List<CompareData> motionVectors)
        {
            Coordinate median = new Coordinate();

            foreach (CompareData comp in motionVectors)
            {
                median.X += comp.vec.VectorDirection.X;
                median.Y += comp.vec.VectorDirection.Y;
            }

            median.X /= motionVectors.Count;
            median.Y /= motionVectors.Count;

            return median;
        }

        /// <summary>
        /// calculates the position where the puck hit the bank
        /// </summary>
        /// <param name="movementLine">the motion line created from the previous positions</param>
        /// <param name="incorrectVector">vector that were created from the positions before and after the puck hit the banlk</param>
        /// <returns></returns>
        private TimedCoordinate getImpactCoordinate(StraightLine movementLine, TimedVector incorrectVector)
        {
            //get impact on border
            TimedCoordinate impact = new TimedCoordinate(movementLine.nextImpact());

            //create vector between last valid position and border
            Vector impactVector = new Vector(incorrectVector.Start, impact);

            //calculate when the impact happened
            impact.Timestamp = (long)(impactVector.Length * (incorrectVector.NeededTime / incorrectVector.Length)) + incorrectVector.TimedStart.Timestamp;

            return impact;
        }

        private List<StraightLine> predict(List<StraightLine> motionStart)
        {
            pathPartPointer = motionStart.Count - 1;

            StraightLine current = motionStart.Last();

            if (current.Direction != new Coordinate())
                while (current.nextImpactOnLongSide().insideBounds())
                    motionStart.Add(current = current.onLongSideReflected());

            return motionStart;
        }

        public void reportProgress()
        {
            if (progress != null)
                reportProgress(progress);
        }

        public void reportProgress(IProgress<List<Vector>> progress)
        {
            Task.Factory.StartNew(() =>
            {
                //make local copy so that validation of path does not interfere with calculation
                pathPartsLocker.AcquireReaderLock(int.MaxValue);
                List<StraightLine> localpathParts = new List<StraightLine>(pathParts ?? new List<StraightLine>());
                pathPartsLocker.ReleaseReaderLock();

                if (localpathParts.Any())
                {
                    List<Vector> motionVectors = new List<Vector>();
                    foreach (StraightLine pathPart in localpathParts)
                        motionVectors.Add(new Vector(pathPart.previousImpact(), pathPart.nextImpact()));

                    progress.Report(motionVectors);
                }
            });
        }
    }

    public class TimingException : Exception
    {
        public TimingException() : base("puck is is past point") { }
    }

    class CompareData
    {
        public TimedVector vec;
        public StraightLine vecLine;
        public TimedCoordinate pos;
        public bool posOnVecLine;
    }
}
