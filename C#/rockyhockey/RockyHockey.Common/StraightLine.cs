using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
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
                if (!retval.insideBounds())
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
                if (!retval.insideBounds())
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
                if (nextImpactOnLongSide().insideBounds())
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

        /// <summary>
        /// checks if given position is on line; checks if distance of given position to the line is lower than the jitter
        /// </summary>
        /// <param name="position">position to check</param>
        /// <param name="jitter">max allowed distance to the line</param>
        /// <returns>true if distance to line greater jitter</returns>
        public bool isOnLine(Coordinate position, double jitter = 0)
        {
            Coordinate orthogonalDirection = new Coordinate();
            orthogonalDirection.X = Direction.Y;
            orthogonalDirection.Y = -1 * Direction.X;

            StraightLine orthogonalLine = new StraightLine(position, orthogonalDirection);
            Vector distanceVector = new Vector(position, intersection(orthogonalLine));

            return distanceVector.Length < jitter;
        }

        /// <summary>
        /// calcualtes position where given line intersects with this one
        /// </summary>
        /// <param name="other"></param>
        /// <returns>position of intersection</returns>
        public Coordinate intersection(StraightLine other)
        {
            Coordinate intersection = new Coordinate();
            intersection.X = (other.yIntercept - yIntercept) / (gradient - other.gradient);
            intersection.Y = gradient * intersection.X + yIntercept;
            return intersection;
        }
    }
}
