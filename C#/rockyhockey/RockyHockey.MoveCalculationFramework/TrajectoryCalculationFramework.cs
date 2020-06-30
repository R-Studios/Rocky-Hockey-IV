using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using RockyHockey.MovementFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            vectorCalculationProvider = new VectorCalculationProvider(motionCaptureProvider);
            movementController = MovementController.Instance;
        }

        private readonly double maxVelocity = Config.Instance.MaxBatVelocity;

        private readonly double restPositionDivisor = Config.Instance.RestPositionDivisor;

        private double impactAxisPosition = Config.Instance.ImaginaryAxePosition;

        private readonly double impactPositionTolerance = Config.Instance.Tolerance;

        private readonly Size gameFieldSize;

        public PositionCollector motionCaptureProvider { get; protected set; }

        protected IMovementController movementController;

        private IStrategyCalculationFramework strategyCalculationFramework;

        protected VectorCalculationProvider vectorCalculationProvider;

        /// <summary>
        /// Determines wether the game continuous or not
        /// </summary>
        public bool KeepPlaying { get; set; } = true;

        /// <summary>
        /// Method, to start the actual calculationloop
        /// </summary>
        public async Task BeginCalculationLoop(IProgress<IEnumerable<IEnumerable<Vector>>> progress)
        {
            GameFieldPosition batRestPosition = await CalculateBatRestPosition(gameFieldSize).ConfigureAwait(false);
            GameFieldPosition oldImpactPosition = new GameFieldPosition { X = 0, Y = 0 };

            strategyCalculationFramework = new StrategyCalculationFramework(movementController, batRestPosition);

            movementController.InitializeSerialPorts();

            while (KeepPlaying)
            {
                VelocityVector puckVector;
                try
                {
                    //GameFieldPosition batPosition = await motionCaptureProvider.GetBatPosition().ConfigureAwait(false);
                    puckVector = await vectorCalculationProvider.CalculatePuckVector().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new Exception("PuckVector Calculation Error", ex);
                }

                if (puckVector.Position.X < puckVector.Direction.X)
                {
                    IEnumerable<Vector> trajectoryVectors = await CalculatePuckTrajectory(puckVector, gameFieldSize).ConfigureAwait(false);
                    double timeLeft = await CalculateNeededTime(trajectoryVectors, puckVector.Velocity).ConfigureAwait(false);
                    double pitch = trajectoryVectors.LastOrDefault().GetVectorGradient();
                    double angle = Math.Atan(pitch) * (180 / Math.PI);

                    GameFieldPosition impactPosition = trajectoryVectors.LastOrDefault().Direction;
                    if ((impactPosition.Y - oldImpactPosition.Y) > impactPositionTolerance || (impactPosition.Y - oldImpactPosition.Y) < -impactPositionTolerance)
                    {
                        List<IEnumerable<Vector>> progressReport;
                        try
                        {
                            progressReport = new List<IEnumerable<Vector>>
                            {
                                trajectoryVectors,
                                await strategyCalculationFramework.CalculateStartegy(impactPosition, timeLeft, angle).ConfigureAwait(false)
                            };
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Strategy Calculation Framework Fehler", ex);
                        }
                        progress?.Report(progressReport);
                    }
                    oldImpactPosition = impactPosition;
                }
                else
                {
                    await MoveBatToRestPosition(batRestPosition).ConfigureAwait(false);
                }
            }
            KeepPlaying = true;
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

        /// <summary>
        /// Calculates the impact position of the puck on the imaginary axis
        /// </summary>
        /// <param name="vec">Vector of the Puck</param>
        /// <param name="gameFieldSize">Size of the gamefield</param>
        /// <returns>Enumerable of Vectors, that describe the trajectory of the puck</returns>
        public async Task<IEnumerable<Vector>> CalculatePuckTrajectory(Vector vec, Size gameFieldSize)
        {
            // List for the trajectory of the Puck
            var calculatedVectors = new List<Vector>();
            Vector strechedVector = await vec.StretchVectorToXCoordinate(gameFieldSize.Height).ConfigureAwait(false);
            Vector notStrechedVector = vec;
            while (strechedVector.Direction.X < impactAxisPosition)
            {
                calculatedVectors.Add(strechedVector);
                notStrechedVector = strechedVector.CalculateReflectedVector();
                strechedVector = await notStrechedVector.StretchVectorToXCoordinate(gameFieldSize.Height).ConfigureAwait(false);
            }
            strechedVector = notStrechedVector.StretchVectorToYCoordinate(impactAxisPosition);
            calculatedVectors.Add(strechedVector);
            return calculatedVectors;
        }

        /// <summary>
        /// Calculates the ideal Position for the bat to rest till the enemy is about to hit the puck
        /// </summary>
        /// <param name="gameFieldSize">Size of picture from the cameras</param>
        /// <returns>position for the bat to rest</returns>
        private Task<GameFieldPosition> CalculateBatRestPosition(Size gameFieldSize)
        {
            return Task.Factory.StartNew(() =>
            {
                var batRestPosition = new GameFieldPosition
                {
                    X = gameFieldSize.Width - (gameFieldSize.Width / restPositionDivisor),
                    Y = gameFieldSize.Height / 2
                };
                return batRestPosition;
            });
        }

        /// <summary>
        /// Moves the bat to the restposition
        /// </summary>
        /// <param name="batRestPosition">Position in front of the goal</param>
        /// <returns>executeable Task</returns>
        private async Task MoveBatToRestPosition(GameFieldPosition batRestPosition)
        {
            // TODO: Schlägerposition der Kamera mit berücksichtigen
            var movementControllerBatPosition = movementController.BatPosition;
            if (movementControllerBatPosition != batRestPosition)
            {
                var movementVector = new VelocityVector
                {
                    Position = movementControllerBatPosition,
                    Direction = batRestPosition,
                    Velocity = maxVelocity
                };
                var vectorList = new List<VelocityVector> { movementVector };
                await movementController.MoveStrategy(vectorList, 0).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Calculates the needed time for the puck to fly the trajectoryVectors
        /// </summary>
        /// <param name="trajectoryVectors">trajectory of the puck</param>
        /// <param name="velocity">velocity of the puck in pixels per milliseconds</param>
        /// <returns>needed time for the puck to fly the trajectoryVectors</returns>
        public async Task<double> CalculateNeededTime(IEnumerable<Vector> trajectoryVectors, double velocity)
        {
            double length = 0;
            foreach (Vector vector in trajectoryVectors)
            {
                length += vector.GetVectorLength();
            }
            return (length / velocity);
        }
    }
}
