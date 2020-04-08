using RockyHockey.Common;
using RockyHockey.GoalDetectionFramework;
using System.ComponentModel;
using System.Windows.Forms;

namespace RockyHockeyGUI
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
            score = GoalDetectionProvider.Instance.GameScore;
            score.PropertyChanged += OnPropertyChanged;
        }

        private Score score;

        private Label display;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            display.Text = $"score: {score.RobotGoals}:{score.HumanGoals}";
        }
    }
}
