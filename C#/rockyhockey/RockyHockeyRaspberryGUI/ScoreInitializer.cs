using RockyHockey.Common;
using RockyHockey.GoalDetectionFramework;
using System.ComponentModel;
using System.Windows.Forms;

namespace RockyHockeyRaspberryGUI
{
    /// <summary>
    /// Handles the logic to display the score on the GUI
    /// </summary>
    public class ScoreInitializer
    {
        /// <summary>
        /// Constructs a new instance of the ScoreInitializer
        /// </summary>
        /// <param name="display">Label to display the score</param>
        public ScoreInitializer(Label display)
        {
            this.display = display;
            GoalDetectionProvider.Instance.GameScore = new Score();
            Score = GoalDetectionProvider.Instance.GameScore;
        }

        private Label display;

        /// <summary>
        /// Score of the running RockyHockey-game
        /// </summary>
        public Score Score { get; private set; }
    }
}
