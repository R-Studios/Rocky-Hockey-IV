using RockyHockey.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    /// <summary>
    /// Interface for the stragey calculation
    /// </summary>
    public interface IStrategyCalculationFramework
    {
        /// <summary>
        /// Calculates a punch strategy
        /// </summary>
        /// <param name="actionGate">position where the puck should be hit</param>
        /// <param name="timeLeft">time left in milliseconds</param>
        /// <param name="angle">angle of the incoming puck</param>
        /// <returns></returns>
        Task<IEnumerable<Vector>> CalculateStartegy(GameFieldPosition actionGate, double timeLeft, double angle);
    }
}
