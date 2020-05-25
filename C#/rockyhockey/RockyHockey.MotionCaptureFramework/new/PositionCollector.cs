using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public class PositionCollector
    {
        private ImageProvider imageProvider;
        public PositionCollector(ImageProvider imageProvider = null)
        {
            this.imageProvider = imageProvider ?? new CameraReader();
        }

        public List<TimedCoordinate> GetPuckPositions()
        {
            List<Task<TimedCoordinate>> detectionTasks = new List<Task<TimedCoordinate>>();
            for (int a = 0; a < 10; a++)
                detectionTasks.Add(PositionCalculator.ProcessImage(imageProvider.getTimedImage(), imageProvider.SliceImage));

            List<TimedCoordinate> coordinates = new List<TimedCoordinate>();
            foreach (Task<TimedCoordinate> detectionTask in detectionTasks)
            {
                detectionTask.Wait();
                TimedCoordinate detectedPosition = detectionTask.Result;

                if (detectedPosition != null)
                    coordinates.Add(detectedPosition);
            }

            return coordinates;
        }

        public void StopMotionCapturing()
        {
            imageProvider.finalize();
        }
    }
}
