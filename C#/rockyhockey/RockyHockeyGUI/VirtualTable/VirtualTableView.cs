using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;
using RockyHockey.Common;
using RockyHockey.MovementFramework;

namespace RockyHockeyGUI.VirtualTable
{
    /// <summary>
    /// This window hosts and interacts with a virtual table simulation (<see cref="VirtualTableWorker"/>).
    /// The virtual table is modeling the real table, bat and puck at a scale of 2 mm : 1 pixel (at 100% display scale).
    /// Since the table size is defined like this, all coordinates going in and out have to be scaled to the play field size in the <see cref="Config"/>.
    /// In addition to scaling, the virtual table, path prediction and motor controls all have their origin - point (0,0) - in different places.
    /// The virtual table has its origin at the top-left corner. This is the left corner when standing on the player side.
    /// Path prediction use the bottom-left corner. This is the right corner of the player side.
    /// Motor controls have it at the top-right corner. This is the right corner when standing on the robot side.
    /// </summary>
    public partial class VirtualTableView : Form
    {
        private const int FieldWidth = 800; //       1600 mm
        private const int FieldHeight = 450; //       900 mm
        private const float BatDiameter = 47.5f; //    95 mm
        private const float BatRadius = 23.75f; //   47.5 mm

        // Not quite sure about the puck size - forgot to measure... Standardized puck sizes are 63 and 50 mm.
        // Putting 63 mm for now but I have a feeling it might be wrong.
        // Dear future maintainers: If you see this note and know the actual size, please change these constants here.
        private const float PuckDiameter = 31.5f; //   63 mm
        private const float PuckRadius = 15.75f; //  31.5 mm
        //private const float PuckDiameter = 25.5f; //   50 mm
        //private const float PuckRadius = 12.25f; //  25.5 mm

        private int mouseHeldX, mouseHeldY;

        private bool isMouseHeld;
        private bool hasVelocityLine;

        internal VirtualTableWorker Table { get; }

        /// <summary>
        /// Creates a new virtual table window.
        /// Automatically sets up the table simulation.
        /// </summary>
        public VirtualTableView()
        {
            InitializeComponent();

            // Enable double buffering on the panel to eliminate flickering. Has to be done via reflection because DoubleBuffered is protected in Panel.
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel,
                new object[] {true});

            Table = new VirtualTableWorker(FieldWidth, FieldHeight, PuckRadius);
            Table.Start();

            TrackBarScroll(trackBar, EventArgs.Empty);
            ActiveControl = goButton;

            MovementController.Instance.OnMove += AxisOnMove;
        }
        
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            MovementController.Instance.OnMove -= AxisOnMove;
            Table.Stop();
        }
        
        private void PanelPaint(object sender, PaintEventArgs e)
        {
            var puckPos = Vector2.Zero;
            var batPos = Vector2.Zero;

            Table.AccessState(state =>
            {
                puckPos = state.Position;
                batPos = state.BatPosition;
            });

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(Color.DarkGreen), puckPos.X - PuckRadius, puckPos.Y - PuckRadius,
                PuckDiameter, PuckDiameter);
            e.Graphics.FillEllipse(new SolidBrush(Color.MediumBlue), batPos.X - BatRadius, batPos.Y - BatRadius,
                BatDiameter, BatDiameter);

            if (hasVelocityLine)
            {
                e.Graphics.DrawLine(new Pen(Color.Red, 3), puckPos.X, puckPos.Y, mouseHeldX, mouseHeldY);
            }
        }

        private void PanelMouseDown(object sender, MouseEventArgs e)
        {
            var isStationary = false;

            Table.AccessState(state =>
            {
                isStationary = state.IsPuckStationary;

                if (isStationary)
                {
                    state.Position = new Vector2(e.X, e.Y);
                }
            });

            if (isStationary)
            {
                mouseHeldX = e.X;
                mouseHeldY = e.Y;

                isMouseHeld = true;
                hasVelocityLine = true;

                panel.Invalidate();
                UpdateTextboxes();
            }
        }

        private void PanelMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseHeld)
            {
                mouseHeldX = e.X;
                mouseHeldY = e.Y;

                panel.Invalidate();
                UpdateTextboxes();
            }
        }

        private void PanelMouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseHeld)
            {
                isMouseHeld = false;

                goButton.Enabled = true;

                panel.Invalidate();
                UpdateTextboxes();
            }
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            if (hasVelocityLine)
            {
                Table.AccessState(state =>
                {
                    var position = state.Position;
                    state.Velocity =
                        new Vector2(mouseHeldX - position.X, mouseHeldY - position.Y) / 25; // 0.25 * ticks/s
                });

                hasVelocityLine = false;

                goButton.Enabled = false;
                stopButton.Enabled = true;
            }
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            Table.AccessState(state => state.IsPuckStationary = true);
            stopButton.Enabled = false;
        }

        private void TrackBarScroll(object sender, EventArgs e)
        {
            Table.AccessState(state => state.Friction = trackBar.Value * 0.001f);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (Table.Evaluate(state => state.IsPuckStationary))
            {
                stopButton.Enabled = false;
            }

            panel.Invalidate();
        }

        private void UpdateTextboxes()
        {
            var position = Table.Evaluate(state => state.Position);

            x0TextBox.Text = position.X.ToString(CultureInfo.InvariantCulture);
            y0TextBox.Text = position.Y.ToString(CultureInfo.InvariantCulture);

            x1TextBox.Text = mouseHeldX.ToString();
            y1TextBox.Text = mouseHeldY.ToString();
        }

        private void AxisOnMove(double x, double y)
        {
            Invoke(new Action(() =>
            {
                xBatTextBox.Text = x.ToString(CultureInfo.InvariantCulture);
                yBatTextBox.Text = y.ToString(CultureInfo.InvariantCulture);
            }));
        }
    }
}
