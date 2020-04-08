using RockyHockey.GoalDetectionFramework;
using RockyHockey.SoundFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockyHockeyRaspberryGUI
{
    /// <summary>
    /// Raspberry UI
    /// </summary>
    public partial class MainView : Form
    {
        /// <summary>
        /// Creates a new Instance of the Form
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            try
            {
                StopButton.Enabled = false;

                var timer = new Timer { Interval = 50 };
                timer.Tick += TimerTick;
                timer.Start();
            }
            catch (Exception ex)
            {
                var initException = new Exception("Error while initializing GUI", ex);
                MsgBoxLogger?.Log(ex).Wait();
                MsgBoxLogger.Show();
            }
        }

        private Stopwatch stopwatch;

        private ScoreInitializer socreInitializer;

        /// <summary>
        /// Logger for displaying a MessageBox
        /// </summary>
        public MessageBoxLogger MsgBoxLogger { get; } = new MessageBoxLogger();

        private async void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                await LEDController.Instance.DoStartLEDShow();
                SoundEffectsPlayer.PlaySound("Start.wav");
                socreInitializer = new ScoreInitializer(ScoreLabel);
                StartButton.Enabled = false;
                StopButton.Enabled = true;
                GoalDetectionProvider.Instance.StartGoalDetection();
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }
            catch (Exception ex)
            {
                await MsgBoxLogger?.Log(ex);
                MsgBoxLogger?.Show();
            }
        }

        private async void StopButton_Click(object sender, EventArgs e)
        {
            await StopRockyHockeyGame();
        }

        /// <summary>
        /// Refreshes the GUI When the timer Ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TimerTick(object sender, EventArgs e)
        {
            if (stopwatch != null)
            {
                TimeLabel.Text = $"Time: {stopwatch.Elapsed.ToString().Split('.').FirstOrDefault()}";
                ScoreLabel.Text = $"score: {socreInitializer.Score.RobotGoals} : {socreInitializer.Score.HumanGoals}";
                if (stopwatch.Elapsed.Minutes >= 5 || socreInitializer.Score.HumanGoals >= 10 || socreInitializer.Score.RobotGoals >= 10)
                {
                    await StopRockyHockeyGame();
                }
            }
            Refresh();
        }

        private async Task StopRockyHockeyGame()
        {
            try
            {
                GoalDetectionProvider.Instance.DetectGoals = false;
                stopwatch.Stop();
                stopwatch = null;
                StartButton.Enabled = true;
                StopButton.Enabled = false;
                await LEDController.Instance.DoStopLEDShow();
                await SoundtrackAndFinishSoundPlayer.Instance.PlayGameFinishSound(socreInitializer.Score).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await MsgBoxLogger?.Log(ex);
                MsgBoxLogger.Show();
            }
        }
    }
}
