using System;

namespace RockyHockey.Common
{
    /// <summary>
    /// Klasse, die einen 2D-Vektor repräsentiert
    /// </summary>
    public class Vector
    {
        public Vector() { }

        public Vector(Coordinate start, Coordinate end)
        {
            init(start, end);
        }

        public Vector(Vector original)
        {
            init(original.Start, original.End);
        }

        protected void init<T>(T start, T end) where T : Coordinate
        {
            object[] args = { start };
            Start = (T)Activator.CreateInstance(typeof(T), args);

            args[0] = end;
            End = (T)Activator.CreateInstance(typeof(T), args);

            calcDirection();
            calcLength();
        }

        private double calcLength()
        {
            if (VectorDirection == null)
                calcDirection();

            Position = new GameFieldPosition
            {
                X = Start.X,
                Y = Start.Y
            };
            Direction = new GameFieldPosition
            {
                X = End.X,
                Y = End.Y
            };

            return Length = Math.Sqrt(Math.Pow(VectorDirection.X, 2) + Math.Pow(VectorDirection.Y, 2));
        }

        private void calcDirection()
        {
            VectorDirection = new Coordinate
            {
                X = End.X - Start.X,
                Y = End.Y - Start.Y
            };
        }
        /// <summary>
        /// X-Achse
        /// </summary>
        public GameFieldPosition Position { get; set; }

        /// <summary>
        /// X-Length
        /// </summary>
        public GameFieldPosition Direction { get; set; }

        public Coordinate Start { get; private set; }
        public Coordinate End { get; private set; }
        public Coordinate VectorDirection { get; private set; }
        public double Length { get; private set; }

        /// <summary>
        /// checks if motion is towards robot side
        /// </summary>
        /// <returns></returns>
        public bool towardsRobot()
        {
            bool retval = false;

            if (Direction.Y < 0)
                retval = true;

            return retval;
        }
    }
}
