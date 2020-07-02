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

        public bool enoughTimeLeft()
        {
            return calculationFinished && batMotionVectors.First().TimedStart.Timestamp - DateTimeOffset.Now.ToUnixTimeMilliseconds() > 0;
        }

        public void execute()
        {
            movementController.MoveStrategy(batMotionVectors, 0);
        }

        public void init(IMovementController movementController, PathPrediction prediction, double x)
        {
            xCoordinate = x;
            this.prediction = prediction;
            this.movementController = movementController;
        }

        public Task startCalculation()
        {
            return Task.Factory.StartNew(calculate);
        }

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
