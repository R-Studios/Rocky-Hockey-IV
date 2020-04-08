using RockyHockey.Common;
using System.Threading.Tasks;

namespace RockyHockey.MovementFramework
{
    /// <summary>
    /// Contains time calculation functions
    /// </summary>
    public class TimeCalculator
    {
        /// <summary>
        /// Calculates the needed time for the velocity vector
        /// </summary>
        /// <param name="velocityVector">Vector with velocity</param>
        /// <returns>time in milliseconds</returns>
        public async Task<double> CalculateTimeOfVelocityVector(VelocityVector velocityVector)
        {
            var length = await velocityVector.GetVectorLength().ConfigureAwait(false);
            return (length / velocityVector.Velocity);
        }
    }
}
