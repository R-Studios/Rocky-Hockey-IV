using RockyHockey.Common;
using RockyHockey.GoalDetectionFramework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.MovementFramework
{
    /// <summary>
    /// Schnittstelle für den MovementController
    /// </summary>
    public interface IMovementController
    {
        /// <summary>
        /// Contains the Position of the Bat
        /// </summary>
        GameFieldPosition BatPosition { get; }

        /// <summary>
        /// Initializes the serial Ports
        /// </summary>
        /// <returns>executeable Task</returns>
        void InitializeSerialPorts();

        /// <summary>
        /// Bewegt den Schläger in die Richtung des Vektors
        /// </summary>
        /// <param name="vec">Vectors to move</param>
        /// <param name="delayBeforePunch">delay time before the punch in milliseconds</param>
        /// <returns></returns>
        Task MoveStrategy(IEnumerable<VelocityVector> vec, int delayBeforePunch);

        /// <summary>
        /// Closes the serial Ports
        /// </summary>
        /// <returns>executeable Task</returns>
        Task CloseSerialPorts();

        /// <summary>
        /// Calibrates the Bat and sets the BatPosition back to default
        /// </summary>
        /// <param name="sender">sender of the Event</param>
        /// <param name="e">Event arguments</param>
        void OnGoalDetected(object sender, DetectedGoalEventArgs e);
    }
}
