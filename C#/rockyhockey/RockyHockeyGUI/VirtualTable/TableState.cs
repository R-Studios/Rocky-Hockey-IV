using System.Numerics;

namespace RockyHockeyGUI.VirtualTable
{
    internal struct TableState
    {
        internal Vector2 Position;
        internal Vector2 Velocity;

        internal Vector2 BatPosition;

        internal TableState(Vector2 position)
        {
            Position = position;
            Velocity = new Vector2();
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
