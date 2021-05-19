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
        private VideoCapture camera;

        private TimedImage screenshot = new TimedImage();

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
            videoCaptureDevice.Start();
            
        }

        public override TimedImage getTimedImage()
        {
            TimedImage image = new TimedImage();
            image.image = new Mat();
            
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            // Image liefert nur null zurück und kann daher nicht ausgewertet werden
            
            return image;
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            TimedImage image = new TimedImage();
            image.image = new Mat();
            Mat convertedImage = new Mat();
            nextFrame = (Bitmap)eventArgs.Frame.Clone();
            Bitmap nextFrame2 = (Bitmap)eventArgs.Frame.Clone();
            if (nextFrame2 != null)
            {
                Image<Bgr, byte> imageCV = new Image<Bgr, Byte>(nextFrame2);
                convertedImage = imageCV.Mat;
                image.image = convertedImage;
                image.timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                lastCapture = image;
                screenshot = image;                
            }            
        }


        public override void finalize()
        {
            //camera?.Stop();
            //camera?.Dispose();
            //camera = null;
            videoCaptureDevice?.SignalToStop();
            videoCaptureDevice = null;
        }

        public override int getFPS()
        {
            return (int)camera.GetCaptureProperty(CapProp.Fps);
        }
    }
}
