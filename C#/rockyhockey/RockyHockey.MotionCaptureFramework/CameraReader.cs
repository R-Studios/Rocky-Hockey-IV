using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.CV.Structure;
using RockyHockey.Common;
using AForge.Video.DirectShow;
using AForge.Video;

namespace RockyHockey.MotionCaptureFramework
{
    public class CameraReader : ImageProvider
    {
        //AForge
        private VideoCaptureDevice videoCaptureDevice;

        //EmguCV
        VideoCapture camera;

        /// <summary>
        /// initializes the camera
        /// </summary>
        /// <param name="withConfigBorders">weather or not the width and height from the config should be used</param>
        public CameraReader(bool withConfigBorders = true, CameraConfig cameraConfig = null)
        {
            cameraConfig = cameraConfig ?? Config.Instance.Camera1;
            SliceImage = true;
            
            //configure selected Camera
            videoCaptureDevice = new VideoCaptureDevice(Config.Instance.Camera1.name);

            //if (withConfigBorders)
            //{
            //    camera.SetCaptureProperty(CapProp.FrameWidth, cameraConfig.Resolution.Width);
            //    camera.SetCaptureProperty(CapProp.FrameHeight, cameraConfig.Resolution.Height);
            //}

            //camera.SetCaptureProperty(CapProp.Fps, Config.Instance.FrameRate);
            videoCaptureDevice.Start();
            //camera.Start();
        }

        public override TimedImage getTimedImage()
        {
            TimedImage image = new TimedImage();
            image.image = new Mat();
            
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            
            if (nextFrame != null)
            {
                Image<Bgr, byte> imageCV = new Image<Bgr, byte>(nextFrame);
                image.image = imageCV.Mat;
                image.timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            }
            
            return image;
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            nextFrame = (Bitmap)eventArgs.Frame.Clone();
        }

        public override void finalize()
        {
            //camera?.Stop();
            //camera?.Dispose();
            //camera = null;
            videoCaptureDevice?.Stop();
            videoCaptureDevice = null;
        }

        public override int getFPS()
        {
            return (int)camera.GetCaptureProperty(CapProp.Fps);
        }
    }
}
