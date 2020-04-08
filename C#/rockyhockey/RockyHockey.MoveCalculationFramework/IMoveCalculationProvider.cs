using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    /// <summary>
    /// Schnittstelle für das MoveCalculationFramework
    /// </summary>
    public interface IMoveCalculationProvider
    {
        /// <summary>
        /// Stoßt das die implementierte KI an
        /// </summary>
        /// <returns></returns>
        Task StartCalculation(IProgress<IEnumerable<IEnumerable<Vector>>> progress = null);

        /// <summary>
        /// Stoppt die KI
        /// </summary>
        /// <returns></returns>
        Task StopCalculation();
    }
}
