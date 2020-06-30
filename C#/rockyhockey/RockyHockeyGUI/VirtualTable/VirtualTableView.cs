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

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Table.Stop();
            base.OnFormClosing(e);
        }

        protected void PanelPaint(object sender, PaintEventArgs e)
        {
            var position = Table.Position;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(Color.Black), position.X - puckDiameter / 2f, position.Y - puckDiameter / 2f,
                puckDiameter, puckDiameter);

            if (hasVelocityLine)
            {
                e.Graphics.DrawLine(new Pen(Color.Red, 3), position.X, position.Y, mouseHeldX, mouseHeldY);
            }
        }

        protected void PanelMouseDown(object sender, MouseEventArgs e)
        {
            if (Table.IsPuckStationary)
            {
                Table.Position = new Vector2(e.X, e.Y);

                mouseHeldX = e.X;
                mouseHeldY = e.Y;

                isMouseHeld = true;
                hasVelocityLine = true;

                panel.Invalidate();
                UpdateTextboxes();
            }
        }

        protected void PanelMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseHeld)
            {
                mouseHeldX = e.X;
                mouseHeldY = e.Y;

                panel.Invalidate();
                UpdateTextboxes();
            }
        }

        protected void PanelMouseUp(object sender, MouseEventArgs e)
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
                var position = Table.Position;

                Table.Velocity = new Vector2(mouseHeldX - position.X, mouseHeldY - position.Y) / 25; // 1/4 * ticks/s

                hasVelocityLine = false;

                goButton.Enabled = false;
                stopButton.Enabled = true;
            }
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            Table.IsPuckStationary = true;
            stopButton.Enabled = false;
        }

        private void TrackBarScroll(object sender, EventArgs e)
        {
            Table.Friction = trackBar.Value * 0.001f;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (Table.IsPuckStationary)
            {
                stopButton.Enabled = false;
            }

            panel.Invalidate();
        }

        private void UpdateTextboxes()
        {
            var position = Table.Position;

            //x0TextBox.Text = position.X.ToString();
            //y0TextBox.Text = position.Y.ToString();

            //x1TextBox.Text = mouseHeldX.ToString();
            //y1TextBox.Text = mouseHeldY.ToString();
        }
    }
}
