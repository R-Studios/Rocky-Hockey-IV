using System;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
    /// <summary>
    /// Logger that logs on the console
    /// </summary>
    public class ConsoleLogger : Logger
    {
        /// <summary>
        /// Logs the text onto the console
        /// </summary>
        /// <param name="text">text to log</param>
        /// <returns>executeable Task</returns>
        public override void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}
