using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyHockey.Common;

namespace RockyHockey.MoveCalculationFramework
{
    /// <summary>
    /// Class where a tangent from the impact position to the goal is calculated directly
    /// </summary>
    internal class StrategyDirect : IStrategy
    {
        /// <summary>
        /// Calcualte a tangent to the circle on which the trajectory for the bat is
        /// </summary>
        /// <param name="impactPosition">Position where the puck will be played</param>
        /// <param name="goalPosition">Position of the goal</param>
        /// <returns>Tangent to the circle on which the trajectory for the bat is</returns>
        public Task<Vector> getTangent(GameFieldPosition impactPosition, GameFieldPosition goalPosition)
        {
            return Task.Factory.StartNew(() => new Vector
            {
                Position = impactPosition,
                Direction = goalPosition
            });
        }
    }
}
