namespace RockyHockey.Common
{
    /// <summary>
    /// GameFieldPosition with the number of the frame it was recorded
    /// </summary>
    public class TimedCoordinate : Coordinate
    {
        public TimedCoordinate() { }
        public TimedCoordinate(Coordinate coord, long timestamp = 0) : base(coord)
        {
            Timestamp = timestamp;
        }
        public TimedCoordinate(double x, double y, long timestamp = 0) : base(x, y)
        {
            Timestamp = timestamp;
        }
        public TimedCoordinate(TimedCoordinate original) : base(original)
        {
            Timestamp = original.Timestamp;
        }

        /// <summary>
        /// timestamp when the position has been detected
        /// </summary>
        public long Timestamp { get; set; }
    }
}
