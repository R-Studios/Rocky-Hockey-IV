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
        //the current calculated motion part
        StraightLine currentMotion;

        //last position of the puck
        TimedCoordinate last;

        //speed of the simulated puck
        double speed;

        //minimum duration for retreiving a position
        int minDuration;

        /// <summary>
        /// creates a motion simulation
        /// </summary>
        /// <param name="start">start coordinate</param>
        /// <param name="angleToLongSide">angle to x-axis; 90 &lt; x &lt; 270</param>
        /// <param name="speed">speed of puck in px/s</param>
        /// <param name="minDuration">minimal needed time pre position calculation</param>
        public SimulationPositionCollector(Coordinate start, Coordinate end, double speed, int minDuration = 0)
        {
            this.speed = speed / 1000;
            this.minDuration = minDuration;
            last = new TimedCoordinate(start);

            currentMotion = new StraightLine(new Vector(start, end));
        }

        public override TimedCoordinate GetPuckPosition()
        {
            //get current time
            long timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            //check if start time is set
            if (last.Timestamp == 0)
                last.Timestamp = timeStamp;

            //calculate distance puck would have traveled
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
            Coordinate direction = new Coordinate(currentMotion.Direction);

            //calculate vector to new position
            double factor = length / new Vector(new Coordinate(), direction).Length;
            direction.X *= factor;
            direction.Y *= factor;

            //calculate next position
            Coordinate newPosition = new Coordinate(last);
            newPosition.X += direction.X;
            newPosition.Y += direction.Y;

            //check if new position is inside game field
            if (!newPosition.insideBounds())
            {
                //calculate position of next impact
                Coordinate next = currentMotion.nextImpact();

                //get distance between last point and impact
                //substract that distance from given distance
                length -= new Vector(last, next).Length;

                //get next part of motion
                currentMotion = currentMotion.reflectedLine();

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
