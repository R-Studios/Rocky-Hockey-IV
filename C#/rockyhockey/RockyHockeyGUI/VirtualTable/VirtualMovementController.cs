using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyHockey.Common;
using RockyHockey.GoalDetectionFramework;
using RockyHockey.MovementFramework;

namespace RockyHockeyGUI.VirtualTable
{
    class VirtualMovementController : IMovementController
    {
        public GameFieldPosition BatPosition { get; }
        public void InitializeSerialPorts()
        {
            throw new NotImplementedException();
        }

        public Task MoveStrategy(IEnumerable<VelocityVector> vec, int delayBeforePunch)
        {
            throw new NotImplementedException();
        }

        public Task CloseSerialPorts()
        {
            throw new NotImplementedException();
        }

        public void OnGoalDetected(object sender, DetectedGoalEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
