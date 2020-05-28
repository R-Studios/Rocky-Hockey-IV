namespace RockyHockey.Common
{
    /// <summary>
    /// Vector with a frame number
    /// </summary>
    public class TimedVector : Vector
    {
        public TimedVector() { }
        public TimedVector(TimedCoordinate start, TimedCoordinate end, Coordinate direction) : base(start, end, direction)
        {
            calcTime();
        }

        public TimedVector(TimedCoordinate start, TimedCoordinate end) : base(start, end)
        {
            calcTime();
        }

        public long calcTime()
        {
            return NeededTime = TimedEnd.Timestamp - TimedStart.Timestamp;
        }

        /// <summary>
        /// time that passed while bat moved from start to end position
        /// </summary>
        public long NeededTime { get; private set; }
        public TimedCoordinate TimedStart { get => (TimedCoordinate)Start; }
        public TimedCoordinate TimedEnd { get => (TimedCoordinate)End; }
    }
}
