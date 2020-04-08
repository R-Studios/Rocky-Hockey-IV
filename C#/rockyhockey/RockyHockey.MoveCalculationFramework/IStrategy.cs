using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    /// <summary>
    /// Interface for the different strategies
    /// </summary>
    internal interface IStrategy
    {
        /// <summary>
        /// Calcualte a tangent to the circle on which the trajectory for the bat is
        /// </summary>
        /// <param name="impactPosition">Position where the puck will be played</param>
        /// <param name="goalPosition">Position of the goal</param>
        /// <returns>Tangent to the circle on which the trajectory for the bat is</returns>
        Task<Vector> getTangent(GameFieldPosition impactPosition, GameFieldPosition goalPosition);
    }
}
