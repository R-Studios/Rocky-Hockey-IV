using Emgu.CV.Structure;
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
    public partial class ImageDebugging : Form
    {
        public ImageDebugging()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        int debuggingIndex = 1;
        bool sliceImage = false;

        ImageProcessing processing;
        TimedImage last;

        public async void displayImage(TimedImage? image)
        {
            if (image == null)
                return;

            last = image.Value;

            processing = await Task<ImageProcessing>.Factory.StartNew(() => { return new ImageProcessing(image?.image?.Clone(), sliceImage); } );

            pictureBox1.Width = processing.Original.Width;
            pictureBox1.Height = processing.Original.Height;

            setImage();
        }

        private void setImage()
        {
            if (processing != null)
                switch (debuggingIndex)
                {
                    case 0:
                        updatePictureBox(pictureBox1, processing.Original.Bitmap);
                        break;
                    case 1:
                        updatePictureBox(pictureBox1, processing.MedianBlur.Bitmap);
                        break;
                    case 2:
                        updatePictureBox(pictureBox1, processing.ChangedColorSchema.Bitmap);
                        break;
                    case 3:
                        updatePictureBox(pictureBox1, processing.BlackWhite.Bitmap);
                        break;
                    case 4:
                        updatePictureBox(pictureBox1, processing.GaussianBlur.Bitmap);
                        break;
                    case 5:
                        updatePictureBox(pictureBox1, processing.Lines.Bitmap);
                        break;
                    case 6:
                        updatePictureBox(pictureBox1, processing.getImagewithCircles());
                        break;
                }
        }

        private void updatePictureBox(PictureBox box, Bitmap image)
        {
            box.Image = image;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                debuggingIndex = 0;

            setImage();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                debuggingIndex = 1;

            setImage();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                debuggingIndex = 2;

            setImage();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                debuggingIndex = 3;

            setImage();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                debuggingIndex = 4;

            setImage();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                debuggingIndex = 5;

            setImage();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            sliceImage = ((CheckBox)sender).Checked;

            displayImage(last);
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                debuggingIndex = 6;

            setImage();
        }
    }
}
