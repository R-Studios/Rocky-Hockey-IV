using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using RockyHockey.Common;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    /// <summary>
    /// Detects the exat middle of a circle on the gamefield
    /// </summary>
    public class GameCameraDetectionFramework : AbstractCameraDetectionFramework
    {
        /// <summary>
        /// Logger for errors
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// filters the red circle out of the image and adds the position to the list
        /// </summary>
        /// <param name="mat">image to process</param>
        /// <returns>executeable Task</returns>
        internal override Task ProcessImage(Mat mat)
        {
            return Task.Factory.StartNew(() =>
            {
                Mat greenHueImage = new Mat();
                Mat hsvImage = new Mat();
                try
                {
                    Mat slicedMat = SliceSingleMat(mat);
                    CvInvoke.MedianBlur(slicedMat, slicedMat, 3);

                    CvInvoke.CvtColor(slicedMat, hsvImage, ColorConversion.Bgr2Hsv);

                    // Threshold the HSV image, keep only the red pixels
                    CvInvoke.InRange(hsvImage, new ScalarArray(new MCvScalar(40, 33, 30)), new ScalarArray(new MCvScalar(85, 255, 255)), greenHueImage);

                    // Make the image blurry
                    CvInvoke.GaussianBlur(greenHueImage, greenHueImage, new Size(9, 9), 2, 2);
                }
                catch (Exception ex)
                {
                    Logger?.Log(ex);
                    return;
                }

                // Use the Hough transform to detect circles in the combined threshold image
                Mat grayScaledImage = greenHueImage.Split().FirstOrDefault();

                //https://docs.opencv.org/3.0-beta/doc/py_tutorials/py_imgproc/py_houghlines/py_houghlines.html
                Mat edges = new Mat();
                CvInvoke.Canny(grayScaledImage, edges, 50, 150);

                CircleF[] circles = CvInvoke.HoughCircles(edges, HoughType.Gradient, 1, greenHueImage.Rows / 8, 100, 20, 5, 10);

                if (circles.Any())
                {
                    var circle = circles.First();

                    if (circle.Center.X == 0 || circle.Center.Y == 0)
                    {
                        return;
                    }

                    SetCameraPicture(new GameFieldPosition
                    {
                        X = circle.Center.X,
                        Y = circle.Center.Y
                    });
                }
            });
        }
        private Mat SliceSingleMat(Mat mat)
        {
            var sourcePointsMat = new PointF[4];
            var destinationPointsMat = new PointF[4];
            var config = Config.Instance;
            sourcePointsMat[0] = new PointF(config.Camera1UpperLeftPositionX, config.Camera1UpperLeftPositionY);
            sourcePointsMat[1] = new PointF(config.Camera1UpperRightPositionX, config.Camera1UpperRightPositionY);
            sourcePointsMat[2] = new PointF(config.Camera1LowerLeftPositionX, config.Camera1LowerLeftPositionY);
            sourcePointsMat[3] = new PointF(config.Camera1LowerRightPositionX, config.Camera1LowerRightPositionY);
            destinationPointsMat[0] = new PointF(0, 0);
            destinationPointsMat[1] = new PointF(320, 0);
            destinationPointsMat[2] = new PointF(0, 240);
            destinationPointsMat[3] = new PointF(320, 240);
            var rectangle = new Rectangle(config.Camera1OffsetFromLeft, config.Camera1OffsetFromTop, config.Camera1GameFieldWidth, config.Camera1GameFieldHeight);        
            Mat lambda = CvInvoke.GetPerspectiveTransform(sourcePointsMat, destinationPointsMat);
            CvInvoke.WarpPerspective(mat, mat, lambda, new Size(320, 240));
            Mat cropped = new Mat(mat, rectangle);

            return cropped;
        }

        private Mat CombineBothMats(Mat mat, Mat mat2)
        {
            PointF[] sourcePointsMat2 = new PointF[4];
            PointF[] sourcePointsMat = new PointF[4];
            PointF[] destinationPointsMat = new PointF[4];
            sourcePointsMat[0] = new PointF(0, 0);
            sourcePointsMat[1] = new PointF(320, 0);
            sourcePointsMat[2] = new PointF(0, 240);
            sourcePointsMat[3] = new PointF(335, 232);
            sourcePointsMat2[0] = new PointF(5, 0);
            sourcePointsMat2[1] = new PointF(300, 0);
            sourcePointsMat2[2] = new PointF(0, 240);
            sourcePointsMat2[3] = new PointF(320, 242);
            destinationPointsMat[0] = new PointF(0, 0);
            destinationPointsMat[1] = new PointF(320, 0);
            destinationPointsMat[2] = new PointF(0, 240);
            destinationPointsMat[3] = new PointF(320, 240);
            var rectangle = new Rectangle(36, 0, 236, 220);
            var rectangle2 = new Rectangle(55, 10, 255, 220);
            Mat lambda = CvInvoke.GetPerspectiveTransform(sourcePointsMat, destinationPointsMat);
            Mat lambda2 = CvInvoke.GetPerspectiveTransform(sourcePointsMat2, destinationPointsMat);
            CvInvoke.WarpPerspective(mat, mat, lambda, new Size(320, 240));
            CvInvoke.WarpPerspective(mat2, mat2, lambda2, new Size(320, 240));
            Mat cropped = new Mat(mat, rectangle);
            Mat cropped2 = new Mat(mat2, rectangle2);
            CvInvoke.Flip(cropped2, cropped2, FlipType.Horizontal);
            Mat combindedMat = new Mat();
            CvInvoke.HConcat(cropped, cropped2, combindedMat);
            CvInvoke.Imshow("combined", combindedMat);
            CvInvoke.WaitKey(100);
            return combindedMat;
        }
    }
}
