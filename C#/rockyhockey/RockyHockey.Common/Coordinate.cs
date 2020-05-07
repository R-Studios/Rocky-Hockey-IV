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
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// the constructor constructs an instance of the class of which it is the constructor (creates a coordinate with x=0 and y=0)
        /// </summary>
        public Coordinate() { }

        /// <summary>
        /// X-Coordinate
        /// </summary>
        public int X { get; set; } = 0;

        /// <summary>
        /// Y-Coordinate
        /// </summary>
        public int Y { get; set; } = 0;
    }
}
