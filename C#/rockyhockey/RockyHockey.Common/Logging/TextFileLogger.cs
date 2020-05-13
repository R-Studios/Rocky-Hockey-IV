using System.IO;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
    /// <summary>
    /// Logger that logs into a text file
    /// </summary>
    public class TextFileLogger : Logger
    {
        /// <summary>
        /// Creates a new Instance of the TextFileLogger with the surpassed file
        /// </summary>
        /// <param name="logFile">file with its path</param>
        public TextFileLogger(string logFile)
        {
            this.logFile = logFile;
        }

        private string logFile;

        /// <summary>
        /// Writes the String into the Logfile
        /// </summary>
        /// <param name="text">String to log</param>
        /// <returns>executeable Task</returns>
        public override async Task Log(string text)
        {
            using (var streamWriter = new StreamWriter(logFile, true))
            {
                await streamWriter.WriteLineAsync(text).ConfigureAwait(false);
                streamWriter.Flush();
            }
        }
    }
}
