using System.ComponentModel;

namespace RockyHockey.Common
{
    /// <summary>
    /// Score of the actual game
    /// </summary>
    public class Score : INotifyPropertyChanged
    {
        private int humanGoals;

        /// <summary>
        /// Goals of the human player
        /// </summary>
        public int HumanGoals
        {
            get => humanGoals;
            set
            {
                humanGoals = value;
                CallPropertyChanged(nameof(humanGoals));
            }
        }

        private int robotGoals;

        /// <summary>
        /// Goals of the robot player
        /// </summary>
        public int RobotGoals
        {
            get => robotGoals;
            set
            {
                robotGoals = value;
                CallPropertyChanged(nameof(robotGoals));
            }
        }

        /// <summary>
        /// PropertyChangedEvent
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void CallPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
