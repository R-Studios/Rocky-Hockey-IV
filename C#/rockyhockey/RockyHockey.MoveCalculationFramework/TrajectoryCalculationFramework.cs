using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using RockyHockey.MovementFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    /// <summary>
    /// Class that contains the calculationloop
    /// </summary>
    public class TrajectoryCalculationFramework
    {
        /// <summary>
        /// Constructs a new Instance of the TrajectoryCalculationFramework
        /// </summary>
        public TrajectoryCalculationFramework(PositionCollector overrideCollector = null)
        {
            motionCaptureProvider = overrideCollector ?? new ImagePositionCollector();
            gameFieldSize = Config.Instance.GameFieldSize;
            movementController = MovementController.Instance;
        }

        private readonly double maxVelocity = Config.Instance.MaxBatVelocity;

        private readonly double restPositionDivisor = Config.Instance.RestPositionDivisor;

        private double impactAxisPosition = Config.Instance.ImaginaryAxePosition;

        private readonly double impactPositionTolerance = Config.Instance.Tolerance;

        private readonly Size gameFieldSize;

        public PositionCollector motionCaptureProvider { get; protected set; }

        protected IMovementController movementController;

        /// <summary>
        /// Determines wether the game continuous or not
        /// </summary>
        public bool KeepPlaying { get; set; } = true;

        /// <summary>
        /// Method, to start the actual calculationloop
        /// </summary>
        public Task BeginCalculationLoop(IProgress<List<Vector>> progress)
        {
            return Task.Factory.StartNew(async () =>
            {
                PathPrediction prediction = new PathPrediction(motionCaptureProvider, progress);
                prediction.OnInit += prediction.reportProgress;

                StrategyManager manager = new StrategyManager(movementController, prediction);

                movementController.InitializeSerialPorts();

                prediction.init();

                KeepPlaying = true;
                while (KeepPlaying)
                {
                    if (prediction.towardsRobot())
                        manager.calculate();
                    else
                        await Task.Factory.StartNew(() => { Thread.Sleep(10); });
                }

                prediction.finalize();
            });
        }

        /// <summary>
        /// Closes all open resources
        /// </summary>
        /// <returns>executeable Task</returns>
        public async Task StopAllUsedFrameworks()
        {
            // Close all open resources
            motionCaptureProvider.StopMotionCapturing();
            await movementController.CloseSerialPorts().ConfigureAwait(false);
        }
    }
}
