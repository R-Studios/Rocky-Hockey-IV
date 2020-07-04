using OxyPlot;
using OxyPlot.Series;
using RockyHockey.Common;
using RockyHockey.MoveCalculationFramework;
using RockyHockey.MovementFramework;
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
using RockyHockey.MotionCaptureFramework;
using RockyHockeyGUI.VirtualTable;

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
                ImageDebuggingButton.Enabled = false;

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

        private ImageDebugging debuggingWindow = null;
        private bool imageDebuggingActive = false;

        CameraCalibration cameraCalibration;

        CircleDetectionCalibration circleDetectionCalibration;

        private PlotModel myModel;

        private IProgress<List<Vector>> progress;

        private OptionsView optionsView;

        private Stopwatch stopwatch;
        
        private TrajectoryCalculationFramework trajectoryCalculationFramework;

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
        /// An alternative position collector to use, like a virtual table.
        /// </summary>
        public PositionCollector OverridePositionCollector { get; set; }

        /// <summary>
        /// Starts the calculation of the Rocky Hockey game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (optionsView != null)
                {
                    optionsView.Close();
                }

                OptionsButton.Enabled = false;
                StartButton.Enabled = false;
                StopButton.Enabled = true;
                CalibrateButton.Enabled = true;
                ImageDebuggingButton.Enabled = true;
                CameraCalibrationButton.Enabled = false;
                ImageProcessingCalibration_Button.Enabled = false;

                StartGameTime();
                
                trajectoryCalculationFramework = new TrajectoryCalculationFramework(OverridePositionCollector);
                await trajectoryCalculationFramework.BeginCalculationLoop(progress).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                MsgBoxLogger?.Log(ex);
                MsgBoxLogger?.Show();

                // Rethrow so the debugger can break on exceptions properly
                if (Debugger.IsAttached)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Initializes all private variables and draws the border
        /// </summary>
        private void InitializeGameField()
        {
            progress = new Progress<List<Vector>>(async t =>
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
        private Task CreatePoints(List<Vector> progress)
        {
            return Task.Factory.StartNew(() =>
            {
                TrajectoryLine.Points.Clear();
                if (progress.Any())
                {
                    TrajectoryLine.Points.Add(new OxyPlot.DataPoint(Convert.ToInt32(progress.First().Start.X),
                        Convert.ToInt32(progress.First().Start.Y)));
                    foreach (Vector vec in progress)
                    {
                        TrajectoryLine.Points.Add(new OxyPlot.DataPoint(Convert.ToInt32(vec.End.X),
                            Convert.ToInt32(vec.End.Y)));
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
                await Task.Factory.StartNew(() =>
                {
                    if (trajectoryCalculationFramework.KeepPlaying)
                    {
                        trajectoryCalculationFramework.KeepPlaying = false;
                        while (trajectoryCalculationFramework.KeepPlaying == false) { }
                        trajectoryCalculationFramework.StopAllUsedFrameworks().Wait();
                    }
                });
                
                stopwatch.Stop();

                debuggingWindow?.Close();

                OptionsButton.Enabled = true;
                StartButton.Enabled = true;
                StopButton.Enabled = false;
                CalibrateButton.Enabled = false;
                ImageDebuggingButton.Enabled = false;
                CameraCalibrationButton.Enabled = true;
                ImageProcessingCalibration_Button.Enabled = true;
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
        private async void TimerTick(object sender, EventArgs e)
        {
            if (stopwatch != null)
            {
                GameTimeLabel.Text = $"Game time: {stopwatch.Elapsed.ToString().Split('.').FirstOrDefault()}";
            }

            pictureBox1.Image = trajectoryCalculationFramework?.motionCaptureProvider.imageProvider?.lastCapture.GetImage();
            if (imageDebuggingActive)
                debuggingWindow?.displayImage(trajectoryCalculationFramework?.motionCaptureProvider.imageProvider.lastCapture);

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
            MovementController.Instance.init();
        }

        private void ImageDebuggingButton_Click(object sender, EventArgs e)
        {
            ImageDebuggingButton.Enabled = false;
            debuggingWindow = new ImageDebugging();
            debuggingWindow.FormClosed += debuggingWindow_FormClosed;
            debuggingWindow.Show();
            imageDebuggingActive = true;
        }

        private void debuggingWindow_FormClosed(object sender, EventArgs e)
        {
            imageDebuggingActive = false;
            debuggingWindow.Dispose();
            debuggingWindow = null;
            ImageDebuggingButton.Enabled = true;
        }

        private void CameraCalibrationButton_Click(object sender, EventArgs e)
        {
            CameraCalibrationButton.Enabled = false;
            ImageProcessingCalibration_Button.Enabled = false;
            StartButton.Enabled = false;
            CalibrateButton.Enabled = false;

            cameraCalibration = new CameraCalibration();
            cameraCalibration.FormClosed += cameraCalibrationWindow_FormClosed;
            cameraCalibration.Show();
        }

        private void cameraCalibrationWindow_FormClosed(object sender, EventArgs e)
        {
            cameraCalibration.Dispose();
            cameraCalibration = null;

            CameraCalibrationButton.Enabled = true;
            ImageProcessingCalibration_Button.Enabled = true;
            StartButton.Enabled = true;
            CalibrateButton.Enabled = true;
        }

        private void ImageProcessingCalibration_Button_Click(object sender, EventArgs e)
        {
            CameraCalibrationButton.Enabled = false;
            ImageProcessingCalibration_Button.Enabled = false;
            StartButton.Enabled = false;
            CalibrateButton.Enabled = false;

            circleDetectionCalibration = new CircleDetectionCalibration();
            circleDetectionCalibration.FormClosed += circleDetectionCalibration_FormClosed;
            circleDetectionCalibration.Show();
        }

        private void circleDetectionCalibration_FormClosed(object sender, EventArgs e)
        {
            circleDetectionCalibration.Dispose();
            circleDetectionCalibration = null;

            CameraCalibrationButton.Enabled = true;
            ImageProcessingCalibration_Button.Enabled = true;
            StartButton.Enabled = true;
            CalibrateButton.Enabled = true;
        }

        /// <summary>
        /// Creates a new virtual table window, and sets it as an alternative <see cref="PositionCollector"/> for the game.
        /// </summary>
        /// <param name="sender">The control that caused this event.</param>
        /// <param name="e">Empty event args.</param>
        private void VirtualTableButtonClick(object sender, EventArgs e)
        {
            virtualTableButton.Enabled = false;
            var tableView = new VirtualTableView();
            
            OverridePositionCollector = tableView.Table;
            tableView.Closing += (o, args) =>
            {
                OverridePositionCollector = null;
                virtualTableButton.Enabled = true;
            };

            tableView.Show();
        }
    }
}
