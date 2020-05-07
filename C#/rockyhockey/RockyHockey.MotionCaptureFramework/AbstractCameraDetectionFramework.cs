using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using RockyHockey.Common;

namespace RockyHockey.MotionCaptureFramework
{
    /// <summary>
    /// Abstract Camera Detection Framework
    /// </summary>
    public abstract class AbstractCameraDetectionFramework
    {
        private readonly int TargetedFramesPerSecond = Config.Instance.FrameRate;
        private const int TargetedCameraHeight = 320;
        private const int TargetedCameraWidth = 240;
        //private const int TargetedCameraHeight = 640;
        //private const int TargetedCameraWidth = 480;
        private VideoCapture camera;
        private int frameCount = 0;
        private int puckRecognizedCount = 0;
        private readonly IList<FrameGameFieldPosition> cameraPuckPositions = new List<FrameGameFieldPosition>();

        /// <summary>
        /// Initializes the cameras, set properties and start them.
        /// Throws an Exception, if not successful.
        /// </summary>
        public void InitializeCameras()
        {
            try
            {
                camera = new VideoCapture(Config.Instance.Camera1.index);
                camera.SetCaptureProperty(CapProp.Fps, TargetedFramesPerSecond);
                camera.SetCaptureProperty(CapProp.FrameWidth, TargetedCameraHeight);
                camera.SetCaptureProperty(CapProp.FrameHeight, TargetedCameraWidth);
                camera.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Processes images for the camera till 5 circles are detected and returns middle point of these circles
        /// </summary>
        /// <returns>CameraPictureQueue</returns>
        public async Task<IEnumerable<FrameGameFieldPosition>> GetCameraPictures()
        {
            cameraPuckPositions.Clear();
            puckRecognizedCount = 0;
            frameCount = 0;

            while(puckRecognizedCount < 5)
            {
                var mat = new Mat();
                camera.Read(mat);
                await ProcessImage(mat).ConfigureAwait(false);
                frameCount++;
            }
            return cameraPuckPositions;
        }

        internal abstract Task ProcessImage(Mat mat);

        internal void SetCameraPicture(GameFieldPosition gameFieldPosition)
        {
            puckRecognizedCount++;
            cameraPuckPositions.Add(new FrameGameFieldPosition
            {
                X = gameFieldPosition.X,
                Y = gameFieldPosition.Y,
                FrameNumber = frameCount
            });
        }

        /// <summary>
        /// Stops and disposes the camera
        /// </summary>
        /// <returns>executeable Task</returns>
        public Task StopCameraDetection()
        {
            return Task.Factory.StartNew(() =>
            {
                camera.Stop();
                camera.Dispose();
                camera = null;
            });
        }
    }
}