using System.IO;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
    /// <summary>
    /// Logger that logs into a text file
    /// </summary>
    public class TextFileLogger : Logger
    {
        private static TextFileLogger instance = null;
        public static TextFileLogger Instance => instance ?? (instance = new TextFileLogger("errors.txt"));

        /// <summary>
        /// Creates a new Instance of the TextFileLogger with the surpassed file
        /// </summary>
        /// <param name="logFile">file with its path</param>
        public TextFileLogger(string logFile)
        {
            if (!File.Exists(logFile))
                File.Create(logFile);

            this.logFile = new StreamWriter(logFile, true);
        }

        private StreamWriter logFile;

        /// <summary>
        /// Writes the String into the Logfile
        /// </summary>
        /// <param name="text">String to log</param>
        /// <returns>executeable Task</returns>
        public override void Log(string text)
        {
            lock (logFile)
            {
                logFile.WriteLine(text);
                logFile.Flush();
            }
        }
    }
}
