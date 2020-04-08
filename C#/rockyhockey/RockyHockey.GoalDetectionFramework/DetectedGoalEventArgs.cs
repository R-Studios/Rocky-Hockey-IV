using System;

namespace RockyHockey.GoalDetectionFramework
{
    /// <summary>
    /// EventArgs for the DetectedGoalEvent
    /// </summary>
    public class DetectedGoalEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance of the DetectedGoalEventArgs
        /// </summary>
        /// <param name="humanGoal">true if the human shot the goal</param>
        public DetectedGoalEventArgs(bool humanGoal)
        {
            HumanGoal = humanGoal;
        }

        /// <summary>
        /// Determines if the human or the robot shot the goal
        /// </summary>
        public bool HumanGoal { get; set; }
    }
}
