namespace RockyHockey.Common
{
    /// <summary>
    /// Vector with its velocity
    /// </summary>
    public class VelocityVector : TimedVector
    {
        public VelocityVector() { }

        public VelocityVector(TimedCoordinate start, TimedCoordinate end) : base(start, end)
        {
            calcVelocity();
        }
        public VelocityVector(VelocityVector original) : base(original)
        {
            Velocity = original.Velocity;
        }

        private double calcVelocity()
        {
            return Velocity = Length / NeededTime;
        }

        /// <summary>
        /// Velocity in pixel per second
        /// </summary>
        public double Velocity { get; set; }
    }
}