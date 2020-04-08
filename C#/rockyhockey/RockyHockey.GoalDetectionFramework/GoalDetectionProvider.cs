using RockyHockey.Common;
using RockyHockey.SoundFramework;
using System;
using System.Threading.Tasks;

namespace RockyHockey.GoalDetectionFramework
{
    /// <summary>
    /// Class that can detect if the puck hit a goal
    /// </summary>
    public class GoalDetectionProvider
    {
        private GoalDetectionProvider()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                DetectedGoalEvent += OnDetectedGoal;
                // Roboter goal
                pin1 = new GPIOPinDriver(Pin.GPIO5, GPIODirection.In);
                // Human goal
                pin2 = new GPIOPinDriver(Pin.GPIO6, GPIODirection.In);
            }
        }

        private static GoalDetectionProvider instance;

        private GPIOPinDriver pin1;

        private GPIOPinDriver pin2;

        /// <summary>
        /// Score of the running RockyHockey game
        /// </summary>
        public Score GameScore { get; set; }

        /// <summary>
        /// Determines if the GoalDetectionProvider should keep detecting goals
        /// </summary>
        public bool DetectGoals { get; set; }

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static GoalDetectionProvider Instance => instance ?? (instance = new GoalDetectionProvider());

        /// <summary>
        /// Event that is fired when a goal has been detected
        /// </summary>
        public event EventHandler<DetectedGoalEventArgs> DetectedGoalEvent;

        /// <summary>
        /// Starts the goal Detection
        /// </summary>
        public async void StartGoalDetection()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                DetectGoals = true;
                await RecognitionLoop().ConfigureAwait(false);
            }
        }
    
        private Task RecognitionLoop()
        {
            return Task.Factory.StartNew(() =>
            {
                while (DetectGoals)
                {
                    if (pin1.State.ToString() == "High")
                    {
                        CallDetectedGoalEvent(false);
                        while (pin1.State.ToString() == "High") { }
                        // Cooldown
                        Task.Delay(2000).Wait();
                    }
                    if (pin2.State.ToString() == "High")
                    {
                        CallDetectedGoalEvent(true);
                        while (pin2.State.ToString() == "High") { }
                        // Cooldown
                        Task.Delay(2000).Wait();
                    }
                }
            });
        }

        /// <summary>
        /// Unexports the used Pins
        /// </summary>
        public void UnexportPins()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                pin1.Unexport();
                pin2.Unexport();
                pin1.Dispose();
                pin2.Dispose();
            }
        }

        private void CallDetectedGoalEvent(bool humanGoal)
        {
            DetectedGoalEvent?.Invoke(this, new DetectedGoalEventArgs(humanGoal));
        }

        private async void OnDetectedGoal(object sender, DetectedGoalEventArgs e)
        {
            await LEDController.Instance.DoGoalLEDShow().ConfigureAwait(false);
            if (GameScore != null)
            {
                if (e.HumanGoal)
                {
                    GameScore.HumanGoals++;
                }
                else
                {
                    GameScore.RobotGoals++;
                }
                if (GameScore.HumanGoals < 10 && GameScore.RobotGoals < 10)
                {
                    await GoalSoundPlayer.Instance.OnDetectedGoal(GameScore).ConfigureAwait(false);
                }
            }
        }
    }
}
