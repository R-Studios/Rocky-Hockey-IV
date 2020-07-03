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
        
        private readonly TableState tableState;

        private bool shouldStop;
        private long targetElapsedMillis;
        private Stopwatch stopwatch;
        private Thread workerThread;

        internal VirtualTableWorker(int fieldWidth, int fieldHeight, int puckRadius)
        {
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;
            this.puckRadius = puckRadius;

            tableState = new TableState(new Vector2(fieldWidth * 0.25f, fieldHeight * 0.5f));
        }

        internal void AccessState(Action<TableState> action)
        {
            lock (lockObj)
            {
                action.Invoke(tableState);
            }
        }

        internal T Evaluate<T>(Func<TableState, T> func)
        {
            lock (lockObj)
            {
                return func.Invoke(tableState);
            }
        }

        internal void Start()
        {
            workerThread = new Thread(RunLoop) {Name = "Virtual Table Worker Thread", IsBackground = true};
            workerThread.Start();

            MovementController.Instance.OnMove += AxisOnMove;
        }

        internal void Stop()
        {
            shouldStop = true;
            MovementController.Instance.OnMove -= AxisOnMove;
            workerThread.Join();
        }

        private void AxisOnMove(double x, double y)
        {
            lock (lockObj)
            {
                tableState.BatPosition = new Vector2((float) x, (float) y);
            }
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

                velocity *= 1 - tableState.Friction;

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
                    positions.Add(new TimedCoordinate(tableState.Position.X, fieldHeight - tableState.Position.Y, DateTimeOffset.Now.ToUnixTimeMilliseconds()));
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
