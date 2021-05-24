using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RockyHockey.Common;
using System.IO.Ports;
using System.Drawing;
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
        private static MovementController instance;

        private bool initialized = false;

        private SerialPort xAxis;

        private SerialPort yAxis;

        /// <summary>
        /// Hook used to grab axis movement for the virtual table.
        /// </summary>
        public event Action<double, double> OnMove;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static MovementController Instance => instance ?? (instance = new MovementController());

        /// <summary>
        /// Position of the RockyHockey Bat
        /// </summary>
        public Coordinate BatPosition { get; private set; } = new Coordinate()
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
            foreach (String s in p)
                Console.WriteLine("Serial Port: " + s);

            try
            {
                if (!initialized)
                {
                    xAxis = new SerialPort(p[0], 115200, Parity.None, 8, StopBits.One);
                    xAxis.Handshake = Handshake.None;
                    xAxis.Open();
                    xAxis.WriteTimeout = 500;
                    yAxis = new SerialPort(p[1], 115200, Parity.None, 8, StopBits.One);
                    
                    yAxis.Handshake = Handshake.None;
                    yAxis.WriteTimeout = 500;
                    yAxis.Open();
                    initialized = true;
                }

                init();
            }
            catch (Exception ex)
            {
                MovementControllerLogger?.Log(ex);
            }
        }

        /// <summary>
        /// Moves the bat to in the direction of the surpassed vectors
        /// </summary>
        /// <param name="vecList">enumerable of vectors that describe the movement of the bat</param>
        /// <param name="delayBeforePunch">delay in milliseconds before the punch mevement should start</param>
        /// <returns>executeable Task</returns>
        public void MoveStrategy(IEnumerable<TimedVector> vecList, int delayBeforePunch)
        {
            foreach (TimedVector vec in vecList)
            {
                BatPosition = vec.End;

                Move(BatPosition);

                long delay = vec.TimedEnd.Timestamp - DateTimeOffset.Now.ToUnixTimeMilliseconds() + delayBeforePunch;

                // no waiting after the last punch
                if (delay > 0)
                {
                    Task.Delay(Convert.ToInt32(delay)).Wait();
                }
            }
        }

        public void Move(Coordinate pos)
        {
            try
            {
                OnMove?.Invoke(pos.X, pos.Y);
                
                xAxis.Write(Convert.ToInt32(pos.X).ToString() + ",");
                yAxis.Write(Convert.ToInt32(pos.Y).ToString() + ",");
            }
            catch (Exception ex)
            {
                MovementControllerLogger?.Log(ex);
            }
        }

        /// <summary>
        /// Closes the serial Ports
        /// </summary>
        /// <returns>executeable Task</returns>
        public Task CloseSerialPorts()
        {
            return Task.Factory.StartNew(() =>
            {
                xAxis?.Close();
                yAxis?.Close();
                xAxis?.Dispose();
                yAxis?.Dispose();

                initialized = false;
            });
        }

        public void init()
        {
            if (!initialized)
                InitializeSerialPorts();
            else
            {
                xAxis.Write("calibrate");
                yAxis.Write("calibrate");
                BatPosition = new Coordinate()
                {
                    X = Config.Instance.GameFieldSizeMM.Width / 2,
                    Y = Config.Instance.GameFieldSizeMM.Height / 2
                };

                Move(BatPosition);
            }
        }
    }
}
