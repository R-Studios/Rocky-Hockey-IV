using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
    [Serializable]
    public class CameraConfig
    {
        public CameraConfig(int index)
        {
            this.index = index;
        }

        private CameraConfig() { }

        /// <summary>
        /// Index of the camera
        /// </summary>
        public int index;

        /// <summary>
        /// game field size in px
        /// </summary>
        public Size FieldSize { get; set; } = new Size(0, 0);

        /// <summary>
        /// camera offset
        /// </summary>
        public CameraOffset Offset { get; set; } = new CameraOffset();

        /// <summary>
        /// upper left position for warping
        /// </summary>
        public Coordinate UpperLeft { get; set; } = new Coordinate();

        /// <summary>
        /// upper right position for warping
        /// </summary>
        public Coordinate UpperRight { get; set; } = new Coordinate();

        /// <summary>
        /// lower right position for warping
        /// </summary>
        public Coordinate LowerRight { get; set; } = new Coordinate();

        /// <summary>
        /// lower left position for warping
        /// </summary>
        public Coordinate LowerLeft { get; set; } = new Coordinate();
    }

    /// <summary>
    /// class to store the offset of the camera to the game field
    /// </summary>
    public class CameraOffset
    {
        public CameraOffset(int fromLeft, int fromTop)
        {
            FromTop = fromTop;
            FromLeft = fromLeft;
        }

        public CameraOffset() { }

        /// <summary>
        /// X-Position
        /// </summary>
        public int FromTop { get; set; } = 0;

        /// <summary>
        /// Y-Position
        /// </summary>
        public int FromLeft { get; set; } = 0;
    }
}
