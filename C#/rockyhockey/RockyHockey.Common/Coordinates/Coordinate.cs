using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
    /// <summary>
    /// represents a point on the game field
    /// </summary>
    [Serializable]
    public class Coordinate
    {
        /// <summary>
        /// the constructor constructs an instance of the class of which it is the constructor (creates a coordinate with given x and y parts)
        /// </summary>
        /// <param name="x">x of the coordinate</param>
        /// <param name="y">y of the coordinate</param>
        public Coordinate(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// the constructor constructs an instance of the class of which it is the constructor (copies given coordinate)
        /// </summary>
        /// <param name="original">the coordinate to be copied</param>
        public Coordinate(Coordinate original)
        {
            X = original.X;
            Y = original.Y;
        }

        /// <summary>
        /// the constructor constructs an instance of the class of which it is the constructor (creates a coordinate with x=0 and y=0)
        /// </summary>
        public Coordinate() { }

        /// <summary>
        /// X-Coordinate
        /// </summary>
        public double X { get; set; } = 0;

        /// <summary>
        /// Y-Coordinate
        /// </summary>
        public double Y { get; set; } = 0;

        public static bool operator ==(Coordinate obj1, Coordinate obj2)
        {
            bool leftIsNull = ReferenceEquals(obj1, null);
            bool rightIsNull = ReferenceEquals(obj2, null);

            bool retVal = leftIsNull ^ rightIsNull;
            retVal = !retVal;

            if (retVal && !leftIsNull)
                retVal = obj1.Equals(obj2);

            return retVal;
        }

        public static bool operator !=(Coordinate obj1, Coordinate obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(Coordinate other)
        {
            bool retVal = false;

            if (!ReferenceEquals(null, other))
            {
                if (ReferenceEquals(this, other))
                {
                    retVal = true;
                }
                else
                {
                    retVal = this.X == other.X && this.Y == other.Y;
                }
            }

            return retVal;
        }

        public override bool Equals(object other)
        {
            bool retVal = false;

            if (other.GetType().IsAssignableFrom(typeof(Coordinate)))
                retVal = Equals((Coordinate)other);

            return retVal;
        }

        /// <summary>
        /// checks if puck position is within the field
        /// </summary>
        /// <param name="jitter">pixel tolerance</param>
        /// <returns></returns>
        public bool insideBounds(double jitter = 0)
        {
            double radius = Config.Instance.PuckRadius - jitter;

            bool retval = X >= radius;

            if (retval)
            {
                retval = X <= Config.Instance.GameFieldSize.Width - radius;

                if (retval)
                {
                    retval = Y >= radius;

                    if (retval)
                        retval = Y <= Config.Instance.GameFieldSize.Height - radius;
                }
            }

            return retval;
        }
    }
}
