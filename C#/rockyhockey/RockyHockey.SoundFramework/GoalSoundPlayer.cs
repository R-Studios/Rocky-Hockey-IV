using NAudio.Wave;
using RockyHockey.Common;
using System;
using System.Threading.Tasks;

namespace RockyHockey.SoundFramework
{
    /// <summary>
    /// Plays sound when a goal was shot
    /// </summary>
    public class GoalSoundPlayer
    {
        private GoalSoundPlayer() { }

        private static GoalSoundPlayer instance;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static GoalSoundPlayer Instance => instance ?? (instance = new GoalSoundPlayer());

        /// <summary>
        /// Plays the goal sounds
        /// </summary>
        /// <param name="score">Score to tell</param>
        /// <returns>executeable Task</returns>
        public Task OnDetectedGoal(Score score)
        {
            return Task.Factory.StartNew(() =>
            {
                SoundEffectsPlayer.PlaySound("Tor.wav");
                Task.Delay(GetWavFileDuration("Tor.wav")).Wait();
                SoundEffectsPlayer.PlaySound("Spielstand.wav");
                Task.Delay(GetWavFileDuration("Spielstand.wav")).Wait();
                SoundEffectsPlayer.PlaySound($"{score.RobotGoals}.wav");
                Task.Delay(GetWavFileDuration($"{score.RobotGoals}.wav")).Wait();
                SoundEffectsPlayer.PlaySound("Zu.wav");
                Task.Delay(GetWavFileDuration("Zu.wav")).Wait();
                SoundEffectsPlayer.PlaySound($"{score.HumanGoals}.wav");
                Task.Delay(GetWavFileDuration($"{score.HumanGoals}.wav")).Wait();
                if (score.HumanGoals > score.RobotGoals)
                {
                    SoundEffectsPlayer.PlaySound("FuerDenHerausforderer.wav");
                }
                else if (score.HumanGoals < score.RobotGoals)
                {
                    SoundEffectsPlayer.PlaySound("FuerMich.wav");
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
