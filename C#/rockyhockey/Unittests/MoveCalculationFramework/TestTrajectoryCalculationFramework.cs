using RockyHockey.MotionCaptureFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    public class TestTrajectoryCalculationFramework : TrajectoryCalculationFramework
    {
        public TestTrajectoryCalculationFramework()
        {
            motionCaptureProvider = new PositionCollector(new TestImageReader());
            vectorCalculationProvider = new VectorCalculationProvider(motionCaptureProvider);
        }
    }
}
