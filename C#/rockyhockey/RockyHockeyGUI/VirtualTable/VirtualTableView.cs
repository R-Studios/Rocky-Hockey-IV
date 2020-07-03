using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;
using RockyHockey.MovementFramework;

namespace RockyHockeyGUI.VirtualTable
{
    public partial class VirtualTableView : Form
    {
        // Todo: load from config
        private readonly int fieldWidth = 785;
        private readonly int fieldHeight = 437;
        private readonly int puckRadius = 10;
        private readonly int puckDiameter = 20;

        private int mouseHeldX;
        private int mouseHeldY;
        
        private bool isMouseHeld;
        private bool hasVelocityLine;
        
        internal VirtualTableWorker Table { get; private set; }

        public VirtualTableView()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel,
                new object[] {true});
            
            Table = new VirtualTableWorker(fieldWidth, fieldHeight, puckRadius);
            Table.Start();

            TrackBarScroll(trackBar, EventArgs.Empty);
            ActiveControl = label1;

            MovementController.Instance.OnMove += MyAxisOnMove;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            MovementController.Instance.OnMove -= MyAxisOnMove;
            Table.Stop();
            base.OnFormClosing(e);
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
            e.Graphics.FillEllipse(new SolidBrush(Color.DarkGreen), puckPos.X - puckRadius, puckPos.Y - puckRadius,
                puckDiameter, puckDiameter);
            e.Graphics.FillEllipse(new SolidBrush(Color.MediumBlue), batPos.X - puckRadius, batPos.Y - puckRadius,
                puckDiameter, puckDiameter);

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
                    state.Velocity = new Vector2(mouseHeldX - position.X, mouseHeldY - position.Y) / 25; // 1/4 * ticks/s
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

            x0TextBox.Text = position.X.ToString();
            y0TextBox.Text = position.Y.ToString();

            x1TextBox.Text = mouseHeldX.ToString();
            y1TextBox.Text = mouseHeldY.ToString();
        }

        private void MyAxisOnMove(double x, double y)
        {
            Invoke(new Action(() =>
            {
                xBatTextBox.Text = x.ToString();
                yBatTextBox.Text = y.ToString();
            }));
        }
    }
}
