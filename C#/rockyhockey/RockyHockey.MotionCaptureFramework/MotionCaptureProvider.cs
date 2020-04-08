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
        /// return the puck positions for the last 5 tracked positions
        /// </summary>
        /// <returns>Enumerable of FrameGameFieldPositions</returns>
        public async Task<IEnumerable<FrameGameFieldPosition>> GetPuckPositions()
        {
            var convertedPositions = new List<FrameGameFieldPosition>();
            foreach (FrameGameFieldPosition position in await gameCameraDetectionFramework.GetCameraPictures().ConfigureAwait(false))
            {
                convertedPositions.Add(PicturePositionToFrameGamefieldPosition.ConvertCameraToFrameGameFieldPosition(position));
            }
            return convertedPositions;
        }

        /// <summary>
        /// Stops the MotionCapureProvider
        /// </summary>
        /// <returns>executeable Task</returns>
        public async Task StopMotionCapturing()
        {
            await gameCameraDetectionFramework.StopCameraDetection().ConfigureAwait(false);
        }
    }
}
