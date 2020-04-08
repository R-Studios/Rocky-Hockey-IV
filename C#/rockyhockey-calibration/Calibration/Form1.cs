using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Calibration
{
    public partial class Form1 : Form
    {
        private VideoCapture camera;
        private bool cameraInitialized;
        private readonly Mat capturedMat = new Mat();

        private readonly ObjectSerializer objectSerializer = new ObjectSerializer("CalibrationProgrammConfig.xml");
        private string fullPath;

        public Form1()
        {
            InitializeComponent();
            camera1Button.Checked = true;
            ConfigInitializer.InitializeConfig(objectSerializer);
        }

        private void PreviewButtonClick(object sender, EventArgs e)
        {
            camera.Read(capturedMat);
            var processedImage = ProccessImage(capturedMat);
            picturePreview.Image = processedImage.Clone().ToImage<Bgr, byte>().ToBitmap();
        }

        private Mat ProccessImage(Mat mat)  
        {
            var sourcePointsMat = new PointF[4];
            var destinationPointsMat = new PointF[4];
            sourcePointsMat[0] = new PointF(int.Parse(positionUpperLeftX.Text), int.Parse(positionUpperLeftY.Text));
            sourcePointsMat[1] = new PointF(int.Parse(positionUpperRightX.Text), int.Parse(positionUpperRightY.Text));
            sourcePointsMat[2] = new PointF(int.Parse(positionLowerLeftX.Text), int.Parse(positionLowerLeftY.Text));
            sourcePointsMat[3] = new PointF(int.Parse(positionLowerRightX.Text), int.Parse(positionLowerRightY.Text));
            destinationPointsMat[0] = new PointF(0, 0);
            destinationPointsMat[1] = new PointF(320, 0);
            destinationPointsMat[2] = new PointF(0, 240);
            destinationPointsMat[3] = new PointF(320, 240);
            var rectangle = new Rectangle(int.Parse(offsetLeftSide.Text), int.Parse(offsetTopSide.Text),
                int.Parse(gameFieldWidth.Text), int.Parse(gameFieldHeight.Text));
            var lambda = CvInvoke.GetPerspectiveTransform(sourcePointsMat, destinationPointsMat);
            CvInvoke.WarpPerspective(mat, mat, lambda, new Size(320, 240));
            CvInvoke.Rectangle(mat, rectangle, new Bgr(Color.Red).MCvScalar, 2);
            return mat;
        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            if (cameraIndex.SelectedItem != null)
            {
                InitializeCamera(int.Parse(cameraIndex.SelectedItem.ToString()));
            }
        }

        private void InitializeCamera(int cameraIndexSelectedItem)
        {
            if (cameraInitialized)
            {
                try
                {
                    camera.Pause();
                    camera.Stop();
                    camera = null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                cameraInitialized = false;
            }

            camera = new VideoCapture(cameraIndexSelectedItem);
            camera.SetCaptureProperty(CapProp.Fps, 30);
            camera.SetCaptureProperty(CapProp.FrameWidth, 320);
            camera.SetCaptureProperty(CapProp.FrameHeight, 240);
            try
            {
                camera.Start();
                cameraInitialized = true;
                previewButton.Enabled = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void chooseConfigFileButton_Click(object sender, EventArgs e)
        {
            if (Config.Instance.LastDirectoryUsed != null)
            {
                configFileFileDialog.InitialDirectory = Config.Instance.LastDirectoryUsed;
            }

            if (configFileFileDialog.ShowDialog() == DialogResult.OK)
            {
                fullPath = Path.GetFullPath(configFileFileDialog.FileName);
                fileLocationLabel.Text = fullPath;
                saveButton.Enabled = true;
                SetTextFieldsForCamera("1");
                Config.Instance.LastDirectoryUsed = fullPath;
                objectSerializer.SerializeObject(Config.Instance);
            }
            else
            {
                saveButton.Enabled = false;
                fileLocationLabel.Text = "Für das Abspeichern bitte die Konfigurationsdatei auswählen...";
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var documentHandler = XDocument.Load(fullPath);
            foreach (var element in documentHandler.Descendants())
            {
                var cameraForParameters = "1";
                if (camera2Button.Checked)
                {
                    cameraForParameters = "2";
                }

                var elementName = element.Name.LocalName;
                if (elementName.Equals("Camera" + cameraForParameters + "GameFieldWidth"))
                {
                    element.Value = gameFieldWidth.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "GameFieldHeight"))
                {
                    element.Value = gameFieldHeight.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "OffsetFromTop"))
                {
                    element.Value = offsetTopSide.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "OffsetFromLeft"))
                {
                    element.Value = offsetLeftSide.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "UpperLeftPositionX"))
                {
                    element.Value = positionUpperLeftX.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "UpperLeftPositionY"))
                {
                    element.Value = positionUpperLeftY.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "UpperRightPositionX"))
                {
                    element.Value = positionUpperRightX.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "UpperRightPositionY"))
                {
                    element.Value = positionUpperRightY.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "LowerLeftPositionX"))
                {
                    element.Value = positionLowerLeftX.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "LowerLeftPositionY"))
                {
                    element.Value = positionLowerLeftY.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "LowerRightPositionX"))
                {
                    element.Value = positionLowerRightX.Text;
                }
                else if (elementName.Equals("Camera" + cameraForParameters + "LowerRightPositionY"))
                {
                    element.Value = positionLowerRightY.Text;
                }
            }

            documentHandler.Save(fullPath);
        }

        private void SetTextFieldsForCamera(String cameraIndex)
        {
            var documentHandler = XDocument.Load(fullPath);
            foreach (var element in documentHandler.Descendants())
            {

                var elementName = element.Name.LocalName;
                if (elementName.Equals("Camera" + cameraIndex + "GameFieldWidth"))
                {
                    gameFieldWidth.Text = element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "GameFieldHeight"))
                {
                    gameFieldHeight.Text = element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "OffsetFromTop"))
                {
                    offsetTopSide.Text = element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "OffsetFromLeft"))
                {
                    offsetLeftSide.Text = element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "UpperLeftPositionX"))
                {
                    positionUpperLeftX.Text=element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "UpperLeftPositionY"))
                {
                    positionUpperLeftY.Text= element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "UpperRightPositionX"))
                {
                    positionUpperRightX.Text= element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "UpperRightPositionY"))
                {
                    positionUpperRightY.Text =element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "LowerLeftPositionX"))
                {
                    positionLowerLeftX.Text =element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "LowerLeftPositionY"))
                {
                    positionLowerLeftY.Text =  element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "LowerRightPositionX"))
                {
                    positionLowerRightX.Text=element.Value;
                }
                else if (elementName.Equals("Camera" + cameraIndex + "LowerRightPositionY"))
                {
                    positionLowerRightY.Text = element.Value;
                }
            }
        }

        private void camera1Button_CheckedChanged(object sender, EventArgs e)
        {
            if (camera1Button.Checked && fullPath != null)
            {
                SetTextFieldsForCamera("1");
            }
        }

        private void camera2Button_CheckedChanged(object sender, EventArgs e)
        {
            if (camera2Button.Checked && fullPath != null)
            {
                SetTextFieldsForCamera("2");
            }
        }
    }
}