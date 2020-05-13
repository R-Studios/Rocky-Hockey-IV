namespace RockyHockey.Common
{
    /// <summary>
    /// Klasse zum Speichern der Position eines Schlägers bzw. des Pucks
    /// </summary>
    public class GameFieldPosition
    {
        /// <summary>
        /// X-Position
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y-Position
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// timestamp when the position has been detected
        /// </summary>
        public long Timestamp { get; set;  }
    }
}
