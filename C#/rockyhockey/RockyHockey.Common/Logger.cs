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
        public abstract Task Log(string text);

        /// <summary>
        ///  Writes the exception and its inner exceptions into the log
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <returns>executeable Task</returns>
        public async Task Log(Exception ex)
        {
            string message = ex.Message + "\n" + ex.StackTrace;
            await Log(message).ConfigureAwait(false);
            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                await Log($"inner exception: {innerException.Message}").ConfigureAwait(false);
                innerException = innerException.InnerException;
            }
        }
    }
}
