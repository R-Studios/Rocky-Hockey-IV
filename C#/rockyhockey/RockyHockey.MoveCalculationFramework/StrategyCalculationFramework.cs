using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using RockyHockey.Common;
using RockyHockey.MovementFramework;

namespace RockyHockey.MoveCalculationFramework
{
    /// <summary>
    /// Class that calculates the way the bat has to move depending on different strategies
    /// </summary>
    public class StrategyCalculationFramework : IStrategyCalculationFramework
    {
        private readonly GameFieldPosition goalPosition;

        private GameFieldPosition batPosition;

        private readonly IMovementController movementController;

        private readonly double maxVelocity = Config.Instance.MaxBatVelocity;

        private readonly Size gameFieldSize = Config.Instance.GameFieldSize;

        private readonly GameFieldPosition batRestPosition;

        private readonly double impactAxisPosition = Config.Instance.ImaginaryAxePosition;

        /// <summary>
        /// Constructs a new instance of the StrategyCalculationFramework
        /// </summary>
        /// <param name="movementController">Instance of the movementController</param>
        /// <param name="batRestPosition">default position of the bat</param>
        public StrategyCalculationFramework(IMovementController movementController, GameFieldPosition batRestPosition)
        {
            this.movementController = movementController;
            this.batRestPosition = batRestPosition;
            goalPosition = CalculateGoalPosition(gameFieldSize);
        }

        /// <summary>
        /// Method to get that calculates a way for the bat and send the calculated vectors to the movementController
        /// </summary>
        /// <param name="impactPosition">Position where the puck will be played</param>
        /// <param name="timeLeft">The time which is left to move the bat to the impactPosition</param>
        /// <param name="angle">Angle in which the puck will fly to the impactPosition</param>
        /// <returns></returns>
        public async Task<IEnumerable<Vector>> CalculateStartegy(GameFieldPosition impactPosition, double timeLeft, double angle)
        {
            IEnumerable<VelocityVector> batTrajectory = null;
            batPosition = movementController.BatPosition;
            batTrajectory = await CalculateDirectPunch(impactPosition).ConfigureAwait(false);
            await movementController.MoveStrategy(batTrajectory, 0);
            return batTrajectory;
            /*
            Vector impactVector = await CalculateGoalTrajectory(impactPosition).ConfigureAwait(false);
            double pitchImpactVector = await impactVector.GetVectorGradient().ConfigureAwait(false);

            GameFieldPosition midpointCircle = await CalculateMidpointCircle(pitchImpactVector, impactPosition).ConfigureAwait(false);
            Vector radiusVector = new Vector { Position = impactPosition, Direction = midpointCircle };
            double radiusCircle = await radiusVector.GetVectorLength().ConfigureAwait(false);

            if (batPosition.X > impactAxisPosition - 1 && batPosition.X < impactAxisPosition + 1)
            {
                batTrajectory = await CalculateDirectPunch(impactPosition).ConfigureAwait(false);
            }
            else
            {
                batTrajectory =
                await CalculateBatTrajectory(midpointCircle, radiusCircle, pitchImpactVector, impactPosition.Y, angle).ConfigureAwait(false);
            }

            int delayTime = Convert.ToInt32(await CalculateDelayTime(batTrajectory, timeLeft).ConfigureAwait(false));

            if (!await IsOutOfGamefield(batTrajectory).ConfigureAwait(false) &&
                delayTime > 0)
            {
                await movementController.MoveStrategy(batTrajectory, delayTime);
                return batTrajectory;
            }
            else
            {
                batTrajectory = await CalculateDirectPunch(impactPosition);
                int delayBeforePunch = Convert.ToInt32(delayTime);
                await movementController.MoveStrategy(batTrajectory, delayBeforePunch);
                return batTrajectory;
            }//*/
        }

        /// <summary>
        /// Method to calculate the vector from the impactPosition to the goal
        /// </summary>
        /// <param name="impactPosition">Position where the puck will be played</param>
        /// <returns>First vector of the trajectory from the impact point to the goal</returns>
        private Task<Vector> CalculateGoalTrajectory(GameFieldPosition impactPosition)
        {
            Random rnd = new Random();
            int numberStrategy = rnd.Next(0, 2);

            if (numberStrategy == 0)
            {
                StrategyDirect strategyDirect = new StrategyDirect();
                return strategyDirect.getTangent(impactPosition, goalPosition);
            }
            else
            {
                StrategyOneBank strategyOneBank = new StrategyOneBank(gameFieldSize);
                return strategyOneBank.getTangent(impactPosition, goalPosition);
            }
        }

        /// <summary>
        /// Method to calculate the midpoint of the goal
        /// </summary>
        /// <param name="gameFieldSize">Size of the game field</param>
        /// <returns>The midpoint of the goal</returns>
        private GameFieldPosition CalculateGoalPosition(Size gameFieldSize)
        {
            return new GameFieldPosition { X = 0, Y = gameFieldSize.Height / 2 };
        }

        /// <summary>
        /// Method to calculate the midpoint of the circle on which the bat will move
        /// </summary>
        /// <param name="pitchImpactVector">The pitch of the vector to the goal</param>
        /// <param name="impactPosition">Position where the puck will be played</param>
        /// <returns>Midpoint of the circle on which the trajectory is</returns>
        private Task<GameFieldPosition> CalculateMidpointCircle(double pitchImpactVector,
            GameFieldPosition impactPosition)
        {
            return Task.Factory.StartNew(() =>
            {
                // get the pitch of the vector which is vertical to the vector how the puck will be played
                double pitchVerticalVector = -1 / pitchImpactVector;
                // get the axis section of the vertical vector
                double axisSectionVerticalVector = impactPosition.Y - pitchVerticalVector * impactPosition.X;
                // how this formula was developed:
                // we know two points on the circle and the vertical vector of the the tangent where the puck will be played. The midpoint of the circle is on the vertical vector
                // with the formula for a circle (x - m1) * (y - m2) = r^2 we can build two formuals with the midpoint and the radius unknown
                // because the midpoint and the radius are the same in both of the formulas we can set them equal and solve this to the y-position of the midpoint
                // with the pitch and the axis section of the vertical vector we can build the formula for a line
                // we can set the the esolved formula for the circle and the formula for the line equal because the midpoint is a point on the line
                // solve this formula to the x-position of the midpoint
                double xCoordinateMidpoint = (-2 * axisSectionVerticalVector * batPosition.Y +
                    2 * axisSectionVerticalVector * impactPosition.Y + Math.Pow(batPosition.X, 2) +
                    Math.Pow(batPosition.Y, 2) - Math.Pow(impactPosition.X, 2) - Math.Pow(impactPosition.Y, 2)) /
                    (2 * pitchVerticalVector * batPosition.Y - 2 * pitchVerticalVector * impactPosition.Y +
                    2 * batPosition.X - 2 * impactPosition.X);
                // calculate the y-position of the midpoint with the formula of the vertical vector and the calculated x-position
                double yCoordinateMidpoint = pitchVerticalVector * xCoordinateMidpoint + axisSectionVerticalVector;

                return new GameFieldPosition { X = xCoordinateMidpoint, Y = yCoordinateMidpoint };
            });
        }

        /// <summary>
        /// Calculates number of velocityVectors for the movementController
        /// </summary>
        /// <param name="midpointCircle">Midpoint of the circle on which the trajectory is</param>
        /// <param name="radiusCircle">Radius of the circle on which the trajectory is</param>
        /// <param name="pitchImpactVector">The pitch of the vector from the impact point to the goal</param>
        /// <param name="impactHeight">The Y Coordinate of the impactPosition</param>
        /// <param name="angle">angle of the puck at impact</param>
        /// <returns>List of velocityVectors</returns>
        private async Task<IEnumerable<VelocityVector>> CalculateBatTrajectory(GameFieldPosition midpointCircle,
            double radiusCircle, double pitchImpactVector, double impactHeight, double angle)
        {
            int numberVectors = 5;
            GameFieldPosition position = batPosition;
            List<VelocityVector> listBatTrajectory = new List<VelocityVector>();
            double distanceBetweenTrajectoryVectorX = (batPosition.X - impactAxisPosition) / numberVectors;
            char sign = '+';

            for (double i = batPosition.X - distanceBetweenTrajectoryVectorX; i > impactAxisPosition - distanceBetweenTrajectoryVectorX;
                i -= distanceBetweenTrajectoryVectorX)
            {
                // Offset describes an area around the middle of the height of the gamefield where the puck must be played different
                double offset = 20;
                double yDirection = 0;
                // Right half of the gamefield with the view of the robot
                if (impactHeight < goalPosition.Y - offset)
                {
                    yDirection = await CalculateYDirection(midpointCircle, radiusCircle, '+', i);
                    sign = '+';
                }
                else if (goalPosition.Y > impactHeight && impactHeight > goalPosition.Y - offset)
                {
                    // When the impact position is around the goal it depends if the puck is 
                    // played over the left or the right bank which side of the circle must be calculated
                    if (angle < 90.0 && angle > 0.0)
                    {
                        yDirection = await CalculateYDirection(midpointCircle, radiusCircle, '-', i);
                        sign = '-';
                    }
                    else
                    {
                        yDirection = await CalculateYDirection(midpointCircle, radiusCircle, '+', i);
                        sign = '+';
                    }
                }
                // Left half og the gamefield with the view of the robot
                else if (impactHeight > goalPosition.Y + offset)
                {
                    yDirection = await CalculateYDirection(midpointCircle, radiusCircle, '-', i);
                    sign = '-';
                }
                else
                {
                    // When the impact position is around the goal it depends if the puck is 
                    // played over the left or the right bank which side of the circle must be calculated
                    if (angle < 90.0 && angle > 0.0)
                    {
                        yDirection = await CalculateYDirection(midpointCircle, radiusCircle, '-', i);
                        sign = '-';
                    }
                    else
                    {
                        yDirection = await CalculateYDirection(midpointCircle, radiusCircle, '+', i);
                        sign = '+';
                    }
                }

                GameFieldPosition direction = new GameFieldPosition { X = i, Y = yDirection };

                Vector trajectoryVector = new Vector { Position = position, Direction = direction };
                double velocity = maxVelocity;//await trajectoryVector.GetVectorLength().ConfigureAwait(false) / (timeLeft / numberVectors);

                listBatTrajectory.Add(new VelocityVector { Velocity = velocity, Direction = direction, Position = position });
                position = direction;
            }

            // If the direction of the last vector is not the same as the impact position the other half of the circle must be calculated.
            // Because the coordinates of the impact position and the direction of the last vector will never be exactely the same an area is defined.
            if (listBatTrajectory[listBatTrajectory.Count - 1].Direction.Y - impactHeight > 2 || listBatTrajectory[listBatTrajectory.Count - 1].Direction.Y - impactHeight < -2)
            {
                position = batPosition;
                listBatTrajectory = new List<VelocityVector>();

                if (sign == '+')
                {
                    sign = '-';
                }
                else
                {
                    sign = '+';
                }

                for (double i = batRestPosition.X - distanceBetweenTrajectoryVectorX; i > impactAxisPosition - distanceBetweenTrajectoryVectorX; i -= distanceBetweenTrajectoryVectorX)
                {
                    double yDirection = await CalculateYDirection(midpointCircle, radiusCircle, sign, i); GameFieldPosition direction = new GameFieldPosition { X = i, Y = yDirection };

                    Vector trajectoryVector = new Vector { Position = position, Direction = direction };
                    double velocity = maxVelocity;
                    listBatTrajectory.Add(new VelocityVector { Velocity = velocity, Direction = direction, Position = position });
                    position = direction;
                }
            }

            return listBatTrajectory;
        }

        /// <summary>
        /// Method to calculate the y coordinate of a point of a vector on the circle on which the trajectory is
        /// </summary>
        /// <param name="midpointCircle">Midpoint of the circle on which the trajectory is</param>
        /// <param name="radiusCircle">Radius of the circle on which the trajectory is</param>
        /// <param name="sign">Sign of the pitch of the vector from the impact point to the goal</param>
        /// <param name="xDirection">X coordinate of a point of a vector on the circle on which the trajectory is</param>
        /// <returns>Y coordinate of a point of a vector on the circle on which the trajectory is</returns>
        private Task<double> CalculateYDirection(GameFieldPosition midpointCircle, double radiusCircle, char sign, double xDirection)
        {
            return Task.Factory.StartNew(() =>
            {

                if (sign == '+')
                {
                    return midpointCircle.Y - Math.Sqrt(Math.Pow(radiusCircle, 2) - Math.Pow((xDirection - midpointCircle.X), 2));
                }
                return midpointCircle.Y + Math.Sqrt(Math.Pow(radiusCircle, 2) - Math.Pow((xDirection - midpointCircle.X), 2));
            });
        }


        /// <summary>
        /// Method to calculate a single velocity vector from the bat position to the impact position
        /// </summary>
        /// <param name="impactPosition">Position where the puck will be played</param>
        /// <returns>List with a single velocity vector</returns>
        private Task<List<VelocityVector>> CalculateDirectPunch(GameFieldPosition impactPosition)
        {
            return Task.Factory.StartNew(() =>
            {
                return new List<VelocityVector>
                {
                    new VelocityVector
                    {
                        Position = batPosition,
                        Direction = impactPosition,
                        Velocity = maxVelocity
                    }
                };
            });
        }
        /// <summary>
        /// Method to check if the calculated trajectory of the bat will be out of the gamefield
        /// </summary>
        /// <param name="trajectoryVectors">List of velocity vectors</param>
        /// <returns>True if at least one velocity vector will be out of the gamefield</returns>
        private Task<bool> IsOutOfGamefield(IEnumerable<VelocityVector> trajectoryVectors)
        {
            return Task.Factory.StartNew(() =>
            {
                bool isOutOfGamefield = false;

                foreach (VelocityVector vector in trajectoryVectors)
                {
                    if (vector.Direction.Y < 0 || vector.Direction.Y > gameFieldSize.Height)
                    {
                        isOutOfGamefield = true;
                    }
                }

                return isOutOfGamefield;
            });
        }

        /// <summary>
        /// Calculates the time which will be needed to move to bat on the calculated trajectory
        /// </summary>
        /// <param name="batTrajectory">List with velocityVectors which describe the trajectory for the bat</param>
        /// <param name="timeLeft">The time which is left until the puck will be at the impact position</param>
        /// <returns>The difference of the timeLeft and the needed Time to move the bat</returns>
        private async Task<double> CalculateDelayTime(IEnumerable<VelocityVector> batTrajectory, double timeLeft)
        {
            double length = 0;

            foreach (VelocityVector vector in batTrajectory)
            {
                length += vector.GetVectorLength();
            }

            double neededTime = length / maxVelocity;

            return timeLeft - neededTime;
        }
    }
}
