using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using RockyHockey.MovementFramework;

namespace RockyHockeyGUI.VirtualTable
{
    internal class VirtualTableWorker : PositionCollector
    {
        private readonly int fieldWidth, fieldHeight, puckRadius;
        private readonly object lockObj = new object();

        private bool shouldStop;
        private long targetElapsedMillis;
        private Stopwatch stopwatch;
        private Thread workerThread;

        private TableState tableState;

        private Vector2 position;
        private Vector2 velocity;
        private float friction;

        internal TableState TableState
        {
            get
            {
                lock (lockObj)
                {
                    return tableState;
                }
            }
            set
            {
                lock (lockObj)
                {
                    tableState = value;
                }
            }
        }

        /// <summary>
        /// Position of the puck on the play field.
        /// </summary>
        internal Vector2 Position
        {
            get
            {
                lock (lockObj)
                {
                    return position;
                }
            }
            set
            {
                lock (lockObj)
                {
                    position = value;
                }
            }
        }

        /// <summary>
        /// Velocity of the puck.
        /// <c>Vector2.Zero</c> means the puck is stationary.
        /// </summary>
        internal Vector2 Velocity
        {
            get
            {
                lock (lockObj)
                {
                    return velocity;
                }
            }
            set
            {
                lock (lockObj)
                {
                    velocity = value;
                }
            }
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

        /// <summary>
        /// Reduces puck velocity to simulate friction.
        /// Measured in relative speed lost per tick.
        /// Not super realistic but should work well enough.
        /// Should be within the range of 0.00 to 0.01.
        /// </summary>
        internal float Friction
        {
            get
            {
                lock (lockObj)
                {
                    return friction;
                }
            }
            set
            {
                lock (lockObj)
                {
                    friction = value;
                }
            }
        }

        internal VirtualTableWorker(int fieldWidth, int fieldHeight, int puckRadius)
        {
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;
            this.puckRadius = puckRadius;

            position = new Vector2(fieldWidth * 0.25f, fieldHeight * 0.5f);
        }

        internal void Start()
        {
            workerThread = new Thread(RunLoop) {Name = "Virtual Table Worker Thread", IsBackground = true};
            workerThread.Start();

            MovementController.Instance.OnMove = (x, y) => 
            {
                x0TextBox.Text = x.ToString();
                y0TextBox.Text = y.ToString();
            };
        }

        internal void Stop()
        {
            shouldStop = true;
            workerThread.Join();
        }

        private void RunLoop()
        {
            stopwatch = Stopwatch.StartNew();

            while (!shouldStop)
            {
                lock (lockObj)
                {
                    Tick();
                }
                
                targetElapsedMillis += 10; // Time between ticks, 10 ms = 100 ticks/s
                var sleepTime = (int) (targetElapsedMillis - stopwatch.ElapsedMilliseconds);

                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
            }
        }

        private void Tick()
        {
            var velocity = tableState.Velocity;
            var position = tableState.Position;

            if (velocity != Vector2.Zero)
            {
                position += velocity;
            
                if (position.X < puckRadius)
                {
                    tableState.Position.X += 2 * (puckRadius - position.X);
                    velocity.X *= -1;
                }
                else if (position.X > fieldWidth - puckRadius)
                {
                    position.X -= 2 * (position.X - fieldWidth + puckRadius);
                    velocity.X *= -1;
                }

                if (position.Y < puckRadius)
                {
                    position.Y += 2 * (puckRadius - position.Y);
                    velocity.Y *= -1;
                }
                else if (position.Y > fieldHeight - puckRadius)
                {
                    position.Y -= 2 * (position.Y - fieldHeight + puckRadius);
                    velocity.Y *= -1;
                }

                velocity *= 1 - friction;

                if (velocity.Length() < 0.2)
                {
                    // Puck has come to a halt
                    velocity = Vector2.Zero;
                }

                tableState.Velocity = velocity;
                tableState.Position = position;
            }
        }

        public override List<TimedCoordinate> GetPuckPositions()
        {
            var positions = new List<TimedCoordinate>(7);

            for (var i = 0; i < positions.Capacity; i++)
            {
                lock (lockObj)
                {
                    positions.Add(new TimedCoordinate(position.X, fieldHeight - position.Y, DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                }

                Thread.Sleep(10);
            }

            //Thread.Sleep(20);

            return positions;
        }

        public override TimedCoordinate GetPuckPosition()
        {
            lock (lockObj)
            {
                return new TimedCoordinate(tableState.Position.X, fieldHeight - tableState.Position.Y);
            }
        }

        public override void StopMotionCapturing() { }
    }
}
