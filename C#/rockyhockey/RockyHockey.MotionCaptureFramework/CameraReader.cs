using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.CvEnum;
using Emgu.CV;
using RockyHockey.Common;

namespace RockyHockey.MotionCaptureFramework
{
    public class CameraReader : ImageProvider
    {
        private VideoCapture camera;

        /// <summary>
        /// initializes the camera
        /// </summary>
        /// <param name="withConfigBorders">weather or not the width and height from the config should be used</param>
        public CameraReader(bool withConfigBorders = true, CameraConfig cameraConfig = null)
        {
            cameraConfig = cameraConfig ?? Config.Instance.Camera1;
            SliceImage = true;
            camera = new VideoCapture(Config.Instance.Camera1.index);

            if (withConfigBorders)
            {
                camera.SetCaptureProperty(CapProp.FrameWidth, cameraConfig.Resolution.Width);
                camera.SetCaptureProperty(CapProp.FrameHeight, cameraConfig.Resolution.Height);
            }

            camera.SetCaptureProperty(CapProp.Fps, Config.Instance.FrameRate);
            camera.Start();
        }

        public override TimedImage getTimedImage()
        {
            TimedImage image = new TimedImage();
            image.image = new Mat();
            camera.Read(image.image);
            image.timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			lastCapture = image;
            return image;
        }

        public override void finalize()
        {
            camera?.Stop();
            camera?.Dispose();
            camera = null;
        }

        public override int getFPS()
        {
            return (int)camera.GetCaptureProperty(CapProp.Fps);
        }
    }
}
