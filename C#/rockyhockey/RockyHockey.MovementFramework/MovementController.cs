using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RockyHockey.Common;
using System.IO.Ports;
using System.Drawing;
using RockyHockey.GoalDetectionFramework;
using System.Windows.Forms;
using System.Threading;
using System.Linq;

namespace RockyHockey.MovementFramework
{
    /// <summary>
    /// Implementierung des MovementController
    /// </summary>
    public class MovementController : IMovementController
    {
        private MovementController()
        {
            // Method needs to be called when a goal was shot
            GoalDetectionProvider.Instance.DetectedGoalEvent += OnGoalDetected;
            pixelToMMFactor = pixelToMMConverter.GetFactor(Config.Instance.GameFieldSize);
        }

        private static MovementController instance;

        private SerialPort xAxis;

        private SerialPort yAxis;

        private double pixelToMMFactor;

        private TimeCalculator timeCalculator = new TimeCalculator();

        private PixelToMMConverter pixelToMMConverter = new PixelToMMConverter();

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static MovementController Instance => instance ?? (instance = new MovementController());

        /// <summary>
        /// Position of the RockyHockey Bat
        /// </summary>
        public GameFieldPosition BatPosition { get; private set; } = new GameFieldPosition()
        {
            X = Config.Instance.GameFieldSize.Width - 50,
            Y = Config.Instance.GameFieldSize.Height - 110
        };

        /// <summary>
        /// Logger for the MovementController information
        /// </summary>
        public ILogger MovementControllerLogger { get; set; } //= new TextFileLogger("ArduinoCommandLog.txt");

        /// <summary>
        /// Initializes the serial Ports
        /// </summary>
        /// <returns>executeable Task</returns>
        public void InitializeSerialPorts()
        {
            string[] p = SerialPort.GetPortNames();

            try
            {

                xAxis = new SerialPort(p[0], 115200);
                xAxis.Open();
                yAxis = new SerialPort(p[1], 115200);
                yAxis.Open();

                xAxis.Write("calibrate");
                yAxis.Write("calibrate");
            }
            catch (Exception ex)
            {
                MovementControllerLogger?.Log(ex);
            }
        }

        /// <summary>
        /// Moves the bat to in the direction of the surpassed vectors
        /// </summary>
        /// <param name="vec">enumerable of vectors that describe the movement of the bat</param>
        /// <param name="delayBeforePunch">delay in milliseconds before the punch mevement should start</param>
        /// <returns>executeable Task</returns>
        public async Task MoveStrategy(IEnumerable<VelocityVector> vec, int delayBeforePunch)
        {
            for (int i = 0; i < vec.Count(); i++)
            {
                var next = vec.ElementAt(i);

                BatPosition = next.Direction;

                var movePosition = new GameFieldPosition()
                {
                    X = Config.Instance.GameFieldSize.Width - next.Direction.X,
                    Y = Config.Instance.GameFieldSize.Height - next.Direction.Y
                };
                await Move(movePosition).ConfigureAwait(false);

                // no waiting after the last punch
                if (!(i == vec.Count() - 1))
                {
                    var neededTime = await timeCalculator.CalculateTimeOfVelocityVector(next).ConfigureAwait(false);
                    Task.Delay(Convert.ToInt32(neededTime)).Wait();
                }
            }
        }

        private Task Move(GameFieldPosition pos)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    xAxis.Write(Convert.ToInt32(pos.X * pixelToMMFactor).ToString() + ",");
                    yAxis.Write(Convert.ToInt32(pos.Y * pixelToMMFactor).ToString() + ",");
                }
                catch (Exception ex)
                {
                    MovementControllerLogger?.Log(ex);
                }
            });
        }

        /// <summary>
        /// Closes the serial Ports
        /// </summary>
        /// <returns>executeable Task</returns>
        public Task CloseSerialPorts()
        {
            return Task.Factory.StartNew(() =>
            {
                if (xAxis != null && yAxis != null && xAxis.IsOpen && yAxis.IsOpen)
                {
                    xAxis.Close();
                    yAxis.Close();
                    xAxis.Dispose();
                    yAxis.Dispose();
                }
            });
        }

        /// <summary>
        /// Calibrates the Bat and sets the BatPosition back to default
        /// </summary>
        /// <param name="sender">sender of the Event</param>
        /// <param name="e">Event arguments</param>
        public void OnGoalDetected(object sender, DetectedGoalEventArgs e)
        {
            try
            {
                xAxis.Write("calibrate");
                yAxis.Write("calibrate");
                BatPosition = new GameFieldPosition()
                {
                    X = Config.Instance.GameFieldSize.Width - 50,
                    Y = Config.Instance.GameFieldSize.Height - 110
                };
            }
            catch (Exception ex)
            {
                MovementControllerLogger?.Log(ex);
            }
        }
    }
}
