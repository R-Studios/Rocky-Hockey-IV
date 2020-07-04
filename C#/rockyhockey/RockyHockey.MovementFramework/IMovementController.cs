using RockyHockey.Common;
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
        Coordinate BatPosition { get; }

        /// <summary>
        /// Initializes the serial Ports
        /// </summary>
        /// <returns>executeable Task</returns>
        void InitializeSerialPorts();

        /// <summary>
        /// Bewegt den Schläger in die Richtung des Vektors
        /// </summary>
        /// <param name="vecList">Vectors to move</param>
        /// <param name="delayBeforePunch">delay time before the punch in milliseconds</param>
        /// <returns></returns>
        void MoveStrategy(IEnumerable<TimedVector> vecList, int delayBeforePunch);

        /// <summary>
        /// moves bat to given position
        /// </summary>
        /// <param name="pos"></param>
        void Move(Coordinate pos);

        /// <summary>
        /// Closes the serial Ports
        /// </summary>
        /// <returns>executeable Task</returns>
        Task CloseSerialPorts();
    }
}
