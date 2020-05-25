using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework.Unittests
{
    /// <summary>
    /// Mock Class, that returns random positions
    /// </summary>
    public class MockMotionCaptureProvider : IMotionCaptureProvider
    {
        private int count = 0;

        /// <summary>
        /// Returns a random Position
        /// </summary>
        /// <returns>random Position</returns>
        public async Task<TimedCoordinate> GetBatPosition()
        {
            return await CreatePosition().ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a random Position
        /// </summary>
        /// <returns>random Position</returns>
        public async Task<TimedCoordinate> GetEnemyBatPosition()
        {
            return await CreatePosition().ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a random game field size
        /// </summary>
        /// <returns>game field size</returns>
        public Task<Size> GetGameFieldSize()
        {
            return Task.Factory.StartNew(() =>
            {
                return new Size(640, 480);
            });
        }

        /// <summary>
        /// Returns a random Position
        /// </summary>
        /// <returns>random Position</returns>
        public async Task<List<TimedCoordinate>> GetPuckPositions()
        {
            var positions = new List<TimedCoordinate>();
            for (int i = 0; i < 5; i++)
            {
                positions.Add(await CreatePosition().ConfigureAwait(false));
            }
            return positions;
        }

        private Task<TimedCoordinate> CreatePosition()
        {
            return Task.Factory.StartNew(() =>
            {
                var pos = new TimedCoordinate
                {
                    X = count,
                    Y = count,
                    Timestamp = count
                };
                if (count % 2 == 0)
                {
                    pos.X = count + (count * 0.9);
                    pos.Timestamp = count;
                }
                count += 1;
                return pos;
            });
        }

        /// <summary>
        /// Mock stop-method
        /// </summary>
        /// <returns>executeable Task</returns>
        public void StopMotionCapturing()
        {
            throw new System.NotSupportedException();
        }
    }
}
