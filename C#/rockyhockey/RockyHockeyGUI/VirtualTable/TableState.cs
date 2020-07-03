using System.Numerics;

namespace RockyHockeyGUI.VirtualTable
{
    internal class TableState
    {
        /// <summary>
        /// Position of the puck on the play field.
        /// </summary>
        internal Vector2 Position;

        /// <summary>
        /// Velocity of the puck.
        /// <c>Vector2.Zero</c> means the puck is stationary.
        /// </summary>
        internal Vector2 Velocity;

        internal Vector2 BatPosition;

        /// <summary>
        /// Reduces puck velocity to simulate friction.
        /// Measured in relative speed lost per simulation tick.
        /// Not super realistic but should work well enough.
        /// Should be within the range of 0.00 to 0.01.
        /// </summary>
        internal float Friction;

        internal TableState(Vector2 position)
        {
            Position = position;
            BatPosition = new Vector2(-1000, -1000);
        }

        internal bool IsPuckStationary
        {
            get => Velocity == Vector2.Zero;
            set
            {
                if (value)
                {
                    Velocity = Vector2.Zero;
                }
            }
        }
    }
}
