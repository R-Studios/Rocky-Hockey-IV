using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using System;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework.Unittests
{
    /// <summary>
    /// Mock class for testing
    /// </summary>
    public class MockVectorCalculationProvider
    {
        /// <summary>
        /// Creates a new Instance of the Mock class
        /// </summary>
        /// <param name="motionCaptureProvider"></param>
        public MockVectorCalculationProvider(IMotionCaptureProvider motionCaptureProvider)
        {
            rnd = new Random();
            cnt = 0;
        }

        private Random rnd;

        private VelocityVector vec;

        private int cnt;

        /// <summary>
        /// Returns a random vector
        /// </summary>
        /// <returns></returns>
        public Task<VelocityVector> CalculatePuckVector()
        {
            return Task.Factory.StartNew(() =>
            {
                vec = new VelocityVector
                {
                    Position = new GameFieldPosition { X = 30, Y = 200 },
                    Direction = new GameFieldPosition { X = rnd.Next(31, 250), Y = rnd.Next(0, 300) },
                    Velocity = rnd.NextDouble()
                };
                cnt++;
                return vec;
            });
        }
    }
}
