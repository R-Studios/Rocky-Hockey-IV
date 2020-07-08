using Emgu.CV;
using Emgu.CV.Structure;
using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockyHockeyGUI
{
    public partial class CircleDetectionCalibration : Form
    {
        public CircleDetectionCalibration()
        {
            InitializeComponent();
            GrabImage_Button.Enabled = false;
            StopImageStream_Button.Enabled = false;
            StopImageStream_Button.Visible = false;
            ImageStreamStart_Button.Visible = false;

            detectionConfig = Config.Instance.detectionConfig ?? new PuckDetectionConfig();

            Param2Display_Box.Text = detectionConfig.Param2.ToString();
            Param1Display_Box.Text = detectionConfig.Param1.ToString();
            DPDisplay_Box.Text = detectionConfig.DP.ToString();
            MinRadiusdisplay_Box.Text = detectionConfig.MinDetectionRadius.ToString();
            MaxRadiusdisplay_Box.Text = detectionConfig.MaxDetectionRadius.ToString();
            MinDistanceDisplay_Box.Text = detectionConfig.MinDistance.ToString();

            Param2trackBar.Value = (int)detectionConfig.Param2;
            Param1trackBar.Value = (int)detectionConfig.Param1;
            DPTrackBar.Value = (int)detectionConfig.DP;
            MinDistanceTrackbar.Value = (int)detectionConfig.MinDistance;
            MinRadiusTrackbar.Value = detectionConfig.MinDetectionRadius;
            MaxRadiusTrackbar.Value = detectionConfig.MaxDetectionRadius;

            FormClosing += formClosing;
        }

        private void formClosing(object sender, EventArgs e)
        {
            readerCalibrationWindow?.Close();
            imageDebuggingWindow?.Close();

            timer?.Stop();
            timer?.Dispose();
            timer = null;

            reader?.finalize();
            reader = null;
        }

        Action<object, EventArgs> streamMethod;

        ImageProvider reader;

        CameraCalibration readerCalibrationWindow;

        ImageDebugging imageDebuggingWindow;

        TimedImage last;

        ImageProcessing process;

        PuckDetectionConfig detectionConfig;

        private async void FromCamera_Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                GrabImage_Button.Enabled = false;
                ImageStreamStart_Button.Enabled = false;
                StopImageStream_Button.Enabled = false;

                timer?.Stop();
                timer?.Dispose();

                reader?.finalize();

                reader = await Task<ImageProvider>.Factory.StartNew(() => { return new CameraReader(); });

                streamMethod = cameraStreamMethod;

                ImageIndex_Box.Visible = false;
                ImageIndex_Label.Visible = false;
                ImageStreamStart_Button.Enabled = true;
                ImageStreamStart_Button.Visible = true;
                StopImageStream_Button.Visible = true;

                GrabImage_Button.Enabled = true;
            }
        }

        private async void FromVideo_Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                GrabImage_Button.Enabled = false;
                ImageStreamStart_Button.Enabled = false;
                StopImageStream_Button.Enabled = false;

                timer?.Stop();
                timer?.Dispose();

                reader?.finalize();

                reader = await Task<ImageProvider>.Factory.StartNew(() => { return new VideoReader("P:\\testImages\\TestAVI.avi"); });

                streamMethod = videostreamMethod;

                ImageIndex_Box.Visible = true;
                ImageIndex_Label.Visible = true;
                ImageStreamStart_Button.Enabled = true;
                ImageStreamStart_Button.Visible = true;
                StopImageStream_Button.Visible = true;

                GrabImage_Button.Enabled = true;
            }
        }

        private void FromImage_Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                GrabImage_Button.Enabled = false;
                ImageStreamStart_Button.Visible = false;
                StopImageStream_Button.Visible = false;

                timer?.Stop();
                timer?.Dispose();

                reader?.finalize();

                reader = new ImageReader("P:\\testImages\\", -2, 12);

                ImageIndex_Box.Visible = true;
                ImageIndex_Label.Visible = true;
                ImageStreamStart_Button.Visible = false;

                GrabImage_Button.Enabled = true;
            }
        }

        private void GrabImage_Button_Click(object sender, EventArgs e)
        {
            if (reader is VideoReader || reader is ImageReader)
            {
                string frameIndexString = ImageIndex_Box.Text;
                if (frameIndexString != "")
                {
                    int frameIndex;
                    if (int.TryParse(frameIndexString, out frameIndex))
                    {
                        if (reader is ImageReader)
                            frameIndex = ((ImageReader)reader).setFrameIndex(frameIndex);
                        else
                            frameIndex = ((VideoReader)reader).setFrameIndex(frameIndex);

                        ImageIndex_Box.Text = frameIndex.ToString();
                    }
                }
            }

            setImage(reader.getTimedImage());
        }

        private void updateCurrentFrame()
        {
            setImage(last);
        }

        private async void setImage(TimedImage timedImage)
        {
            if (timedImage.image != null)
            {
                last = new TimedImage
                {
                    image = timedImage.image.Clone(),
                    timeStamp = timedImage.timeStamp
                };

                process = await Task<ImageProcessing>.Factory.StartNew(() => { 
                    return new ImageProcessing(timedImage.image.Clone(), true, null,
                        UseConfigParams_Button.Checked ? null : detectionConfig); });

                Bitmap image = process.Lines.Bitmap;

                CurrentFrame.Width = image.Width;
                CurrentFrame.Height = image.Height;

                StringBuilder builder = new StringBuilder();

                Bitmap tempMap = new Bitmap(image.Width, image.Height);

                Graphics CurrentFrameGraphics = Graphics.FromImage(tempMap);

                CurrentFrameGraphics.DrawImage(image, 0, 0);

                Pen pen = new Pen(new SolidBrush(Color.White), 1f);
                foreach (CircleF circle in process.circles)
                {
                    float radius = circle.Radius;
                    float x = circle.Center.X;
                    float y = circle.Center.Y;
                    CurrentFrameGraphics.DrawEllipse(pen, new RectangleF(x - radius, y - radius, 2 * radius, 2 * radius));
                    builder.Append("( ")
                        .Append(x)
                        .Append(" / ")
                        .Append(y)
                        .Append(" ), r = ")
                        .Append(radius).Append("\r\n");
                }

                FoundCircles_Box.Clear();
                FoundCircles_Box.Text += builder.ToString();

                CurrentFrame.Image = tempMap;

                imageDebuggingWindow?.displayImage(timedImage);
            }
        }


        //debugging and calibration window functions

        private void CalibrateReader_Button_Click(object sender, EventArgs e)
        {
            CalibrateReader_Button.Enabled = false;

            readerCalibrationWindow = new CameraCalibration(reader);
            readerCalibrationWindow.Show();
            readerCalibrationWindow.FormClosed += cameraCalibrationWindow_FormClosed;
        }

        private void cameraCalibrationWindow_FormClosed(object sender, EventArgs e)
        {
            readerCalibrationWindow.Dispose();
            readerCalibrationWindow = null;

            CalibrateReader_Button.Enabled = true;
        }

        private void ImageDebugging_Button_Click(object sender, EventArgs e)
        {
            ImageDebugging_Button.Enabled = false;

            imageDebuggingWindow = new ImageDebugging();
            imageDebuggingWindow.Show();
            imageDebuggingWindow.FormClosed += imageDebuggingWindow_FormClosed;
        }

        private void imageDebuggingWindow_FormClosed(object sender, EventArgs e)
        {
            imageDebuggingWindow.Dispose();
            imageDebuggingWindow = null;

            ImageDebugging_Button.Enabled = true;
        }


        //value setter

        private void MinRadiusTrackbar_Scroll(object sender, EventArgs e)
        {
            detectionConfig.MinDetectionRadius = ((TrackBar)sender).Value;
            if (detectionConfig.MinDetectionRadius > detectionConfig.MaxDetectionRadius)
            {
                detectionConfig.MinDetectionRadius = detectionConfig.MaxDetectionRadius;
                ((TrackBar)sender).Value = detectionConfig.MaxDetectionRadius;
            }
            MinRadiusdisplay_Box.Text = detectionConfig.MinDetectionRadius.ToString();
            updateCurrentFrame();
        }

        private void MaxRadiusTrackbar_Scroll(object sender, EventArgs e)
        {
            detectionConfig.MaxDetectionRadius = ((TrackBar)sender).Value;
            if (detectionConfig.MaxDetectionRadius < detectionConfig.MinDetectionRadius)
            {
                detectionConfig.MaxDetectionRadius = detectionConfig.MinDetectionRadius;
                ((TrackBar)sender).Value = detectionConfig.MinDetectionRadius;
            }
            MaxRadiusdisplay_Box.Text = detectionConfig.MaxDetectionRadius.ToString();
            updateCurrentFrame();
        }

        private void MinDistanceTrackbar_Scroll(object sender, EventArgs e)
        {
            detectionConfig.MinDistance = ((TrackBar)sender).Value;
            MinDistanceDisplay_Box.Text = detectionConfig.MinDistance.ToString();
            updateCurrentFrame();
        }

        private void DPTrackBar_Scroll(object sender, EventArgs e)
        {
            detectionConfig.DP = ((TrackBar)sender).Value;
            DPDisplay_Box.Text = detectionConfig.DP.ToString();
            updateCurrentFrame();
        }

        private void Param1trackBar_Scroll(object sender, EventArgs e)
        {
            detectionConfig.Param1 = ((TrackBar)sender).Value;
            Param1Display_Box.Text = detectionConfig.Param1.ToString();
            updateCurrentFrame();
        }

        private void Param2trackBar_Scroll(object sender, EventArgs e)
        {
            detectionConfig.Param2 = ((TrackBar)sender).Value;
            Param2Display_Box.Text = detectionConfig.Param2.ToString();
            updateCurrentFrame();
        }


        private void cameraStreamMethod(object sender, EventArgs e)
        {
            setImage(reader.getTimedImage());
        }


        int frameIndexStart;
        private void videostreamMethod(object sender, EventArgs e)
        {
            int currentframe = ((VideoReader)reader).setFrameIndex(frameIndexStart);
            ImageIndex_Box.Text = frameIndexStart.ToString();

            setImage(reader.getTimedImage());

            if (currentframe != frameIndexStart++)
            {
                ((Timer)sender).Stop();
            }
        }


        Timer timer;
        private void ImageStreamStart_Button_Click(object sender, EventArgs e)
        {
            if (reader == null)
                return;

            ImageStreamStart_Button.Enabled = false;

            if (reader is VideoReader)
            {
                string frameIndexString = ImageIndex_Box.Text;
                if (frameIndexString == "")
                    frameIndexString = "0";

                if (!int.TryParse(frameIndexString, out frameIndexStart))
                    return;
            }

            timer = new Timer
            {
                Interval = 1000 / reader.getFPS()
            };
            timer.Tick += new EventHandler(streamMethod);
            timer.Start();

            StopImageStream_Button.Enabled = true;
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            Config.Instance.detectionConfig = detectionConfig;
            Config.Instance.save();

            Close();
        }

        private void StopImageStream_Button_Click(object sender, EventArgs e)
        {
            StopImageStream_Button.Enabled = false;
            timer.Stop();
            timer.Dispose();
            timer = null;
            ImageStreamStart_Button.Enabled = true;
        }
    }
}
