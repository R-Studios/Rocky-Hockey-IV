using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public class ImagePositionCollector : PositionCollector
    {
        public ImagePositionCollector(ImageProvider imageProvider = null)
        {
            this.imageProvider = imageProvider ?? new CameraReader();
        }

        public override List<TimedCoordinate> GetPuckPositions()
        {
            List<TimedCoordinate> coordinates;

            List<Task<TimedCoordinate>> detectionTasks = new List<Task<TimedCoordinate>>();
            for (int a = 0; a < 10; a++)
                detectionTasks.Add(ImageProcessing.ProcessImage(imageProvider.getTimedImage(), imageProvider.SliceImage));

            coordinates = new List<TimedCoordinate>();
            foreach (Task<TimedCoordinate> detectionTask in detectionTasks)
            {
                detectionTask.Wait();
                TimedCoordinate detectedPosition = detectionTask.Result;

                if (detectedPosition != null)
                    coordinates.Add(detectedPosition);
            }

            return coordinates;
        }

        public override TimedCoordinate GetPuckPosition()
        {
            Task<TimedCoordinate> detectionTask = ImageProcessing.ProcessImage(imageProvider.getTimedImage(), imageProvider.SliceImage);
            detectionTask.Wait();
            return detectionTask.Result;
        }

        public override void StopMotionCapturing()
        {
            imageProvider.finalize();
        }
    }
}
