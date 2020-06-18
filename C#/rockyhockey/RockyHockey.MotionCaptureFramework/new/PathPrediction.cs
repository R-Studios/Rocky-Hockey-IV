using Emgu.CV;
using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public class PathPrediction
    {
        ReaderWriterLock rwl;

        private PositionCollector collector;
        TimedCoordinate current;
        List<StraightLine> pathParts;
        int pathPartPointer;
        bool validationFlag = true;

        double batVelocity = 0;

        public PathPrediction(ImageProvider provider = null)
        {
            collector = new ImagePositionCollector(provider);
            rwl = new ReaderWriterLock();
        }

        public PathPrediction(PositionCollector provider)
        {
            collector = provider;
            rwl = new ReaderWriterLock();
        }

        /// <summary>
        /// initializes prediction
        /// </summary>
        public void init()
        {
            List<StraightLine> localpathParts = new List<StraightLine>();
            parallelCompare(collector.GetPuckPositions(), ref localpathParts);
            localpathParts = predict(localpathParts);

            rwl.AcquireWriterLock(int.MaxValue);
            pathParts = localpathParts;
            rwl.ReleaseWriterLock();

            validate();
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
                while (validationFlag)
                {
                    //get current position
                    TimedCoordinate localCurrent = collector.GetPuckPosition();

                    rwl.AcquireWriterLock(int.MaxValue);
                    current = localCurrent;
                    rwl.ReleaseWriterLock();

                    //check if it is on last ckecked line
                    rwl.AcquireReaderLock(int.MaxValue);
                    if (!pathParts[pathPartPointer].isOnLine(current))
                    {
                        //check if it is on next line
                        if (!pathParts[++pathPartPointer].isOnLine(current))
                        {
                            //update path prediction
                            rwl.ReleaseReaderLock();
                            init();
                            break;
                        }
                    }
                    rwl.ReleaseReaderLock();
                }

                current = null;
                pathParts = null;
                collector.StopMotionCapturing();
                collector = null;
                rwl = null;
            });
        }

        /// <summary>
        /// guarantees minimum number of detected positions to make prediction more accurate
        /// </summary>
        /// <param name="detectedPosition"></param>
        private void fillListToMinLength(ref List<TimedCoordinate> detectedPosition)
        {
            while (detectedPosition.Count < 5)
                detectedPosition.Add(collector.GetPuckPosition());
        }

        /// <summary>
        /// calculates position on the motion prediction at given x and time when the bat reaches the calculated position
        /// </summary>
        public TimedCoordinate getTimeForPosition(double x)
        {
            TimedCoordinate retval = null;
            double length = 0;

            //make local copy so that validation of path does not interfere with calculation
            rwl.AcquireReaderLock(int.MaxValue);
            List<StraightLine> localpathParts = new List<StraightLine>(pathParts);
            rwl.ReleaseReaderLock();

            //find line on which the requested y position is located
            int a;
            for (a = localpathParts.Count - 1; a > -1; a--)
            {
                retval = new TimedCoordinate(localpathParts[a].reachesX(x));

                if (Coordinate.insideBounds(retval))
                    break;
            }

            //throw error if requested y position is not located on any line in the path
            if (a == -1)
                throw new TimingException();

            //make local copy so that validation of path does not interfere with calculation
            rwl.AcquireReaderLock(int.MaxValue);
            TimedCoordinate localCurrent = new TimedCoordinate(current);
            int localPathPartPointer = pathPartPointer;
            rwl.ReleaseReaderLock();

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
            retval.Timestamp = localCurrent.Timestamp + (long)(length / batVelocity);

            return retval;
        }

        /// <summary>
        /// checks if given coordinates are in the same line
        /// </summary>
        /// <param name="detectedPosition">detected positions</param>
        /// <returns>puck motion parts</returns>
        private void parallelCompare(List<TimedCoordinate> detectedPosition, ref List<StraightLine> motionLines)
        {
            //make sure the minimum amount of positions are in the list
            fillListToMinLength(ref detectedPosition);

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
            if (simpleCheckResult.posOnVecLine)
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

                    comparisonData.Add(currentComparisonResult);

                    //checks if an impact on
                    if (!currentComparisonResult.posOnVecLine && impactIndex == length)
                        impactIndex = a;
                }

                //create motion line from correct vectors
                motionLines.Add(new StraightLine(comparisonData.First().vec.Start, directionMedian(comparisonData.GetRange(0, impactIndex))));

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
                //create struct that holds comparison data
                CompareData data = new CompareData();

                //create a vector from the first two positions
                data.vec = new TimedVector(start, end);

                //create a line out of the vector
                data.vecLine = new StraightLine(data.vec);

                //check if third position is on line
                data.posOnVecLine = data.vecLine.isOnLine(position, 5);

                //makes sure an impact on the bank is not ignored due to the allowed jitter
                data.posOnVecLine = data.posOnVecLine && data.vecLine.reachesX(position.X).insideBounds();

                //store third position in comparison struct
                data.pos = position;

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

        private TimedCoordinate getImpactCoordinate(StraightLine movementLine, TimedVector incorrectVector)
        {
            //get impact on border
            TimedCoordinate impact = new TimedCoordinate(movementLine.nextImpactOnLongSide());

            if (!Coordinate.insideBounds(impact))
                impact = new TimedCoordinate(movementLine.nextImpactOnShortSide());

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

            while (Coordinate.insideBounds(current.nextImpactOnLongSide()))
                motionStart.Add(current = current.onLongSideReflected());

            return motionStart;
        }
    }

    public class TimingException : Exception
    {
        public TimingException() : base("bat is past point") { }
    }

    class CompareData
    {
        public TimedVector vec;
        public StraightLine vecLine;
        public TimedCoordinate pos;
        public bool posOnVecLine;
    }

    public class StraightLine
    {
        public Coordinate Direction { get; private set; }
        private double gradient = 0;
        private double yIntercept;
        private bool increasingX;
        private bool increasingY;

        /// <summary>
        /// initializes Line from a Vector
        /// </summary>
        /// <param name="vec"></param>
        public StraightLine(Vector vec)
        {
            init(vec.Start, vec.VectorDirection);
        }

        /// <summary>
        /// initializes Line from a direction and a yIntercept
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="yIntercept"></param>
        public StraightLine(Coordinate direction, double yIntercept)
        {
            init(new Coordinate(), direction);
            this.yIntercept = yIntercept;
        }

        /// <summary>
        /// initializes Line from a position and a direction
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        public StraightLine(Coordinate position, Coordinate direction)
        {
            init(position, direction);
        }

        private void init(Coordinate position, Coordinate direction)
        {
            this.Direction = direction;

            if (direction.Y != 0)
                gradient = direction.Y / direction.X;

            increasingX = direction.X >= 0;
            increasingY = direction.Y >= 0;

            yIntercept = position.Y - gradient * position.X;
        }





        /// <summary>
        /// returns a position on the line at the given x value
        /// </summary>
        /// <param name="x">x value</param>
        /// <returns>position on the line</returns>
        public Coordinate reachesX(double x)
        {
            Coordinate reach = new Coordinate(x, yIntercept);

            if (gradient != 0)
                reach.Y += gradient * x;

            return reach;
        }

        /// <summary>
        /// calculates the coordinate of the next impact on a short side (player or robot side); return value depends on the direction with which the line has been initialized
        /// </summary>
        /// <returns>coordinate of the next impact</returns>
        public Coordinate nextImpactOnShortSide()
        {
            double distance = Config.Instance.PuckRadius;
            if (increasingX)
                distance = Config.Instance.GameFieldSize.Width - 1 - distance;

            return reachesX(distance);
        }

        /// <summary>
        /// calculates the coordinate of the previous impact on a short side (player or robot side); return value depends on the direction with which the line has been initialized
        /// </summary>
        /// <returns>coordinate of the next impact</returns>
        public Coordinate previousImpactOnShortSide()
        {
            double distance = Config.Instance.PuckRadius;
            if (!increasingX)
                distance = Config.Instance.GameFieldSize.Width - 1 - distance;

            return reachesX(distance);
        }

        /// <summary>
        /// creates the next motion line after the bat hit a short side (player or robot side)
        /// </summary>
        /// <returns>next motion line after the bat hit a short side</returns>
        public StraightLine onShortSideReflected()
        {
            Coordinate invertedDirection = new Coordinate(Direction);
            invertedDirection.X *= -1;
            return new StraightLine(nextImpactOnShortSide(), invertedDirection);
        }





        /// <summary>
        /// returns a position on the line at the given y value
        /// </summary>
        /// <param name="y">y value</param>
        /// <returns>position on the line</returns>
        public Coordinate reachesY(double y)
        {
            Coordinate reach = new Coordinate(0, y);

            if (gradient != 0)
                reach.X = (y - yIntercept) / gradient;
            else
                throw new ArgumentOutOfRangeException("given y unreachable");

            return reach;
        }

        /// <summary>
        /// calculates the coordinate of the next impact on a long side; return value depends on the direction with which the line has been initialized
        /// </summary>
        /// <returns>coordinate of the next impact</returns>
        public Coordinate nextImpactOnLongSide()
        {
            double distance = Config.Instance.PuckRadius;
            if (increasingY)
                distance = Config.Instance.GameFieldSize.Height - 1 - distance;

            return reachesY(distance);
        }

        /// <summary>
        /// calculates the coordinate of the previous impact on a long side; return value depends on the direction with which the line has been initialized
        /// </summary>
        /// <returns>coordinate of the previous impact</returns>
        public Coordinate previousImpactOnLongSide()
        {
            double distance = Config.Instance.PuckRadius;
            if (!increasingY)
                distance = Config.Instance.GameFieldSize.Height - 1 - distance;

            return reachesY(distance);
        }

        /// <summary>
        /// creates the next motion line after the bat hit a long side
        /// </summary>
        /// <returns>next motion line after the bat hit a short side</returns>
        public StraightLine onLongSideReflected()
        {
            Coordinate invertedDirection = new Coordinate(Direction);
            invertedDirection.Y *= -1;
            return new StraightLine(nextImpactOnLongSide(), invertedDirection);
        }





        /// <summary>
        /// calculates previous impact on a long or short side; return value depends on the direction with which the line has been initialized
        /// </summary>
        /// <returns>coordinate of the next impact</returns>
        public Coordinate previousImpact()
        {
            Coordinate retval;

            try
            {
                retval = previousImpactOnLongSide();
                if (!Coordinate.insideBounds(retval))
                    retval = previousImpactOnShortSide();
            }
            catch (Exception e)
            {
                retval = previousImpactOnShortSide();
            }

            return retval;
        }

        /// <summary>
        /// calculates next impact on a long or short side; return value depends on the direction with which the line has been initialized
        /// </summary>
        /// <returns>coordinate of the next impact</returns>
        public Coordinate nextImpact()
        {
            Coordinate retval;

            try
            {
                retval = nextImpactOnLongSide();
                if (!Coordinate.insideBounds(retval))
                    retval = nextImpactOnShortSide();
            }
            catch (Exception e)
            {
                retval = nextImpactOnShortSide();
            }

            return retval;
        }

        /// <summary>
        /// calculates reflected line on next impact
        /// </summary>
        /// <returns></returns>
        public StraightLine reflectedLine()
        {
            StraightLine retval;

            try
            {
                if (Coordinate.insideBounds(nextImpactOnLongSide()))
                    retval = onLongSideReflected();
                else
                    retval = onShortSideReflected();
            }
            catch (Exception e)
            {
                retval = onShortSideReflected();
            }

            return retval;
        }

        public bool isSimilarDirection(StraightLine other)
        {
            return increasingX == other.increasingX && increasingY == other.increasingY;
        }

        public bool isOnLine(Coordinate position)
        {
            Coordinate orthogonalDirection = new Coordinate();
            orthogonalDirection.X = Direction.Y;
            orthogonalDirection.Y = -1 * Direction.X;

            StraightLine orthogonalLine = new StraightLine(position, orthogonalDirection);
            Vector distanceVector = new Vector(position, intersection(orthogonalLine));

            return distanceVector.Length < 5;
        }

        public Coordinate intersection(StraightLine other)
        {
            Coordinate intersection = new Coordinate();
            intersection.X = (other.yIntercept - yIntercept) / (gradient - other.gradient);
            intersection.Y = gradient * intersection.X + yIntercept;
            return intersection;
        }
    }
}
