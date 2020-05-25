namespace RockyHockey.Common
{
    /// <summary>
    /// GameFieldPosition with the number of the frame it was recorded
    /// </summary>
    public class TimedCoordinate : Coordinate
    {
        /// <summary>
        /// timestamp when the position has been detected
        /// </summary>
        public long Timestamp { get; set; }
    }
}
