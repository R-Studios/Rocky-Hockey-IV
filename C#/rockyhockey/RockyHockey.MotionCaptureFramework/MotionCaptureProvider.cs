using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    /// <inheritdoc />
    /// <summary>
    /// Implementierung des MotionCaptureProvider
    /// </summary>
    public class MotionCaptureProvider : IMotionCaptureProvider
    {
        /// <summary>
        /// Creates a new instance of the MotionCapturePorvider
        /// </summary>
        public MotionCaptureProvider()
        {
            gameCameraDetectionFramework = new GameCameraDetectionFramework();
            gameCameraDetectionFramework.InitializeCameras();
        }

        private readonly AbstractCameraDetectionFramework gameCameraDetectionFramework;

        public Task<GameFieldPosition> GetBatPosition()
        {
            throw new NotImplementedException();
        }

        public Task<GameFieldPosition> GetEnemyBatPosition()
        {
            throw new NotImplementedException();
        }

        public Task<Size> GetGameFieldSize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// return bat positions for the last 5 to 10 tracked positions
        /// </summary>
        /// <returns>Enumerable of FrameGameFieldPositions</returns>
        public async Task<IEnumerable<FrameGameFieldPosition>> GetPuckPositions()
        {
            var convertedPositions = new List<FrameGameFieldPosition>();

            int counter = 0;

            do
            {
                foreach (Task<GameFieldPosition> positionDetection in gameCameraDetectionFramework.GetCameraPictures())
                {
                    GameFieldPosition detectedPosition = await positionDetection;

                    if (detectedPosition != null)
                        convertedPositions.Add(new FrameGameFieldPosition
                        {
                            Y = detectedPosition.X,
                            X = Config.Instance.Camera1.FieldSize.Height - detectedPosition.Y,
                            FrameNumber = counter++
                        });
                }
            } while (convertedPositions.Count < 5);

            return convertedPositions;
        }

        /// <summary>
        /// Stops the MotionCapureProvider
        /// </summary>
        /// <returns>executeable Task</returns>
        public void StopMotionCapturing()
        {
            gameCameraDetectionFramework.StopCameraDetection();
        }
    }
}
