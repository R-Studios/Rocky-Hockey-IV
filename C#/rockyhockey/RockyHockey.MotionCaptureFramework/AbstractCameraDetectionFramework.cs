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

        private bool stopped = false;

        /// <summary>
        /// Logger for errors
        /// </summary>
        public ILogger Logger { get; }


        /// <summary>
        /// Initializes the cameras, set properties and start them.
        /// Throws an Exception, if not successful.
        /// </summary>
        public void InitializeCameras()
        {
            try
            {
                stopped = false;
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
        /// initiates calculation of bat positions
        /// </summary>
        /// <returns>calculation task list</returns>
        public List<Task<TimedCoordinate>> GetCameraPictures()
        {
            int frameCount = 0;

            List<Task<TimedCoordinate>> detectionTasks = new List<Task<TimedCoordinate>>();

            while(!stopped && frameCount++ < 10)
            {
                var mat = new Mat();
                camera.Read(mat);
                detectionTasks.Add(ProcessImage(mat, DateTimeOffset.Now.ToUnixTimeMilliseconds()));
            }

            return detectionTasks;
        }

        internal abstract Task<TimedCoordinate> ProcessImage(Mat mat, long timestamp);

        /// <summary>
        /// Stops and disposes the camera
        /// </summary>
        /// <returns>executeable Task</returns>
        public void StopCameraDetection()
        {
            stopped = true;
            camera.Stop();
            camera.Dispose();
            camera = null;
        }
    }
}