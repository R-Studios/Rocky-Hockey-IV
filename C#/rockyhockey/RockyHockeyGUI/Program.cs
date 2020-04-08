using RockyHockey.Common;
using RockyHockey.GoalDetectionFramework;
using System;
using System.Windows.Forms;

namespace RockyHockeyGUI
{
    static class Program
    {
        /// <summary>
        /// Logger for errors
        /// </summary>
        public static ILogger MessageBoxLogger { get; } = new MessageBoxLogger();

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainView());
            }
            catch (Exception ex)
            {
                MessageBoxLogger?.Log(ex);
            }
            finally
            {
                GoalDetectionProvider.Instance.UnexportPins();
                Application.Exit();
            }
        }
    }
}
