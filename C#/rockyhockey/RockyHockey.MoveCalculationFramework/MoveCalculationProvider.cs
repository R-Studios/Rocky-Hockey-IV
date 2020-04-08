using RockyHockey.Common;
using RockyHockey.GoalDetectionFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    /// <summary>
    /// Implementierung des MoveCalculationProvider
    /// </summary>
    public class MoveCalculationProvider : IMoveCalculationProvider
    {
        /// <summary>
        /// private Field, which contains an Instance from TrajectoryCalculationFramework.
        /// The Initialization executes just once.
        /// </summary>
        private TrajectoryCalculationFramework trajectoryCalculationFramework;

        /// <summary>
        /// Method, to start the caculation-loop in the TrajectoryCalculationFramework class
        /// </summary>
        /// <returns>executeable Task</returns>
        public async Task StartCalculation(IProgress<IEnumerable<IEnumerable<Vector>>> progress)
        {
            trajectoryCalculationFramework = new TrajectoryCalculationFramework();
            await trajectoryCalculationFramework.BeginCalculationLoop(progress).ConfigureAwait(false);
        }

        /// <summary>
        /// Method, to stop the caculationloop in the TrajectoryCalculationFramework class
        /// </summary>
        /// <returns>executeable Task</returns>
        public Task StopCalculation()
        {
            return Task.Factory.StartNew(() =>
            {
                if (trajectoryCalculationFramework.KeepPlaying == true)
                {
                    trajectoryCalculationFramework.KeepPlaying = false;
                    GoalDetectionProvider.Instance.DetectGoals = false;
                    while (trajectoryCalculationFramework.KeepPlaying == false) { }
                    trajectoryCalculationFramework.StopAllUsedFrameworks().Wait();
                }
            });
        }
    }
}
