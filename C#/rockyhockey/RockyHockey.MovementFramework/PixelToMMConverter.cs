using System.Drawing;

namespace RockyHockey.MovementFramework
{
    /// <summary>
    /// Holds functionality for converting pixels into mm
    /// </summary>
    public class PixelToMMConverter
    {
        /// <summary>
        /// Calculates the Factor to calculate pixels into mm
        /// </summary>
        /// <param name="gameFieldSize">Size of the game fiels</param>
        /// <returns>factor for pixel to mm</returns>
        public double GetFactor(Size gameFieldSize)
        {
            double factor = 900.0 / gameFieldSize.Height;
            return factor;
        }
    }
}
