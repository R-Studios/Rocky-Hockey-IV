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
        public CameraConfig(String name)
        {
            this.name = name;
        }

        private CameraConfig() { }

        /// <summary>
        /// Index of the camera
        /// </summary>
        public int index;

        public String name;

        /// <summary>
        /// wether or not the camera records the origin coordinate
        /// </summary>
        public bool displaysOrigin { get; set; } = true;

        /// <summary>
        /// degrees how far to rotate the image; 0, 90, 180, 270 allowed
        /// </summary>
        public int ImageRotation { get; set; } = 0;

        /// <summary>
        /// size of game field part visible on the camera images
        /// </summary>
        public Size FieldView { get; set; } = new Size(0, 0);

        /// <summary>
        /// resolution of Camera
        /// </summary>
        public Size Resolution { get; set; } = new Size(0, 0);

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
}
