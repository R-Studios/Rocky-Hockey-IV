using NUnit.Framework;
using RockyHockey.Common;
using System.Drawing;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework.Unittests
{
    [TestFixture]
    public class VectorCalculationProviderTests
    {
        [SetUp]
        public void Setup()
        {
            Config.Instance.GameFieldSize = new Size(398, 224);
            Config.Instance.FrameRate = 187;
            Config.Instance.ImaginaryAxePosition = 320;
            Config.Instance.Camera1.index = 0;
            Config.Instance.Camera2.index = 1;
            Config.Instance.Tolerance = 5;
            Config.Instance.MaxBatVelocity = 100;
        }

        [Test]
        public async Task TestCalculatePuckVelocity()
        {
            var vectorCalculationProvider = new VectorCalculationProvider(new MockMotionCaptureProvider());
            VelocityVector vec = await vectorCalculationProvider.CalculatePuckVector().ConfigureAwait(false);
            double expectedVelocity = 0.485054301382442;
            Assert.AreEqual(expectedVelocity, vec.Velocity);
        }

        [Test]
        public async Task TestCalculatedDirection()
        {
            var vectorCalculationProvider = new VectorCalculationProvider(new MockMotionCaptureProvider());
            VelocityVector vec = await vectorCalculationProvider.CalculatePuckVector().ConfigureAwait(false);
            Assert.AreEqual(1, vec.Direction.X);
            Assert.AreEqual(0.081133540372670773, vec.Direction.Y);
        }
    }
}
