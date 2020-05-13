using RockyHockey.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    /// <summary>
    /// Schnittstelle für den MotionCaptureProvider
    /// </summary>
    public interface IMotionCaptureProvider
    {
        /// <summary>
        /// Ermittelt die Bewegungsrichtung des Pucks
        /// </summary>
        /// <returns>Bewegungsrichtung in Form eines Vektors</returns>
        Task<IEnumerable<FrameGameFieldPosition>> GetPuckPositions();

        /// <summary>
        /// Ermittelt die Position des programmgesteuerten Schlägers
        /// </summary>
        /// <returns>genaue Position des Schlägers</returns>
        Task<GameFieldPosition> GetBatPosition();

        /// <summary>
        /// Ermittelt die Position des Schlägers des Menschen
        /// </summary>
        /// <returns>genaue Position des Schlägers</returns>
        Task<GameFieldPosition> GetEnemyBatPosition();

        /// <summary>
        /// Calculates the size of the gamefield
        /// </summary>
        /// <returns>Gamefieldsize</returns>
        Task<Size> GetGameFieldSize();

        /// <summary>
        /// Stops the MotionCaptureProvider
        /// </summary>
        /// <returns>executeable Task</returns>
        void StopMotionCapturing();
    }
}
