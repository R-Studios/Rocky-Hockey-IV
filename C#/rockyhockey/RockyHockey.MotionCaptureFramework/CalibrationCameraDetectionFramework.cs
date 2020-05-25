using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using RockyHockey.Common;

namespace RockyHockey.MotionCaptureFramework
{
    //TODO: complete implementation and add comments
    public class CalibrationCameraDetectionFramework : AbstractCameraDetectionFramework
    {
        private const float Tolerance = 0;
        private Mat whiteHueRange = new Mat();
        private Mat grayScaledImage = new Mat();
        private Mat blurredImage = new Mat();
        private int frameCount = 0;
        int largest_contour_index = 0;
        double largest_area = 0;
        VectorOfPoint largestContour;

        internal override Task<TimedCoordinate> ProcessImage(Mat mat, long timestamp)
        {
            return Task<TimedCoordinate>.Factory.StartNew(() =>
            {
                CvInvoke.CvtColor(mat, grayScaledImage, ColorConversion.Bgr2Gray);
                grayScaledImage.Save(@"C:\Users\Tim-ThinkPad\Desktop\test\grayScaled.png");
                CvInvoke.GaussianBlur(grayScaledImage, blurredImage, new Size(11, 11), 0);
                blurredImage.Save(@"C:\Users\Tim-ThinkPad\Desktop\test\blurred.png");
                CvInvoke.Threshold(blurredImage, whiteHueRange, 80, 255, ThresholdType.Binary);
                var structuringElement =
                    CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(30, 30), new Point(-1, -1));
                whiteHueRange.Save(@"C:\Users\Tim-ThinkPad\Desktop\test\white.png");
                //CvInvoke.Erode(whiteHueRange,whiteHueRange,structuringElement,new Point(-1,-1),1,BorderType.Default,default(MCvScalar));
                UMat detectedEdges = new UMat();
                CvInvoke.Canny(whiteHueRange, detectedEdges, 180, 120);
                LineSegment2D[] lines = CvInvoke.HoughLinesP(
                    detectedEdges,
                    1, //Distance resolution in pixel-related units
                    Math.PI / 45.0, //Angle resolution measured in radians.
                    20, //threshold
                    30, //min Line width
                    10); //gap between lines
                List<Triangle2DF> triangleList = new List<Triangle2DF>();
                List<RotatedRect> boxList = new List<RotatedRect>(); //a box is a rotated rectangle

                using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                {
                    CvInvoke.FindContours(detectedEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                    int count = contours.Size;
                    for (int i = 0; i < count; i++)
                    {
                        using (VectorOfPoint contour = contours[i])
                        using (VectorOfPoint approxContour = new VectorOfPoint())
                        {
                            CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                            if (CvInvoke.ContourArea(approxContour, false) > 250) //only consider contours with area greater than 250
                            {
                                if (approxContour.Size == 3) //The contour has 3 vertices, it is a triangle
                                {
                                    Point[] pts = approxContour.ToArray();
                                    triangleList.Add(new Triangle2DF(
                                       pts[0],
                                       pts[1],
                                       pts[2]
                                       ));
                                }
                                else if (approxContour.Size == 6) //The contour has 4 vertices.
                                {
                                    #region determine if all the angles in the contour are within [80, 100] degree
                                    bool isRectangle = true;
                                    Point[] pts = approxContour.ToArray();
                                    LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                    for (int j = 0; j < edges.Length; j++)
                                    {
                                        double angle = Math.Abs(
                                           edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                        if (angle < 80 || angle > 100)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                    }
                                    #endregion

                                    if (isRectangle) boxList.Add(CvInvoke.MinAreaRect(approxContour));
                                }
                            }
                        }
                    }
                }
                //VectorOfVectorOfPoint hierachy = new VectorOfVectorOfPoint();
                //CvInvoke.FindContours(whiteHueRange, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple,
                //   new Point(-1, -1));


                if (boxList.Count > 0)
                {

                    Mat triangleRectangleImage = new Mat();
                    foreach (RotatedRect box in boxList)
                    {
                        var boxSize = box.Size;
                        var boxCenter = box.Center;
                    }
                }

                //whiteHueRange = cv2.dilate(thresh, None, iterations = 4);
                //whiteHueRange.Save(@"C:\Users\Tim-ThinkPad\Desktop\test\dilate.png");

                return null;

            });
        }

    }
}