using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    /// <summary>
    /// Class that can calculate the Vector of the puck based on its positions
    /// </summary>
    public class VectorCalculationProvider
    {
        /// <summary>
        /// Constructs a new instance of the VectorCalculationProvider
        /// </summary>
        /// <param name="motionCaptureProvider">motionCaptureProvider to get the positions of the puck</param>
        public VectorCalculationProvider(IMotionCaptureProvider motionCaptureProvider)
        {
            this.motionCaptureProvider = motionCaptureProvider;
        }

        /// <summary>
        /// Amount of frames the camera records per second
        /// </summary>
        private double cameraFrameRate = Config.Instance.FrameRate;

        private readonly IMotionCaptureProvider motionCaptureProvider;

        private readonly double tolerance = Config.Instance.Tolerance;

        /// <summary>
        /// Calculates the vector that describes the trajectory of the puck
        /// </summary>
        /// <returns>calculatedvVector of the puck with its velocity</returns>
        public async Task<VelocityVector> CalculatePuckVector()
        {
            List<TimedCoordinate> puckPositions = motionCaptureProvider.GetPuckPositions();

            // Vector should start at the last point because the puck has been detected there for the last time
            var puckVector = new VelocityVector(puckPositions.First(), puckPositions.Last());

            puckVector.Velocity = CalculatePuckVelocity(puckVectors);
            return puckVector;
        }

        private Task<List<VelocityVector>> CreateVectors(List<TimedCoordinate> puckPositions)
        {
            return Task.Factory.StartNew(() =>
            {
                var puckVectors = new List<VelocityVector>();
                for (int i = 1; i < puckPositions.Count(); i++)
                {
                    var vec = new VelocityVector(puckPositions[i - 1], puckPositions[0]);

                    puckVectors.Add(vec);
                }
                return puckVectors;
            });
        }

        /// <summary>
        /// Creates single vectors of the puck and calculates the average velocity
        /// </summary>
        /// <returns>velocity of the puck in pixels per millisecond</returns>
        private double CalculatePuckVelocity(IEnumerable<TimedVector> puckVectors)
        {
            double velocity = 0;
            foreach (TimedVector vec in puckVectors)
            {
                velocity += vec.Length / vec.NeededTime;
            }
            return velocity / puckVectors.Count();
        }

        private double CalculateAveragePitch(IEnumerable<Vector> puckVectors)
        {
            double retVal = 0;

            if (puckVectors.Any())
            {
                double totalPitch = 0;
                double oldPitch = 0;
                int count = 0;

                foreach (Vector vec in puckVectors)
                {
                    double pitch = 0;
                    if (!(vec.Position.X == vec.Direction.X) || !(vec.Position.Y == vec.Direction.Y))
                    {
                        pitch = vec.GetVectorGradient();
                    }

                    if (oldPitch == 0 || (oldPitch - pitch) < tolerance || (oldPitch - pitch) > -tolerance)
                    {
                        // In this case the recognized vector is in tolerance an will be used to calculate the average
                        totalPitch += pitch;
                        count++;
                    }
                }

                retVal = totalPitch / count;
            }

            return retVal;
        }
    }
}
