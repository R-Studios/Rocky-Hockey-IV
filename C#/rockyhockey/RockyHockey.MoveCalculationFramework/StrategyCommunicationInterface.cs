using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using RockyHockey.MovementFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    interface StrategyCommunicationInterface
    {
        /// <summary>
        /// sets 
        /// </summary>
        /// <param name="movementController"></param>
        /// <param name="prediction"></param>
        /// <param name="x"></param>
        void init(IMovementController movementController, PathPrediction prediction, double x);

        /// <summary>
        /// starts calculating strategie in own task; returns task so that the end can be awaited
        /// </summary>
        /// <returns></returns>
        Task startCalculation();

        /// <summary>
        /// checks if enough time is left to execute strategie; e.g. calc timestamp when execution has to start, compare to current timestamp
        /// </summary>
        /// <returns></returns>
        bool enoughTimeLeft();

        /// <summary>
        /// executes the strategie; moves bat to initial hit position; executes move to hit position
        /// </summary>
        void execute();
    }
}
