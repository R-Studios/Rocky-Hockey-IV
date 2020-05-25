using System;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
    /// <summary>
    /// Base class for a logger implementation that contains the funcionality to
    /// log every inner exception of an exception
    /// </summary>
    public abstract class Logger : ILogger
    {
        /// <summary>
        /// Writes the string into the log
        /// </summary>
        /// <param name="text">String to log</param>
        /// <returns>executeable Task</returns>
        public abstract void Log(string text);

        /// <summary>
        ///  Writes the exception and its inner exceptions into the log
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <returns>executeable Task</returns>
        public async void Log(Exception ex)
        {
            string message = ex.Message + "\n" + ex.StackTrace;
            Log(message);
            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                Log($"inner exception: {innerException.Message}");
                innerException = innerException.InnerException;
            }
        }
    }
}
