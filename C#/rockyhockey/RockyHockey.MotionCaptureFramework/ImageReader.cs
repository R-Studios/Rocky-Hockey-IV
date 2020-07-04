using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public class ImageReader : ImageProvider
    {
        string filePath = "P:\\testImages\\";
        int minIndex;
        int maxindex;
        int frameIndex;

        public ImageReader(string filePath, int minIndex, int maxindex)
        {
            SliceImage = false;
            frameIndex = minIndex;

            this.minIndex = minIndex;
            this.maxindex = maxindex;
            this.filePath = filePath;
        }

        public override void finalize()
        {
        }

        public override int getFPS()
        {
            return 0;
        }

        public override TimedImage getTimedImage()
        {
            TimedImage retval = new TimedImage();

            retval.image = CvInvoke.Imread(filePath + frameIndex + ".png");

            retval.timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

			lastCapture = retval;

            setFrameIndex(++frameIndex);

            return retval;
        }

        public int setFrameIndex(int index = -1)
        {
            if (index < minIndex)
                index = 0;
            else if (index > maxindex)
                index = maxindex;

            frameIndex = index;

            return index;
        }
    }
}
