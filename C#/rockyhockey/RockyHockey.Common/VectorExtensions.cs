using System;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
    /// <summary>
    /// Class that contains extension methods for the Vector class
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Stretches the Vector till the barrier
        /// If the Vector shows upwards, then the yBarrier is set to 0
        /// </summary>
        /// <param name="vec">Vector to Call the Method</param>
        /// <param name="yBarrier">position of the barrier</param>
        /// <returns>longer vector to the yBarrier</returns>
        public static async Task<Vector> StretchVectorToXCoordinate(this Vector vec, double yBarrier)
        {
            // If the Vector shows upwards, then the puck will collide with the upper barrier
            if (vec.Position.Y > vec.Direction.Y)
            {
                yBarrier = 0;
            }

            double pitch = vec.GetVectorGradient();
            // calculates the shift of the new vector direction
            double shift = vec.Position.X * pitch * (-1) + vec.Position.Y;
            return new Vector
            {
                Position = vec.Position,
                Direction = new GameFieldPosition { X = (yBarrier - shift) / pitch, Y = yBarrier }
            };
        }

        /// <summary>
        /// Stretches the Vector till the barrier
        /// If the Vector shows upwards, then the xBarrier is set to 0
        /// </summary>
        /// <param name="vec">Vector to Call the Method</param>
        /// <param name="xBarrier">position of the barrier</param>
        /// <returns>longer vector to the xBarrier</returns>
        public static Vector StretchVectorToYCoordinate(this Vector vec, double xBarrier)
        {
            // If the Vector shows to the left, then the puck will collide with the left barrier
            if (vec.Position.X > vec.Direction.X)
            {
                xBarrier = 0;
            }

            double pitch = vec.GetVectorGradient();
            // calculates the shift of the new vector direction
            double shift = vec.Position.X * pitch * (-1) + vec.Position.Y;

            return new Vector
            {
                Position = vec.Position,
                Direction = new GameFieldPosition { X = xBarrier, Y = (pitch * xBarrier) + shift }
            };
        }

        /// <summary>
        /// Calculates the reflected Vector for the Vector has called this method
        /// </summary>
        /// <param name="vec">Vector to Call the Method</param>
        /// <returns>reflected vector</returns>
        public static Vector CalculateReflectedVector(this Vector vec)
        {
            double reflectedVecPitch = vec.GetVectorGradient() * (-1);
            var reflectedVector = new Vector
            {
                Position = new GameFieldPosition { X = vec.Direction.X, Y = vec.Direction.Y }
            };

            if (vec.Direction.X > vec.Position.X)
            {
                reflectedVector.Direction = new GameFieldPosition { X = vec.Direction.X + 1, Y = vec.Direction.Y + reflectedVecPitch };
            }
            else
            {
                reflectedVector.Direction = new GameFieldPosition { X = vec.Direction.X - 1, Y = vec.Direction.Y + reflectedVecPitch };
            }

            return reflectedVector;
        }

        /// <summary>
        /// Calculates the zero vector for the surpasses vector
        /// </summary>
        /// <param name="vec">Vector to Call the Method</param>
        /// <returns>zero vector</returns>
        public static Task<Vector> GetZeroVector(this Vector vec)
        {
            return Task.Factory.StartNew(() =>
            {
                return new Vector
                {
                    Position = new GameFieldPosition { X = 0, Y = 0 },
                    Direction = new GameFieldPosition { X = vec.Direction.X - vec.Position.X, Y = vec.Direction.Y - vec.Position.Y }
                };
            });
        }

        /// <summary>
        /// Calculates the gradient of the Vector
        /// </summary>
        /// <param name="vec">Vector to Call the Method</param>
        /// <returns>gradient</returns>
        public static double GetVectorGradient(this Vector vec)
        {
            return (vec.Direction.Y - vec.Position.Y) / (vec.Direction.X - vec.Position.X);
        }

        /// <summary>
        /// Calculates the length of the Vector
        /// </summary>
        /// <param name="vec">Vector to Call the Method</param>
        /// <returns>length of the Vector</returns>
        public async static Task<double> GetVectorLength(this Vector vec)
        {
            Vector normalVector = await vec.GetZeroVector().ConfigureAwait(false);
            // Pythagoras formula for the length of the vector
            return Math.Sqrt(Math.Pow(normalVector.Direction.X, 2) + Math.Pow(normalVector.Direction.Y, 2));
        }
    }
}
