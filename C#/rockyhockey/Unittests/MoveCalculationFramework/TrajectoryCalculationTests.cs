using NUnit.Framework;
using RockyHockey.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework.Unittests
{
    [TestFixture]
    public class TrajectoryCalculationTests
    {
        [SetUp]
        public void SetUp()
        {
            Config.Instance.GameFieldSize = new Size(700, 400);
            Config.Instance.FrameRate = 187;
            Config.Instance.ImaginaryAxePosition = 600;
            Config.Instance.Camera1.index = 0;
            Config.Instance.Camera2.index = 1;
            Config.Instance.Tolerance = 6;
            Config.Instance.MaxBatVelocity = 100;
        }

        [TestCase(0, 0, 1, 1, 600, 200)]
        [TestCase(1, 1, 3, 6, 600, 101.50000000000023)]
        [TestCase(4.5, 7.56, 14, 15, 600, 326.06947368420879)]
        public async Task TestCalculatePuckTrajectory(double xPos, double yPos, double xDir, double yDir, double xImpact, double yImpact)
        {
            var vector = new Vector
            {
                Position = new GameFieldPosition { X = xPos, Y = yPos },
                Direction = new GameFieldPosition { X = xDir, Y = yDir }
            };

            var trajectoryCalculationFramework = new TrajectoryCalculationFramework();

            IEnumerable<Vector> vectors = await trajectoryCalculationFramework.CalculatePuckTrajectory(vector, Config.Instance.GameFieldSize);
            GameFieldPosition impactPosition = vectors.Last().Direction;
            Assert.AreEqual(xImpact, impactPosition.X);
            Assert.AreEqual(yImpact, impactPosition.Y);
        }

        [TestCase(0, 0, 1, 1, 5)]
        [TestCase(1, 1, 3, 6, 19)]
        [TestCase(445, 123, 444, 986, 40)]
        [TestCase(-4, -5, 3425, 2134, 55)]
        [TestCase(-112, -66, -2345, -234, 33)]
        [TestCase(88, 66, -241, -451, 98)]
        [TestCase(0.123, 6.567, 24.56, 35.65, -4)]
        public async Task TestCalculateNeededTime(double xPos, double yPos, double xDir, double yDir, double velocity)
        {
            var vectors = new List<Vector>();
            vectors.Add(new Vector
            {
                Position = new GameFieldPosition { X = xPos, Y = yPos },
                Direction = new GameFieldPosition { X = xDir, Y = yDir }
            });
            var trajectoryCalculationFramework = new TrajectoryCalculationFramework();
            double length = vectors.First().GetVectorLength();
            double expectedMilliSeconds = (length / velocity) * 1000;

            double neededTime = await trajectoryCalculationFramework.CalculateNeededTime(vectors, velocity).ConfigureAwait(false);
            Assert.AreEqual(expectedMilliSeconds, neededTime);
        }
    }
}
