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
    /// <summary>
    /// This class is doing the actual "simulation" work for the virtual table.
    /// Since GDI rendering with WinForms is limited to 30 fps, the simulation is running on its own thread.
    /// It is using fixed time steps of 10 ms (100 ticks per second).
    /// Also see <see cref="VirtualTableView"/> for the window hosting this simulation.
    /// </summary>
    internal class VirtualTableWorker : PositionCollector
    {
        private readonly int fieldWidth, fieldHeight;
        private readonly float puckRadius;
        private readonly object lockObj = new object();
        
        private readonly TableState tableState;

        private bool shouldStop;
        private long targetElapsedMillis;
        private Stopwatch stopwatch;
        private Thread workerThread;

        internal VirtualTableWorker(int fieldWidth, int fieldHeight, float puckRadius)
        {
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;
            this.puckRadius = puckRadius;

            tableState = new TableState(new Vector2(fieldWidth * 0.75f, fieldHeight * 0.5f));
        }

        /// <summary>
        /// Allows safe access to the table state.
        /// Ensures that the code within the passed function has atomic access.
        /// (Meaning that the table simulation code can't modify the state while your code is being processed.)
        /// Example use in <see cref="VirtualTableView.GoButtonClick"/>.
        /// </summary>
        /// <param name="action">A function to run. Has a single parameter of type <see cref="TableState"/> that is safe to modify.</param>
        internal void AccessState(Action<TableState> action)
        {
            lock (lockObj)
            {
                action.Invoke(tableState);
            }
        }

        /// <summary>
        /// A convenience method to access the table state similar to <see cref="AccessState"/>.
        /// It additionally transfers the returned value of your function to its own return value.
        /// Example use in <see cref="VirtualTableView.TimerTick"/>.
        /// </summary>
        /// <typeparam name="T">The type that your function returns. Usually automatically inferred from your function.</typeparam>
        /// <param name="func">A function to run. Has a single parameter of type <see cref="TableState"/> and can return anything.</param>
        /// <returns>The value your function has returned.</returns>
        internal T Evaluate<T>(Func<TableState, T> func)
        {
            lock (lockObj)
            {
                return func.Invoke(tableState);
            }
        }

        /// <summary>
        /// Starts the table simulation and hooks into <see cref="MovementController.OnMove"/> for reading the bat position.
        /// </summary>
        internal void Start()
        {
            workerThread = new Thread(RunLoop) {Name = "Virtual Table Worker Thread", IsBackground = true};
            workerThread.Start();

            MovementController.Instance.OnMove += AxisOnMove;
        }

        /// <summary>
        /// Stops the table simulation and unhooks from the <see cref="MovementController.OnMove"/> event.
        /// It is recommended to call this method, but not strictly necessary because the worker thread is marked as background,
        /// therefore automatically terminating when the application is shutting down.
        /// </summary>
        internal void Stop()
        {
            shouldStop = true;
            MovementController.Instance.OnMove -= AxisOnMove;
            workerThread.Join();
        }

        private void AxisOnMove(double axisX, double axisY)
        {
            lock (lockObj)
            {
                // Todo: fix this issue
                // This callback is attached to the MovementController OnMove event that's triggered right before movement data is sent to the motor control unit.
                // The data is supposed to be sent as absolute distance in millimeters from the motor control's origin.
                // I have no clue what to do here, the movement data doesn't make a lot of sense.
                var size = Config.Instance.GameFieldSizeMM;

                // Convert from "real" bat position to virtual table position. See VirtualTableView class summary for details about this.
                var x = axisX * fieldWidth / size.Width;
                var y = fieldHeight - axisY * fieldHeight / size.Height;

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

        /// <summary>
        /// Runs a single simulation tick. Access to the state is safe in this entire method.
        /// </summary>
        private void Tick()
        {
            var velocity = tableState.Velocity;
            var position = tableState.Position;

            if (velocity != Vector2.Zero)
            {
                // Try to get puck unstuck if it was placed inside a wall
                position.X = Clamp(position.X, puckRadius, fieldWidth - puckRadius);
                position.Y = Clamp(position.Y, puckRadius, fieldHeight - puckRadius);
                
                // Move puck
                position += velocity;
            
                // Bounce off the short sides
                if (position.X < puckRadius)
                {
                    position.X += 2 * (puckRadius - position.X);
                    velocity.X *= -1;
                }
                else if (position.X > fieldWidth - puckRadius)
                {
                    position.X -= 2 * (position.X - fieldWidth + puckRadius);
                    velocity.X *= -1;
                }

                // Bounce off the long sides
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

                // Apply friction
                velocity *= 1 - tableState.Friction;
                
                // Puck has become slow enough to completely stop
                if (velocity.Length() < 0.2)
                {
                    velocity = Vector2.Zero;
                }

                // Write the new values back to the state
                tableState.Velocity = velocity;
                tableState.Position = position;
            }
        }

        /// <summary>
        /// Used by path prediction.
        /// </summary>
        /// <returns>A list of seven <see cref="TimedCoordinate"/>s collected over 70 milliseconds from the table state.</returns>
        public override List<TimedCoordinate> GetPuckPositions()
        {
            var positions = new List<TimedCoordinate>(7);

            for (var i = 0; i < positions.Capacity; i++)
            {
                var puckPosition = GetPuckPosition();
                puckPosition.Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                positions.Add(puckPosition);

                Thread.Sleep(10);
            }

            // Possibly simulate delay from image processing? Not sure if that's a good thing to do.
            //Thread.Sleep(20);

            return positions;
        }

        /// <summary>
        /// Also used by path prediction.
        /// </summary>
        /// <returns>A single <see cref="TimedCoordinate"/> derived from the current table state.</returns>
        public override TimedCoordinate GetPuckPosition()
        {
            lock (lockObj)
            {
                var size = Config.Instance.GameFieldSize;
                // Convert from virtual table position to "real" position. See VirtualTableView class summary for details about this.
                var x = tableState.Position.X * size.Width / fieldWidth;
                var y = (fieldHeight - tableState.Position.Y) * size.Height / fieldHeight;

                return new TimedCoordinate(x, y);
            }
        }

        public override void StopMotionCapturing() { }

        private static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }
    }
}
