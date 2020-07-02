using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public abstract class PositionCollector
    {
        public ImageProvider imageProvider { get; protected set; }

        public abstract List<TimedCoordinate> GetPuckPositions();
        public abstract TimedCoordinate GetPuckPosition();
        public abstract void StopMotionCapturing();
    }
}
