using NAudio.Wave;
using RockyHockey.Common;
using System;
using System.Threading.Tasks;

namespace RockyHockey.SoundFramework
{
    /// <summary>
    /// Plays the soundtrack and the finish Sound
    /// </summary>
    public class SoundtrackAndFinishSoundPlayer
    {
        private SoundtrackAndFinishSoundPlayer() { }

        private static SoundtrackAndFinishSoundPlayer instance;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static SoundtrackAndFinishSoundPlayer Instance => instance ?? (instance = new SoundtrackAndFinishSoundPlayer());

        /// <summary>
        /// Plays the sound at the end of a game
        /// </summary>
        /// <param name="score">Score to determine which player was better</param>
        /// <returns>executeable Task</returns>
        public Task PlayGameFinishSound(Score score)
        {
            return Task.Factory.StartNew(() =>
            {
                SoundEffectsPlayer.PlaySound("Spielende.wav");
                Task.Delay(GetWavFileDuration("Spielende.wav")).Wait();
                if (score.HumanGoals < score.RobotGoals)
                {
                    SoundEffectsPlayer.PlaySound("IchHabeGewonnen.wav");
                }
                else if (score.HumanGoals > score.RobotGoals)
                {
                    SoundEffectsPlayer.PlaySound("MenschHatGewonnen.wav");
                }
                else
                {
                    SoundEffectsPlayer.PlaySound("Unentschieden.wav");
                }
            });
        }

        private TimeSpan GetWavFileDuration(string fileName)
        {
            WaveFileReader wf = new WaveFileReader(fileName);
            return wf.TotalTime;
        }
    }
}
