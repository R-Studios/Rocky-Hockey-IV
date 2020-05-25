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
        int count = 0;
        string[] names;

        public TestImageReader()
        {
            SliceImage = false;
            string prefix = "P:\\testImages\\";

            names = new string[10];

            for (int a = 0; a < 10; a++)
                names[a] = prefix + a + ".png";
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
            retval = new TimedImage();

            if (number < 8)
                retval.image = CvInvoke.Imread(names[number]);
            else
                retval.image = new Mat();

            retval.timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

			lastCapture = retval;

            return retval;
        }
    }
}
