using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    class SimulationPositionCollector : PositionCollector
    {
        //lines on which the bat moves
        List<StraightLine> motion;
        int motionIterator;

        //last position of the bat
        TimedCoordinate last;

        //speed of the simulated bat
        double speed;

        //minimum duration for retreiving a position
        int minDuration;

        /// <summary>
        /// creates a motion simulation
        /// </summary>
        /// <param name="start">start coordinate</param>
        /// <param name="angleToLongSide">angle to x-axis; 90 &lt; x &lt; 270</param>
        /// <param name="speed">speed of bat in px/s</param>
        /// <param name="minDuration">minimal needed time pre position calculation</param>
        public SimulationPositionCollector(Coordinate start, double angleToLongSide, double speed, int minDuration = 0)
        {
            if (angleToLongSide <= 90 || angleToLongSide >= 270)
                throw new ArgumentException("invalid value for angleToLongSide");

            this.speed = speed;
            this.minDuration = minDuration;
            last = new TimedCoordinate(start);
            motion = new List<StraightLine>();
            motionIterator = 0;

            //calculate direction of first line
            Coordinate direction = new Coordinate(0, start.Y - Config.Instance.PuckRadius);

            if (angleToLongSide == 180)
                direction = new Coordinate(-1, 0);
            else
            {
                //get angle of triangle with x-axis
                if (angleToLongSide > 180)
                {
                    direction.Y *= -1;
                    angleToLongSide = 90 - (angleToLongSide - 180);
                }
                else
                    angleToLongSide -= 90;

                direction.X -= Math.Abs(direction.Y) * Math.Sin((Math.PI / 180) * angleToLongSide) / Math.Cos((Math.PI / 180) * angleToLongSide);
            }

            //creates first line
            motion.Add(new StraightLine(start, direction));

            //calculates other lines
            while (motion.Last().nextImpact().X < start.X)
                motion.Add(motion.Last().reflectedLine());
        }

        public override TimedCoordinate GetPuckPosition()
        {
            //get current time
            long timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            //check if start time is set
            if (last.Timestamp == 0)
                last.Timestamp = timeStamp;

            //calculate distance bat would have traveled
            double length = speed * (timeStamp - last.Timestamp);

            TimedCoordinate position = new TimedCoordinate(getCoordinateFromDistance(length), timeStamp);

            //
            int sleepDuration = (int)(minDuration + timeStamp - DateTimeOffset.Now.ToUnixTimeMilliseconds());
            if (sleepDuration > 0)
                Thread.Sleep(sleepDuration);

            return last = position;
        }

        private Coordinate getCoordinateFromDistance(double length)
        {
            //get direction of current line
            Coordinate direction = new Coordinate(motion[motionIterator].Direction);

            //calculate vector to new position
            double multiplicator = Math.Sqrt(Math.Pow(length, 2) / (Math.Pow(direction.X, 2) + Math.Pow(direction.X, 2)));
            direction.X *= multiplicator;
            direction.Y *= multiplicator;

            //calculate next position
            Coordinate newPosition = new Coordinate(last);
            newPosition.X += direction.X;
            newPosition.Y += direction.Y;

            //check if new position is inside game field
            if (!Coordinate.insideBounds(newPosition))
            {
                //calculate position of next impact
                Coordinate next = motion[motionIterator].nextImpact();

                //get distance between lasp point and impact
                //substract that distance from given distance
                length -= new Vector(last, next).Length;

                //set line iterator to next line
                motionIterator++;

                //set impact as last
                last = new TimedCoordinate(next);

                //get coordinate from new distance
                newPosition = getCoordinateFromDistance(length);
            }

            return newPosition;
        }

        public override List<TimedCoordinate> GetPuckPositions()
        {
            List<TimedCoordinate> positions = new List<TimedCoordinate>();

            for (int a = 0; a < 10; a++)
                positions.Add(GetPuckPosition());

            return positions;
        }

        public override void StopMotionCapturing()
        {
        }
    }
}
