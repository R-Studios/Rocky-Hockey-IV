using System;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
    /// <summary>
    /// Basic Logger-Interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes the String into the Log
        /// </summary>
        /// <param name="text">String to log</param>
        /// <returns>executeable Task</returns>
        Task Log(string text);

        /// <summary>
        ///  Writes the exception into the log
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <returns>executeable Task</returns>
        Task Log(Exception ex);
    }
}
