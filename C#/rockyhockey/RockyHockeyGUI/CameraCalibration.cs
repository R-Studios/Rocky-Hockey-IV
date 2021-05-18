using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockyHockeyGUI
{
    public partial class CameraCalibration : Form
    {
        ImageProvider reader;
        CameraConfig config = new CameraConfig("");

        bool cantChangeReader = false;
        public CameraCalibration(ImageProvider reader = null)
        {
            InitializeComponent();

            if (reader != null)
            {
                cantChangeReader = true;

                ConfigFieldsize_CheckBox.Enabled = false;

                ResolutionCalibration_Panel.Visible = false;
            }

            this.reader = reader ?? new CameraReader();
            clickHandler = pictureBox1_Click_TopLeftCorner;

            TopLeft_Radio.Checked = true;
            DrawLines_CheckBox.Checked = true;

            FormClosing += formClosing;

            ZoomFactor_ComboBox.Items.Add("100%");
            ZoomFactor_ComboBox.Items.Add("200%");
            ZoomFactor_ComboBox.Items.Add("400%");
            ZoomFactor_ComboBox.Items.Add("800%");
            ZoomFactor_ComboBox.SelectedIndex = ZoomFactor_ComboBox.FindStringExact("100%");

            Rotation_ComboBox.Items.Add("0");
            Rotation_ComboBox.Items.Add("90");
            Rotation_ComboBox.Items.Add("180");
            Rotation_ComboBox.Items.Add("270");
            Rotation_ComboBox.SelectedIndex = Rotation_ComboBox.FindStringExact("0");
        }

        bool drawLines = true;

        double zoomFactor = 1;

        Action<object, EventArgs> clickHandler;

        bool topLeftSet = false;
        bool topRightSet = false;
        bool bottomRightSet = false;
        bool bottomLeftSet = false;

        private void button1_Click(object sender, EventArgs e)
        {
            setImage(reader.getTimedImage().GetImage());
        }

        private void setImage(Bitmap image)
        {
            if (image != null)
            {
                config.Resolution = new Size(image.Width, image.Height);
                updateResolutionBoxes();

                Bitmap tempMap = new Bitmap((int)(image.Width * zoomFactor), (int)(image.Height * zoomFactor));
                tempMap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                Graphics tempGraphics = Graphics.FromImage(tempMap);
                tempGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                tempGraphics.DrawImage(image, new Rectangle(0, 0, tempMap.Width, tempMap.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

                pictureBox1.Width = tempMap.Width;
                pictureBox1.Height = tempMap.Height;
                pictureBox1.Image = tempMap;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            clickHandler(sender, e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEvent = (MouseEventArgs)e;
            PreciewClickLocation.Text = "( " + mouseEvent.X + " / " + mouseEvent.Y + " )";
        }

        private void LoadConfig_Button_Click(object sender, EventArgs e)
        {
            config = Config.Instance.Camera1;

            topLeftSet = false;
            topRightSet = false;
            bottomRightSet = false;
            bottomLeftSet = false;
            setTextBoxes(config.UpperLeft, TopLeft_X, TopLeft_Y);
            setTextBoxes(config.UpperRight, TopRight_X, TopRight_Y);
            setTextBoxes(config.LowerRight, BottomRight_X, BottomRight_Y);
            setTextBoxes(config.LowerLeft, BottomLeft_X, BottomLeft_Y);
            topLeftSet = true;
            topRightSet = true;
            bottomRightSet = true;
            bottomLeftSet = true;
            Rotation_ComboBox.SelectedIndex = Rotation_ComboBox.FindStringExact(config.ImageRotation.ToString());
            DisplaysOrigin_CheckBox.Checked = config.displaysOrigin;
            updateResolutionBoxes();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click_TopLeftCorner(object sender, EventArgs e)
        {
            topLeftSet = false;
            setCorner(config.UpperLeft, TopLeft_X, TopLeft_Y, (MouseEventArgs)e);
            topLeftSet = true;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click_TopRightCorner(object sender, EventArgs e)
        {
            topRightSet = false;
            setCorner(config.UpperRight, TopRight_X, TopRight_Y, (MouseEventArgs)e);
            topRightSet = true;

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click_BottomRightCorner(object sender, EventArgs e)
        {
            bottomRightSet = false;
            setCorner(config.LowerRight, BottomRight_X, BottomRight_Y, (MouseEventArgs)e);
            bottomRightSet = true;

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click_BottomLeftCorner(object sender, EventArgs e)
        {
            bottomLeftSet = false;
            setCorner(config.LowerLeft, BottomLeft_X, BottomLeft_Y, (MouseEventArgs)e);
            bottomLeftSet = true;

            pictureBox1.Invalidate();
        }

        private void setCorner(Coordinate position, TextBox x, TextBox y, MouseEventArgs mouseEvent)
        {
            position.X = mouseEvent.X / zoomFactor;
            position.Y = mouseEvent.Y / zoomFactor;

            setTextBoxes(position, x, y);
        }

        private void setTextBoxes(Coordinate position, TextBox x, TextBox y)
        {
            x.Text = position.X.ToString();
            y.Text = position.Y.ToString();
        }

        private void drawDot(Graphics canvas, Coordinate position, Brush brush)
        {
            canvas.FillRectangle(brush, (float)(position.X * zoomFactor), (float)(position.Y * zoomFactor), (int)zoomFactor, (int)zoomFactor);
        }

        private void drawLine(Graphics canvas, Coordinate start, Coordinate end, Pen pen)
        {
            canvas.DrawLine(pen, (float)(start.X * zoomFactor), (float)(start.Y * zoomFactor), (float)(end.X * zoomFactor), (float)(end.Y * zoomFactor));
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Red);

            Pen pen = new Pen(brush, 3f);

            if (topLeftSet)
                drawDot(e.Graphics, config.UpperLeft, brush);

            if (topRightSet)
                drawDot(e.Graphics, config.UpperRight, brush);

            if (bottomRightSet)
                drawDot(e.Graphics, config.LowerRight, brush);

            if (bottomLeftSet)
                drawDot(e.Graphics, config.LowerLeft, brush);

            if (drawLines)
            {
                if (topLeftSet && topRightSet)
                    drawLine(e.Graphics, config.UpperLeft, config.UpperRight, pen);

                if (bottomLeftSet && bottomRightSet)
                    drawLine(e.Graphics, config.LowerRight, config.LowerLeft, pen);

                if (topLeftSet && bottomLeftSet)
                    drawLine(e.Graphics, config.UpperLeft, config.LowerLeft, pen);

                if (bottomRightSet && topRightSet)
                    drawLine(e.Graphics, config.UpperRight, config.LowerRight, pen);
            }

            int widthTop = (int)(config.UpperRight.X - config.UpperLeft.X);
            int widthBottom = (int)(config.LowerRight.X - config.LowerLeft.X);

            int heightLeft = (int)(config.LowerLeft.Y - config.UpperLeft.Y);
            int heightRight = (int)(config.LowerRight.Y - config.UpperRight.Y);

            config.FieldView = new Size(widthTop < widthBottom ? widthBottom : widthTop, heightLeft < heightRight ? heightRight : heightLeft);

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            clickHandler = pictureBox1_Click_TopLeftCorner;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            clickHandler = pictureBox1_Click_TopRightCorner;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            clickHandler = pictureBox1_Click_BottomRightCorner;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            clickHandler = pictureBox1_Click_BottomLeftCorner;
        }

        private async void ConfigFieldsize_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            newReader();
        }

        private async void newReader()
        {
            if (!cantChangeReader)
            {
                GrabImage_Button.Enabled = false;

                await Task.Factory.StartNew(() =>
                {
                    reader.finalize();
                    reader = new CameraReader(ConfigFieldsize_CheckBox.Checked, config);
                });

                GrabImage_Button.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            drawLines = ((CheckBox)sender).Checked;

            pictureBox1.Invalidate();
        }

        private void textBoxBackground(TextBox box, Color color)
        {
            box.BackColor = color;
            box.Invalidate();
        }

        private void formClosing(object sender, EventArgs e)
        {
            if (!cantChangeReader)
                reader.finalize();
        }

        private bool validSettings()
        {
            bool retval = true;

            retval &= topLeftSet;

            if (!topLeftSet)
            {
                textBoxBackground(TopLeft_X, Color.Red);
                textBoxBackground(TopLeft_Y, Color.Red);
            }

            retval &= topRightSet;

            if (!topRightSet)
            {
                textBoxBackground(TopRight_X, Color.Red);
                textBoxBackground(TopRight_Y, Color.Red);
            }

            retval &= bottomLeftSet;

            if (!bottomLeftSet)
            {
                textBoxBackground(BottomLeft_X, Color.Red);
                textBoxBackground(BottomLeft_Y, Color.Red);
            }

            retval &= bottomRightSet;

            if (!bottomRightSet)
            {
                textBoxBackground(BottomRight_X, Color.Red);
                textBoxBackground(BottomRight_Y, Color.Red);
            }

            return retval;
        }

        private void ShowResult_Button_Click(object sender, EventArgs e)
        {
            if (validSettings())
            {
                ImageProcessing processing = new ImageProcessing(reader.lastCapture.image.Clone(), true, config);

                pictureBox2.Width = processing.Original.Width;
                pictureBox2.Height = processing.Original.Height;
                pictureBox2.Image = processing.Original.Bitmap;
            }
        }

        private bool getInput(TextBox sender, out double value)
        {
            bool retval;
            if (retval = double.TryParse(sender.Text, out value))
                sender.BackColor = Color.White;
            else
                sender.BackColor = Color.Red;

            sender.Invalidate();

            return retval;
        }

        private bool getInput(TextBox sender, out int value)
        {
            bool retval;
            if (retval = int.TryParse(sender.Text, out value))
                sender.BackColor = Color.White;
            else
                sender.BackColor = Color.Red;

            sender.Invalidate();

            return retval;
        }

        private void TopLeft_X_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (getInput((TextBox)sender, out value))
            {
                topLeftSet = true;
                config.UpperLeft.X = value;
                pictureBox1.Invalidate();
            }
        }

        private void TopLeft_Y_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (getInput((TextBox)sender, out value))
            {
                topLeftSet = true;
                config.UpperLeft.Y = value;
                pictureBox1.Invalidate();
            }
        }

        private void TopRight_X_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (getInput((TextBox)sender, out value))
            {
                topRightSet = true;
                config.UpperRight.X = value;
                pictureBox1.Invalidate();
            }
        }

        private void TopRight_Y_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (getInput((TextBox)sender, out value))
            {
                topRightSet = true;
                config.UpperRight.Y = value;
                pictureBox1.Invalidate();
            }
        }

        private void BottomRight_X_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (getInput((TextBox)sender, out value))
            {
                bottomRightSet = true;
                config.LowerRight.X = value;
                pictureBox1.Invalidate();
            }
        }

        private void BottomRight_Y_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (getInput((TextBox)sender, out value))
            {
                bottomRightSet = true;
                config.LowerRight.Y = value;
                pictureBox1.Invalidate();
            }
        }

        private void BottomLeft_X_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (getInput((TextBox)sender, out value))
            {
                bottomLeftSet = true;
                config.LowerLeft.X = value;
                pictureBox1.Invalidate();
            }
        }

        private void BottomLeft_Y_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (getInput((TextBox)sender, out value))
            {
                bottomLeftSet = true;
                config.LowerLeft.Y = value;
                pictureBox1.Invalidate();
            }
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            if (validSettings())
            {
                ImageProcessing processing = new ImageProcessing(reader.getTimedImage().image, true, config);
                Config.Instance.Camera1 = config;
                Size gameFieldSizeMM = Config.Instance.GameFieldSizeMM;
                Config.Instance.SizeRatio = (double)Config.Instance.GameFieldSizeMM.Height / (double)processing.Lines.Height;
                Config.Instance.PuckRadius = Config.Instance.PuckRadiusMM / Config.Instance.SizeRatio;
                Config.Instance.BatRadius = Config.Instance.BatRadiusMM / Config.Instance.SizeRatio;
                Config.Instance.GameFieldSize = new Size((int)Math.Ceiling(gameFieldSizeMM.Width / Config.Instance.SizeRatio), (int)Math.Ceiling(gameFieldSizeMM.Height / Config.Instance.SizeRatio));
                Config.Instance.save();

                Close();
            }
        }

        private void ZoomFactor_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            zoomFactor = Math.Pow(2, ZoomFactor_ComboBox.SelectedIndex);

            setImage(reader.lastCapture.GetImage());
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Rotation_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.ImageRotation = Rotation_ComboBox.SelectedIndex * 90;
        }

        private void DisplaysOrigin_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.displaysOrigin = ((CheckBox)sender).Checked;
        }

        private void ChangeResolution_Button_Click(object sender, EventArgs e)
        {
            int width;
            if (getInput(ResolutionWidth_Box, out width))
            {
                int height;
                if (getInput(ResolutionHeight_Box, out height))
                {
                    config.Resolution = new Size(width, height);
                    newReader();
                }
            }
        }

        private void updateResolutionBoxes()
        {
            ResolutionWidth_Box.Text = config.Resolution.Width.ToString();
            ResolutionHeight_Box.Text = config.Resolution.Height.ToString();
        }
    }
}
