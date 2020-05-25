using RockyHockey.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockyHockeyGUI
{
    /// <summary>
    /// Logger that can display a MessageBox
    /// </summary>
    public class MessageBoxLogger : Logger
    {
        private string displayText;

        /// <summary>
        /// Writes the text into a private field
        /// </summary>
        /// <param name="text">text to write</param>
        /// <returns>executeable Task</returns>
        public override void Log(string text)
        {
            displayText += text + Environment.NewLine;
        }

        /// <summary>
        /// Displays the logged information
        /// </summary>
        public void Show()
        {
            MessageBox.Show(displayText);
            displayText = string.Empty;
        }
    }
}
