using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public class TestImageReader : ImageProvider
    {
        int count = -2;
        string prefix = "P:\\testImages\\";

        public TestImageReader()
        {
            SliceImage = false;
        }

        public TestImageReader setcounter(int newCounter)
        {
            count = newCounter;

            return this;
        }

        public override void finalize()
        {
        }

        public override TimedImage getTimedImage()
        {
            return getTimedImage(count++);
        }

        public TimedImage getTimedImage(int number)
        {
            TimedImage retval = new TimedImage();

            if (number < 13)
                retval.image = CvInvoke.Imread(prefix + number + ".png");
            else
                retval.image = new Mat();

            retval.timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

			lastCapture = retval;

            return retval;
        }
    }
}
