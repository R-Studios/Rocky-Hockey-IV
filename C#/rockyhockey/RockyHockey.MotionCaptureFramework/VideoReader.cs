using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public class VideoReader : ImageProvider
    {
        int frameCount;
        VideoCapture video;
        int frameIndex;

        public VideoReader(string videoPath)
        {
            SliceImage = true;
            video = new VideoCapture(videoPath);
            video.Start();
            frameCount = (int)video.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);
            frameIndex = 1;
        }

        public override void finalize()
        {
            video?.Stop();
            video?.Dispose();
            video = null;
        }

        public override int getFPS()
        {
            return (int)video.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
        }

        public override TimedImage getTimedImage()
        {
            setFrameIndex(frameIndex++);

            TimedImage capture = new TimedImage
            {
                image = video.QueryFrame(),
                timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };

            lastCapture = capture;

            return capture;
        }

        public int setFrameIndex(int index = -1)
        {
            if (index < 0)
                index = 0;
            else if (index >= frameCount)
                index = frameCount - 1;

            frameIndex = index;

            video.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames, index);

            return index;
        }
    }
}
