using RockyHockey.Common;
using System;
using System.Diagnostics;
using System.Media;

namespace RockyHockey.SoundFramework
{
    /// <summary>
    /// Static Class that can Play *.wmv files form the SoundEffects directory
    /// </summary>
    public static class SoundEffectsPlayer
    {
        private static SoundPlayer soundPlayer = new SoundPlayer();

        /// <summary>
        /// Logger for Errors
        /// </summary>
        public static ILogger Logger { get; set; }

        /// <summary>
        /// Plays the surpassed sound file
        /// </summary>
        /// <param name="soundFile">name of the sound file</param>
        public static void PlaySound(string soundFile)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                // Code for Sound under Unix
                try
                {
                    Process proc = new Process { EnableRaisingEvents = false };
                    proc.StartInfo.FileName = "aplay";
                    proc.StartInfo.Arguments = $"-t wav {soundFile}";
                    proc.Start();
                }
                catch (Exception ex)
                {
                    Logger?.Log("Error while playing a sound file on linux");
                    Logger?.Log(ex);
                }
            }
            else
            {
                // Code for Sound under Windows
                try
                {
                    soundPlayer.SoundLocation = $"{soundFile}";
                    soundPlayer.PlaySync();
                }
                catch (Exception ex)
                {
                    Logger?.Log("Error while playing a sound file on windows");
                    Logger?.Log(ex);
                }
            }
        }
    }
}
