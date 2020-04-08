using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.GoalDetectionFramework
{
    /// <summary>
    /// Has the functionality to light the Led strip
    /// </summary>
    public class LEDController
    {
        private LEDController()
        {
            try
            {
                serialPort = new SerialPort("/dev/ttyUSB0", 115200);
                serialPort.Open();
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static LEDController instance;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static LEDController Instance => instance ?? (instance = new LEDController());

        private SerialPort serialPort;

        /// <summary>
        /// LED show when a goal was shot
        /// </summary>
        public Task DoGoalLEDShow()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    serialPort.Write("goal");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        /// <summary>
        /// LED show when the program starts
        /// </summary>
        public Task DoStartLEDShow()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    serialPort.Write("start");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        /// <summary>
        /// LED show when the game is stopped
        /// </summary>
        /// <returns>executeable Task</returns>
        public Task DoStopLEDShow()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    serialPort.Write("stop");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
    }
}
