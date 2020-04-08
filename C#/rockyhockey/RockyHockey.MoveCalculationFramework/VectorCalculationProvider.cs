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
            var puckPositions = await motionCaptureProvider.GetPuckPositions().ConfigureAwait(false);
            while (puckPositions.Count() < 5)
            {
                puckPositions = await motionCaptureProvider.GetPuckPositions().ConfigureAwait(false);
            }

            // Vector should start at the last point because the puck has been detected there for the last time
            var puckVector = new VelocityVector
            {
                Position = new GameFieldPosition { X = puckPositions.Last().X, Y = puckPositions.Last().Y }
            };
            IEnumerable<FrameVector> puckVectors = await CreateVectors(puckPositions).ConfigureAwait(false);
            // Vector has the average pitch of all detected vectors
            puckVector.Direction = new GameFieldPosition
            {
                Y = puckPositions.Last().Y + await CalculateAveragePitch(puckVectors).ConfigureAwait(false)
            };

            // Direction of the puckVector
            if (puckPositions.Last().X >= puckPositions.First().X)
            {
                puckVector.Direction.X = puckVector.Position.X + 1;
            }
            else
            {
                puckVector.Direction.X = puckVector.Position.X - 1;
            }

            puckVector.Velocity = await CalculatePuckVelocity(puckVectors).ConfigureAwait(false);
            return puckVector;
        }

        private Task<List<FrameVector>> CreateVectors(IEnumerable<FrameGameFieldPosition> puckPositions)
        {
            return Task.Factory.StartNew(() =>
            {
                var puckVectors = new List<FrameVector>();
                for (int i = 1; i < puckPositions.Count(); i++)
                {
                    var vec = new FrameVector
                    {
                        Position = puckPositions.ElementAt(i - 1),
                        Direction = puckPositions.ElementAt(i),
                        FrameNumber = puckPositions.ElementAt(i).FrameNumber - puckPositions.ElementAt(i - 1).FrameNumber
                    };
                    puckVectors.Add(vec);
                }
                return puckVectors;
            });
        }

        /// <summary>
        /// Creates single vectors of the puck and calculates the average velocity
        /// </summary>
        /// <returns>velocity of the puck in pixels per millisecond</returns>
        private async Task<double> CalculatePuckVelocity(IEnumerable<FrameVector> puckVectors)
        {
            double velocity = 0;
            foreach (FrameVector vec in puckVectors)
            {
                double length = await vec.GetVectorLength().ConfigureAwait(false);
                velocity += length / ((vec.FrameNumber / cameraFrameRate) * 1000);
            }
            return velocity / puckVectors.Count();
        }

        private async Task<double> CalculateAveragePitch(IEnumerable<Vector> puckVectors)
        {
            if (!puckVectors.Any())
            {
                return 0;
            }

            double totalPitch = 0;
            double oldPitch = 0;
            int count = 0;

            foreach (Vector vec in puckVectors)
            {
                double pitch = 0;
                if (!(vec.Position.X == vec.Direction.X) || !(vec.Position.Y == vec.Direction.Y))
                {
                    pitch = await vec.GetVectorPitch().ConfigureAwait(false); ;
                }
                 
                if (oldPitch == 0 || (oldPitch - pitch) < tolerance || (oldPitch - pitch) > -tolerance)
                {
                    // In this case the recognized vector is in tolerance an will be used to calculate the average
                    totalPitch += pitch;
                    count++;
                }
            }
            return totalPitch / count;
        }
    }
}
