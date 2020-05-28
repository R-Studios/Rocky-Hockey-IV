using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public interface PositionCollector
    {
        List<TimedCoordinate> GetPuckPositions();
        TimedCoordinate GetPuckPosition();
        void StopMotionCapturing();
    }
}
