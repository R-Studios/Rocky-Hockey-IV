using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection;
using RockyHockey.Common;

using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;

using AForge.Vision;
using System.Drawing.Drawing2D;


namespace RockyHockeyGUI
{
    public partial class GameFieldDetectionView : Form
    {
        public GameFieldDetectionView(Bitmap imgInput)
        {
            InitializeComponent();
            this.imgInput = imgInput;
            RectanglePicBox.Image = this.imgInput;
        }
        Bitmap imgInput;
        public ArrayList coordinatelist = new ArrayList();
        

        private void RectanglePicBox_Click(object sender, EventArgs e)
        {
            if (coordinatelist.Count <= 3)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                System.Drawing.Point coordinates = me.Location;
                coordinatelist.Add(coordinates);
                textBox1.Text += "" + coordinates;

                var X = me.Location.X;
                var Y = me.Location.Y;


                drawPoint(X, Y);
            }
        }

        public void drawPoint(int x, int y)
        {
            Graphics g = Graphics.FromHwnd(RectanglePicBox.Handle);
            SolidBrush brush = new SolidBrush(Color.LimeGreen);
            System.Drawing.Point dPoint = new System.Drawing.Point(x, y);
            Rectangle rect = new Rectangle(dPoint, new Size(4, 4));
            g.FillRectangle(brush, rect);
            g.Dispose();
        }

        private void DrawRectangleButton_Click(object sender, EventArgs e)
        {
            if (coordinatelist.Count > 0)
            {
                Graphics g = Graphics.FromHwnd(RectanglePicBox.Handle);
                Pen pen = new Pen(Color.Green, 3);

                for (int i = 0; i < coordinatelist.Count - 1; i++)
                {
                    System.Drawing.Point p1 = (System.Drawing.Point)coordinatelist[i];
                    System.Drawing.Point p2 = (System.Drawing.Point)coordinatelist[i + 1];
                    g.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);

                }

                System.Drawing.Point k1 = (System.Drawing.Point)coordinatelist[0];
                System.Drawing.Point k2 = (System.Drawing.Point)coordinatelist[3];
                g.DrawLine(pen, k1.X, k1.Y, k2.X, k2.Y);
            }
        }

        private void ResetCoordinatesButton_Click(object sender, EventArgs e)
        {
            coordinatelist = new ArrayList();
            textBox1.Text = "";
            RectanglePicBox.Image = imgInput;
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            Config.Instance.Upp
            this.Close();
        }
    }
}
