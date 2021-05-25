using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RockyHockey.Common
{
    [Serializable]
    public class GameFieldConfig
    {
        public Point UpperLeft { get; set; }

        public Point UpperRight { get; set; }

        public Point LowerLeft { get; set; }

        public Point LowerRight { get; set; }
        
        /// <summary>
        /// Origin of coordinate system
        /// </summary>
        public Point XYOrigin { get; set; }

        public Point ExtremeX { get; set; }

        public Point ExtremeY { get; set; }

        public Point Offset { get; set; }

    }


}
