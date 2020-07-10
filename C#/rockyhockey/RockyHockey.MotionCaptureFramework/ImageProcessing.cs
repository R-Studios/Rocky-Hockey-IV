using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    public class ImageProcessing
    {
        public Mat Original { get; private set; } = new Mat();
        public Mat MedianBlur { get; private set; } = new Mat();
        public Mat ChangedColorSchema { get; private set; } = new Mat();
        public Mat BlackWhite { get; private set; } = new Mat();
        public Mat GaussianBlur { get; private set; } = new Mat();
        public Mat GrayScaled { get; private set; } = new Mat();
        public Mat Lines { get; private set; } = new Mat();
        public CircleF[] circles { get; private set; } = new CircleF[0];

        private CameraConfig cameraConfig;
        private PuckDetectionConfig detectionConfig;


        public ImageProcessing(Mat original, bool sliceImage, CameraConfig cameraConfig = null, PuckDetectionConfig detectionConfig = null)
        {
            if (original != null)
            {
                this.cameraConfig = cameraConfig ?? Config.Instance.Camera1;
                this.detectionConfig = detectionConfig ?? Config.Instance.detectionConfig ?? new PuckDetectionConfig();

                if (sliceImage)
                    original = SliceSingleMat(original);

                Original = original;

                process();
            }
        }

        private void process()
        {
            //removes noise from image (pixels that don't fit into their surrounding)
            CvInvoke.MedianBlur(Original, MedianBlur, 3);

            //converts color schema
            CvInvoke.CvtColor(MedianBlur, ChangedColorSchema, ColorConversion.Bgr2Hsv);

            // Threshold the HSV image, keep only the red pixels
            CvInvoke.InRange(ChangedColorSchema, new ScalarArray(new MCvScalar(40, 33, 30)), new ScalarArray(new MCvScalar(85, 255, 255)), BlackWhite);

            // Make the image blurry
            CvInvoke.GaussianBlur(BlackWhite, GaussianBlur, new Size(9, 9), 2, 2);

            // Use the Hough transform to detect circles in the combined threshold image
            GrayScaled = GaussianBlur.Split().FirstOrDefault();

            //https://docs.opencv.org/3.0-beta/doc/py_tutorials/py_imgproc/py_houghlines/py_houghlines.html
            CvInvoke.Canny(GrayScaled, Lines, 50, 150);

            //detect circles
            circles = CvInvoke.HoughCircles(Lines, 
                HoughType.Gradient, 
                detectionConfig.DP, 
                detectionConfig.MinDistance, 
                detectionConfig.Param1, 
                detectionConfig.Param2, 
                detectionConfig.MinDetectionRadius, 
                detectionConfig.MaxDetectionRadius);
        }

        /// <summary>
        /// transforms distorted gamefield on the picture into a rectangle
        /// </summary>
        /// <param name="mat">input image</param>
        /// <returns></returns>
        private Mat SliceSingleMat(Mat mat)
        {
            var sourcePointsMat = new PointF[4];
            sourcePointsMat[0] = new PointF((float)cameraConfig.UpperLeft.X, (float)cameraConfig.UpperLeft.Y);
            sourcePointsMat[1] = new PointF((float)cameraConfig.UpperRight.X, (float)cameraConfig.UpperRight.Y);
            sourcePointsMat[2] = new PointF((float)cameraConfig.LowerLeft.X, (float)cameraConfig.LowerLeft.Y);
            sourcePointsMat[3] = new PointF((float)cameraConfig.LowerRight.X, (float)cameraConfig.LowerRight.Y);

            var destinationPointsMat = new PointF[4];
            destinationPointsMat[0] = new PointF(0, 0);
            destinationPointsMat[1] = new PointF(cameraConfig.FieldView.Width, 0);
            destinationPointsMat[2] = new PointF(0, cameraConfig.FieldView.Height);
            destinationPointsMat[3] = new PointF(cameraConfig.FieldView.Width, cameraConfig.FieldView.Height);

            var rectangle = new Rectangle(0, 0, cameraConfig.FieldView.Width, cameraConfig.FieldView.Height);
            Mat lambda = CvInvoke.GetPerspectiveTransform(sourcePointsMat, destinationPointsMat);
            CvInvoke.WarpPerspective(mat, mat, lambda, cameraConfig.FieldView);
            Mat cropped = new Mat(mat, rectangle);

            Mat rotated = null;
            Bitmap rota = cropped.Clone().Bitmap;
            switch (cameraConfig.ImageRotation)
            {
                case 90:
                    rota.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    rotated = new Image<Bgr, Byte>(rota).Mat;
                    break;
                case 180:
                    rota.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    rotated = new Image<Bgr, Byte>(rota).Mat;
                    break;
                case 270:
                    rota.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    rotated = new Image<Bgr, Byte>(rota).Mat;
                    break;
                default:
                    rotated = cropped;
                    break;
            }

            return rotated;
        }

        public Bitmap getImagewithCircles()
        {
            Bitmap tempMap = new Bitmap(Lines.Width, Lines.Height);

            Graphics CurrentFrameGraphics = Graphics.FromImage(tempMap);

            CurrentFrameGraphics.DrawImage(Lines.Bitmap, 0, 0);

            Pen pen = new Pen(new SolidBrush(Color.White), 1f);
            foreach (CircleF circle in circles)
            {
                float radius = circle.Radius;
                CurrentFrameGraphics.DrawEllipse(pen, new RectangleF(circle.Center.X - radius, circle.Center.Y - radius, 2 * radius, 2 * radius));
            }

            return tempMap;
        }

        public static Task<TimedCoordinate> ProcessImage(TimedImage image, bool sliceImage = true)
        {
            return Task<TimedCoordinate>.Factory.StartNew(() =>
            {
                TimedCoordinate retval = null;

                if (image.image.Size != new Size(0, 0))
                {
                    try
                    {
                        CircleF[] circles = new ImageProcessing(image.image.Clone(), sliceImage).circles;

                        if (circles.Any())
                        {
                            var circle = circles.First();

                            if (circle.Center.X != 0 && circle.Center.Y != 0)
                                retval = new TimedCoordinate
                                {
                                    X = circle.Center.X,
                                    Y = circle.Center.Y,
                                    Timestamp = image.timeStamp
                                };
                        }
                    }
                    catch (Exception ex)
                    {
                        TextFileLogger.Instance.Log(ex);
                    }
                }

                return retval;
            });
        }
    }
}
