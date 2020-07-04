using NUnit.Framework;
using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.MovementFramework.Unittests
{
    [TestFixture]
    public class MovementControllerTests
    {
        [TestCase(0, 0, -1500, -1500)]
        public async Task TestMovement(double xPos, double yPos, double xDir, double yDir)
        {
            var vector = new VelocityVector(
                new TimedCoordinate { X = xPos, Y = yPos },
                new TimedCoordinate { X = xDir, Y = yDir });

            vector.Velocity = 1500;

            var vectors = new List<VelocityVector> { vector };

            var tokenSource = new CancellationTokenSource();
            MovementController.Instance.MoveStrategy(vectors, 0);

            Thread.Sleep(5000);
        }
    }
}
