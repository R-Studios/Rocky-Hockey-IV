using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;

namespace RockyHockey.MotionCaptureFramework
{
    public abstract class ImageProvider
    {
		public TimedImage lastCapture { get; protected set; }
        public bool SliceImage { get; protected set; }
        public abstract TimedImage getTimedImage();
        public abstract void finalize();
        public abstract int getFPS();
    }

    public struct TimedImage
    {
        public Mat image;
        public long timeStamp;

        public Bitmap GetImage()
        {
            return image?.Bitmap;
        }
    }
}
