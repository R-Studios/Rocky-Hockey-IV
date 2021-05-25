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
        public ArrayList axiscoordinatelist = new ArrayList();


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
            DetermineGameField();
            this.Close();
        }

        private void DetermineGameField()
        {
            Config.Instance.GameField.UpperLeft = (System.Drawing.Point)coordinatelist[0];
            Config.Instance.GameField.UpperRight = (System.Drawing.Point)coordinatelist[1];
            Config.Instance.GameField.LowerRight = (System.Drawing.Point)coordinatelist[2];
            Config.Instance.GameField.LowerLeft = (System.Drawing.Point)coordinatelist[3];
            SaveAxes();
        }

        private void SaveAxes()
        {
            int smallestx = 0;
            int highesty = 0;
            int smallesty = 0;
            int highestx = 0;


            int min = int.MaxValue;
            for (int i = 0; i < coordinatelist.Count; i++)
            {
                System.Drawing.Point p = (System.Drawing.Point)coordinatelist[i];

                if (p.X < min)
                {
                    min = p.X;
                    smallestx = p.X;
                }

            }

            int max = int.MinValue;
            for (int i = 0; i < coordinatelist.Count; i++)
            {
                System.Drawing.Point p = (System.Drawing.Point)coordinatelist[i];

                if (p.Y > max)
                {
                    max = p.Y;
                    highesty = p.Y;
                }

            }

            min = int.MaxValue;
            for (int i = 0; i < coordinatelist.Count; i++)
            {
                System.Drawing.Point p = (System.Drawing.Point)coordinatelist[i];

                if (p.Y < min)
                {
                    min = p.Y;
                    smallesty = p.Y;
                }
            }

            max = int.MinValue;
            for (int i = 0; i < coordinatelist.Count; i++)
            {
                System.Drawing.Point p = (System.Drawing.Point)coordinatelist[i];

                if (p.X > max)
                {
                    max = p.X;
                    highestx = p.X;
                }
            }

            System.Drawing.Point smallestxhighesty = new System.Drawing.Point(smallestx, highesty); // Hier ist der 0 Punkt im Koordinatensystem
            axiscoordinatelist.Add(smallestxhighesty);
            Config.Instance.GameField.Offset = smallestxhighesty;
            System.Drawing.Point point = new System.Drawing.Point(smallestxhighesty.X - smallestxhighesty.X, smallestxhighesty.Y - smallestxhighesty.Y);
            Config.Instance.GameField.XYOrigin = point;

            System.Drawing.Point smallestxsmallesty = new System.Drawing.Point(smallestx, smallesty); // Hier ist der Punkt ganz oben links
            axiscoordinatelist.Add(smallestxsmallesty);
            point = new System.Drawing.Point(smallestxsmallesty.X - smallestxhighesty.X, smallestxsmallesty.Y - smallestxhighesty.Y);
            Config.Instance.GameField.ExtremeY = point;

            System.Drawing.Point highestxhighesty = new System.Drawing.Point(highestx, highesty); // Hier ist der Punkt ganz unten rechts
            axiscoordinatelist.Add(highestxhighesty);
            point = new System.Drawing.Point(highestxhighesty.X - smallestxhighesty.X, highestxhighesty.Y - smallestxhighesty.Y);
            Config.Instance.GameField.ExtremeX = point;

        }
    }
}
