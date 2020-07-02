using RockyHockey.Common;
using RockyHockey.MovementFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    public class StrategyManager
    {
        private IMovementController movementController;
        private PathPrediction prediction;
        private TimedCoordinate defense;

        private SimpleStrategie[] strategies;

        int distanceParts;

        public StrategyManager()
        {
            distanceParts = 10;
        }

        private void moveToDefensePosition()
        {
            defense = prediction.getTimeForPosition(Config.Instance.BatRadius + Config.Instance.Tolerance).TimedEnd;

            movementController.Move(defense);
        }

        public void calculate(IMovementController movementController, PathPrediction prediction)
        {
            this.movementController = movementController;
            this.prediction = prediction;

            moveToDefensePosition();

            if (createStrategies(2) == strategies.Length)
            {
                prediction.finalize();

                if (prediction.collector.GetPuckPositions() != null)
                {
                    prediction.init();
                    createStrategies(4);
                    prediction.finalize();
                }
            }
        }

        private int createStrategies(int distanceFactor)
        {
            int numberOfStrategies = 4;

            strategies = new SimpleStrategie[numberOfStrategies];
            Task[] strategieTasks = new Task[numberOfStrategies];

            for (int a = numberOfStrategies + distanceFactor - 1; a >= distanceFactor; a--)
            {
                SimpleStrategie strategie = new SimpleStrategie();
                strategies[a] = strategie;

                strategie.init(movementController, prediction, a * distanceParts);

                strategieTasks[a] = strategie.startCalculation();
            }

            Task.WaitAll(strategieTasks);

            int strategieIterator = 0;
            for (; strategieIterator < strategies.Length; strategieIterator++)
                if (strategies[strategieIterator].enoughTimeLeft())
                {
                    strategies[strategieIterator].execute();

                    break;
                }

            return strategieIterator;
        }
    }
}
