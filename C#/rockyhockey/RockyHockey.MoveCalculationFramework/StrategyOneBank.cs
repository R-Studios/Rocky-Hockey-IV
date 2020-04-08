using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyHockey.Common;

namespace RockyHockey.MoveCalculationFramework
{
    /// <summary>
    /// Class where a tangent from the impact position to the goal is calculated over one bank
    /// </summary>
    internal class StrategyOneBank : IStrategy
    {
        private readonly Size gameFieldSize;

        /// <summary>
        /// Constructs a new instance of the StrategyOnebank
        /// </summary>
        /// <param name="gameFieldSize">Size of the game field</param>
        public StrategyOneBank(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;
        }

        /// <summary>
        /// Calcualte a tangent to the circle on which the trajectory for the bat is
        /// </summary>
        /// <param name="impactPosition">Position where the puck will be played</param>
        /// <param name="goalPosition">Position of the goal</param>
        /// <returns>Tangent to the circle on which the trajectory for the bat is</returns>
        public async Task<Vector> getTangent(GameFieldPosition impactPosition, GameFieldPosition goalPosition)
        {
            GameFieldPosition reflactingPoint = await calculateReflactingPoint(impactPosition, goalPosition).ConfigureAwait(false);
            return new Vector
            {
                Position = impactPosition,
                Direction = reflactingPoint
            };
        }

        /// <summary>
        /// Calculates the position where the puck  must hit the bank
        /// </summary>
        /// <param name="impactPosition">Position where the puck will be played</param>
        /// <param name="goalPosition">position of the goal</param>
        /// <returns>Position where the puck must hit the bank</returns>
        private Task<GameFieldPosition> calculateReflactingPoint(GameFieldPosition impactPosition, GameFieldPosition goalPosition)
        {
            return Task.Factory.StartNew(() =>
            {
                double factor = goalPosition.Y + impactPosition.Y;
                double ratio = gameFieldSize.Width / factor;
                double reflactingPointXPosition = ratio * goalPosition.Y;
                return new GameFieldPosition { X = reflactingPointXPosition, Y = 0 };
            });
        }
    }
}
