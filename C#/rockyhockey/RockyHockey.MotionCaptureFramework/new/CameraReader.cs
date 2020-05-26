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

        public CameraReader()
        {
            SliceImage = true;
            camera = new VideoCapture(1);
            camera.SetCaptureProperty(CapProp.Fps, Config.Instance.FrameRate);
            camera.SetCaptureProperty(CapProp.FrameWidth, Config.Instance.Camera1.FieldSize.Width);
            camera.SetCaptureProperty(CapProp.FrameHeight, Config.Instance.Camera1.FieldSize.Height);
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
            camera.Stop();
            camera.Dispose();
            camera = null;
        }
    }
}
