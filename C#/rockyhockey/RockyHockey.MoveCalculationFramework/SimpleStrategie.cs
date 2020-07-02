using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using RockyHockey.MovementFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    class SimpleStrategie : StrategyCommunicationInterface
    {
        private double xCoordinate = 0;
        PathPrediction prediction;
        IMovementController movementController;
        List<TimedVector> batMotionVectors;

        bool calculationFinished = false;

        /// <summary>
        /// checks if enough time is left to use a strategy or just to block
        /// </summary>
        /// <returns>true if a strategy is faster than the remaining time /returns>
        public bool enoughTimeLeft()
        {
            return calculationFinished && batMotionVectors.First().TimedStart.Timestamp - DateTimeOffset.Now.ToUnixTimeMilliseconds() > 0;
        }

        /// <summary>
        /// Works out the given Strategy; moves the bat to the calculated hitpoint given by the strategy
        /// </summary>
        public void execute()
        {
            movementController.MoveStrategy(batMotionVectors, 0);
        }

        /// <summary>
        /// initalizes the parameters
        /// </summary>
        /// <param name="movementController"> Instance of the movementController</param>
        /// <param name="prediction">the trajectory of the puck</param>
        /// <param name="x">Coordinate to set where the Puck should be hit in a coutnerattack</param>
        public void init(IMovementController movementController, PathPrediction prediction, double x)
        {
            xCoordinate = x;
            this.prediction = prediction;
            this.movementController = movementController;
        }

        /// <summary>
        /// starts the calculation different counterattacks
        /// </summary>
        /// <returns></returns>
        public Task startCalculation()
        {
            return Task.Factory.StartNew(calculate);
        }

        /// <summary>
        /// calculates different strategy based on the pucks trajectory
        /// </summary>
        private void calculate()
        {
            batMotionVectors = new List<TimedVector>();

            //get motion of puck
            TimedVector oldPath = prediction.getTimeForPosition(xCoordinate);


            Random rnd = new Random();

            //determine nuzmber of hits on the bank
            int bankHits = rnd.Next() % 4;

            //select position where the puck should be played to
            Coordinate target = new Coordinate(Config.Instance.GameFieldSize.Width, rnd.Next() % Config.Instance.GameFieldSize.Height);

            //calculate motion after bat hit puck
            Vector newPath = DirectionForStrategy.getPuckDirection(oldPath.End, target, bankHits, rnd.Next() % 2 == 1);


            //claculate needed motion direction of bat
            Coordinate batVector = new Coordinate
            {
                X = newPath.VectorDirection.X - oldPath.VectorDirection.X,
                Y = newPath.VectorDirection.Y - oldPath.VectorDirection.Y
            };

            //create motion line for bat
            StraightLine motionLine = new StraightLine(oldPath.End, batVector);

            //select start position for bat
            Coordinate initPos = motionLine.previousImpact();

            //calculate when bat must be at initial position
            TimedCoordinate initialPosition = new TimedCoordinate(initPos, oldPath.TimedEnd.Timestamp - MovementDurationCalculator.getMovementDuration(initPos, oldPath.End));

            //current position of bat
            TimedCoordinate currentPosition = new TimedCoordinate(movementController.BatPosition, initialPosition.Timestamp - MovementDurationCalculator.getMovementDuration(movementController.BatPosition, initialPosition));


            batMotionVectors.Add(new TimedVector(currentPosition, initialPosition));
            batMotionVectors.Add(new TimedVector(initialPosition, oldPath.TimedEnd));

            calculationFinished = true;
        }
    }
}
