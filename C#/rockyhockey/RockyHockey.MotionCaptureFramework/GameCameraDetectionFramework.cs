using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using RockyHockey.Common;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    /// <summary>
    /// Detects the exat middle of a circle on the gamefield
    /// </summary>
    public class GameCameraDetectionFramework : AbstractCameraDetectionFramework
    {
        /// <summary>
        /// filters the red circle out of the image and adds the position to the list
        /// </summary>
        /// <param name="mat">image to process</param>
        /// <returns>executeable Task</returns>
        internal override Task<GameFieldPosition> ProcessImage(Mat mat)
        {
            return Task<GameFieldPosition>.Factory.StartNew(() =>
            {
                GameFieldPosition retval = null;

                try
                {
                    Mat greenHueImage = new Mat();
                    Mat hsvImage = new Mat();

                    Mat slicedMat = SliceSingleMat(mat);

                    //removes noise from image (pixels that don't fit into their surrounding)
                    CvInvoke.MedianBlur(slicedMat, slicedMat, 3);

                    CvInvoke.CvtColor(slicedMat, hsvImage, ColorConversion.Bgr2Hsv);

                    // Threshold the HSV image, keep only the red pixels
                    CvInvoke.InRange(hsvImage, new ScalarArray(new MCvScalar(40, 33, 30)), new ScalarArray(new MCvScalar(85, 255, 255)), greenHueImage);

                    // Make the image blurry
                    CvInvoke.GaussianBlur(greenHueImage, greenHueImage, new Size(9, 9), 2, 2);

                    // Use the Hough transform to detect circles in the combined threshold image
                    Mat grayScaledImage = greenHueImage.Split().FirstOrDefault();

                    //https://docs.opencv.org/3.0-beta/doc/py_tutorials/py_imgproc/py_houghlines/py_houghlines.html
                    Mat edges = new Mat();
                    CvInvoke.Canny(grayScaledImage, edges, 50, 150);

                    CircleF[] circles = CvInvoke.HoughCircles(edges, HoughType.Gradient, 1, greenHueImage.Rows / 8, 100, 20, 5, 10);

                    if (circles.Any())
                    {
                        var circle = circles.First();

                        if (circle.Center.X != 0 && circle.Center.Y != 0)
                            retval = new GameFieldPosition
                            {
                                X = circle.Center.X,
                                Y = circle.Center.Y
                            };
                    }
                }
                catch (Exception ex)
                {
                    Logger?.Log(ex);
                }

                return retval;
            });
        }

        /// <summary>
        /// transforms distorted gamefield on the picture into a rectangle
        /// </summary>
        /// <param name="mat">input image</param>
        /// <returns></returns>
        private Mat SliceSingleMat(Mat mat)
        {
            var config = Config.Instance;

            var sourcePointsMat = new PointF[4];
            sourcePointsMat[0] = new PointF(config.Camera1.UpperLeft.X, config.Camera1.UpperLeft.Y);
            sourcePointsMat[1] = new PointF(config.Camera1.UpperRight.X, config.Camera1.UpperLeft.Y);
            sourcePointsMat[2] = new PointF(config.Camera1.LowerLeft.X, config.Camera1.UpperLeft.Y);
            sourcePointsMat[3] = new PointF(config.Camera1.LowerRight.X, config.Camera1.UpperLeft.Y);

            var destinationPointsMat = new PointF[4];
            destinationPointsMat[0] = new PointF(0, 0);
            destinationPointsMat[1] = new PointF(320, 0);
            destinationPointsMat[2] = new PointF(0, 240);
            destinationPointsMat[3] = new PointF(320, 240);

            var rectangle = new Rectangle(config.Camera1.Offset.FromLeft, config.Camera1.Offset.FromTop, config.Camera1.FieldSize.Width, config.Camera1.FieldSize.Height);        
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
