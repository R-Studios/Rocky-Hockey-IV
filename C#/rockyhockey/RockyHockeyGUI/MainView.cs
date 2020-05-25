using OxyPlot;
using OxyPlot.Series;
using RockyHockey.Common;
using RockyHockey.GoalDetectionFramework;
using RockyHockey.MoveCalculationFramework;
using RockyHockey.MovementFramework;
using RockyHockey.SoundFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RockyHockeyGUI
{
    /// <summary>
    /// Windows Form for drawing the graph
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
                CalibrateButton.Enabled = false;

                myModel = new PlotModel();
                var timer = new System.Windows.Forms.Timer { Interval = 50 };
                timer.Tick += TimerTick;
                timer.Start();

                PlotView.Model = myModel;
                InitializeGameField();
            }
            catch (Exception ex)
            {
                var initException = new Exception("Error while initializing GUI", ex);
                MsgBoxLogger?.Log(ex);
                MsgBoxLogger.Show();
            }
        }

        private PlotModel myModel;

        private IProgress<IEnumerable<IEnumerable<Vector>>> progress;

        private OptionsView optionsView;

        private Stopwatch stopwatch;

        private ScoreInitializer socreInitializer;

        private IMoveCalculationProvider moveCalculationProvider;

        /// <summary>
        /// Logger for displaying a MessageBox
        /// </summary>
        public MessageBoxLogger MsgBoxLogger { get; } = new MessageBoxLogger();

        /// <summary>
        /// Trajectory points that will be drawed
        /// </summary>
        public LineSeries TrajectoryLine { get; set; }

        /// <summary>
        /// Punch points that will be drawed
        /// </summary>
        public LineSeries PunchLine { get; set; }

        /// <summary>
        /// Border points that will be drawed
        /// </summary>
        public LineSeries BorderLine { get; set; }

        /// <summary>
        /// Starts the calculation of the Rocky Hockey game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                await LEDController.Instance.DoStartLEDShow();
                moveCalculationProvider = new MoveCalculationProvider();

                socreInitializer = new ScoreInitializer(ScoreLabel);
                if (optionsView != null)
                {
                    optionsView.Close();
                }

                OptionsButton.Enabled = false;
                StartButton.Enabled = false;
                StopButton.Enabled = true;
                CalibrateButton.Enabled = true;

                StartGameTime();

                GoalDetectionProvider.Instance.StartGoalDetection();
                await moveCalculationProvider.StartCalculation(progress).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                MsgBoxLogger?.Log(ex);
                MsgBoxLogger?.Show();
            }
        }

        /// <summary>
        /// Initializes all private variables and draws the border
        /// </summary>
        private void InitializeGameField()
        {
            progress = new Progress<IEnumerable<IEnumerable<Vector>>>(async t =>
            {
                await CreatePoints(t);
                Draw();
            });

            BorderLine = new LineSeries { Color = OxyColors.Black };
            TrajectoryLine = new LineSeries { Color = OxyColors.Blue };
            PunchLine = new LineSeries { Color = OxyColors.Red };

            // Draw the game Field
            BorderLine.Points.Add(new OxyPlot.DataPoint(0, 0));
            BorderLine.Points.Add(new OxyPlot.DataPoint(Config.Instance.GameFieldSize.Width, 0));
            BorderLine.Points.Add(new OxyPlot.DataPoint(Config.Instance.GameFieldSize.Width, Config.Instance.GameFieldSize.Height));
            BorderLine.Points.Add(new OxyPlot.DataPoint(0, Config.Instance.GameFieldSize.Height));
            BorderLine.Points.Add(new OxyPlot.DataPoint(0, 0));
            Draw();
        }

        /// <summary>
        /// Creates the Points to draw from the surpassed vectors
        /// </summary>
        /// <param name="progress">Progress that needs to be reported to the UI</param>
        private Task CreatePoints(IEnumerable<IEnumerable<Vector>> progress)
        {
            return Task.Factory.StartNew(() =>
            {
                IEnumerable<Vector> vectors = new List<Vector>();
                LineSeries line = null;
                for (int i = 0; i < progress.Count(); i++)
                {
                    if (i == 0)
                    {
                        line = TrajectoryLine;
                    }
                    else if (i == 1)
                    {
                        line = PunchLine;
                    }
                    else
                    {
                        continue;
                    }

                    vectors = progress.ElementAt(i);
                    line.Points.Clear();
                    if (vectors != null && vectors.Any())
                    {
                        line.Points.Add(new OxyPlot.DataPoint(Convert.ToInt32(vectors.First().Position.X),
                            Convert.ToInt32(vectors.First().Position.Y)));
                        foreach (Vector vec in vectors)
                        {
                            line.Points.Add(new OxyPlot.DataPoint(Convert.ToInt32(vec.Direction.X),
                                Convert.ToInt32(vec.Direction.Y)));
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Stops the execution
        /// </summary>
        /// <returns></returns>
        private async void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                await moveCalculationProvider.StopCalculation();
                stopwatch.Stop();

                OptionsButton.Enabled = true;
                StartButton.Enabled = true;
                StopButton.Enabled = false;
                CalibrateButton.Enabled = false;
            }
            catch (Exception ex)
            {
                var startException = new Exception("Error while stopping RockyHockey", ex);
                MsgBoxLogger?.Log(startException);
                MsgBoxLogger.Show();
            }
        }

        /// <summary>
        /// Draws the new Graph
        /// </summary>
        private void Draw()
        {
            myModel.Series.Clear();
            myModel.Series.Add(BorderLine);
            myModel.Series.Add(TrajectoryLine);
            myModel.Series.Add(PunchLine);
        }

        /// <summary>
        /// Refreshes the GUI When the timer Ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            if (stopwatch != null)
            {
                GameTimeLabel.Text = $"Game time: {stopwatch.Elapsed.ToString().Split('.').FirstOrDefault()}";
            }
            Refresh();
        }

        private void StartGameTime()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            if (optionsView == null)
            {
                optionsView = new OptionsView();
                optionsView.FormClosed += OnOptionsClosed;
            }
            optionsView.Show();
        }

        private void OnOptionsClosed(object sender, EventArgs e)
        {
            optionsView.Dispose();
            optionsView = null;
        }

        private void CalibrateButton_Click(object sender, EventArgs e)
        {
            MovementController.Instance.OnGoalDetected(this, new DetectedGoalEventArgs(true));
        }
    }
}
