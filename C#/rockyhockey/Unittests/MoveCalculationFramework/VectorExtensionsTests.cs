using NUnit.Framework;
using RockyHockey.Common;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework.Unittests
{
    [TestFixture]
    public class VectorExtensionsTests
    {
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 1, 3, 6)]
        [TestCase(445, 123, 444, 986)]
        [TestCase(-4, -5, 3425, 2134)]
        [TestCase(-112, -66, -2345, -234)]
        [TestCase(88, 66, -241, -451)]
        [TestCase(0.123, 6.567, 24.56, 35.65)]
        public async Task TestGetNormalVector(double xPos, double yPos, double xDir, double yDir)
        {
            var vector = new Vector
            {
                Position = new GameFieldPosition { X = xPos, Y = yPos },
                Direction = new GameFieldPosition { X = xDir, Y = yDir }
            };

            Vector normalVector = vector.GetZeroVector();
            double expectedX = xDir - xPos;
            double expectedY = yDir - yPos;

            Assert.AreEqual(0, normalVector.Position.X);
            Assert.AreEqual(0, normalVector.Position.Y);
            Assert.AreEqual(expectedX, normalVector.Direction.X);
            Assert.AreEqual(expectedY, normalVector.Direction.Y);
        }

        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 1, 3, 6)]
        [TestCase(445, 123, 444, 986)]
        [TestCase(-4, -5, 3425, 2134)]
        [TestCase(-112, -66, -2345, -234)]
        [TestCase(88, 66, -241, -451)]
        [TestCase(0.123, 6.567, 24.56, 35.65)]
        public void TestGetVectorPitch(double xPos, double yPos, double xDir, double yDir)
        {
            var vector = new Vector
            {
                Position = new GameFieldPosition { X = xPos, Y = yPos },
                Direction = new GameFieldPosition { X = xDir, Y = yDir }
            };

            double pitch = vector.GetVectorGradient();
            Vector normalVector = vector.GetZeroVector();
            double expectedPitch = normalVector.Direction.Y / normalVector.Direction.X;

            Assert.AreEqual(expectedPitch, pitch);
        }

        [TestCase(0, 0, 1, 1, 400)]
        [TestCase(2, 2, 4, 4, 300)]
        public async Task TestStretchVectorToXCoordinate(double xPos, double yPos, double xDir, double yDir, double yBarrier)
        {
            var vector = new Vector
            {
                Position = new GameFieldPosition { X = xPos, Y = yPos },
                Direction = new GameFieldPosition { X = xDir, Y = yDir }
            };

            Vector strechedVector = await vector.StretchVectorToXCoordinate(yBarrier).ConfigureAwait(false);
            Assert.AreEqual(yBarrier, strechedVector.Direction.Y);
            Assert.AreEqual(yBarrier, strechedVector.Direction.X);
        }

        [TestCase(0, 0, 1, 1, 400)]
        [TestCase(2, 2, 4, 4, 300)]
        public async Task TestStretchVectorToYCoordinate(double xPos, double yPos, double xDir, double yDir, double xBarrier)
        {
            var vector = new Vector
            {
                Position = new GameFieldPosition { X = xPos, Y = yPos },
                Direction = new GameFieldPosition { X = xDir, Y = yDir }
            };

            Vector strechedVector = vector.StretchVectorToYCoordinate(xBarrier);
            Assert.AreEqual(xBarrier, strechedVector.Direction.Y);
            Assert.AreEqual(xBarrier, strechedVector.Direction.X);
        }

        [TestCase(7, 7, 8, 8)]
        [TestCase(2, 2, 3, 3)]
        public void TestCalculateReflectedVector(double xPos, double yPos, double xDir, double yDir)
        {
            var vector = new Vector
            {
                Position = new GameFieldPosition { X = xPos, Y = yPos },
                Direction = new GameFieldPosition { X = xDir, Y = yDir }
            };

            Vector reflectedVector = vector.CalculateReflectedVector();

            double expectedXPos = xDir;
            double expectedYPos = yDir;
            double expectedXDir = xDir + 1;
            double expectedYDir = yDir - 1;

            Assert.AreEqual(expectedXPos, reflectedVector.Position.X);
            Assert.AreEqual(expectedYPos, reflectedVector.Position.Y);
            Assert.AreEqual(expectedXDir, reflectedVector.Direction.X);
            Assert.AreEqual(expectedYDir, reflectedVector.Direction.Y);
        }

        [TestCase(0, 0, 0, 8)]
        [TestCase(0, 0, 8, 0)]
        public async Task TestGetVectorLength(double xPos, double yPos, double xDir, double yDir)
        {
            var vector = new Vector
            {
                Position = new GameFieldPosition { X = xPos, Y = yPos },
                Direction = new GameFieldPosition { X = xDir, Y = yDir }
            };

            var vectorLength = vector.GetVectorLength();
            double expectedLength = 8;
            Assert.AreEqual(expectedLength, vectorLength);
        }
    }
}
