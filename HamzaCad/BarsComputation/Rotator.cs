using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamzaCad.BarsComputation
{
    public class Rotator
    {
        public static void RotatePoints(
          List<Point2D> points,
          double angle)
        {
            angle = angle * Math.PI / 180;//convert degrees to radians

            double cx = points[0].X;
            double cy = points[0].Y;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double temp;

            for (int n = 0; n < points.Count; n++)
            {
                temp = ((points[n].X - cx) * cos - (points[n].Y - cy) * sin) + cx;
                points[n].Y = ((points[n].X - cx) * sin + (points[n].Y - cy) * cos) + cy;
                points[n].X = temp;
            }
        }

        // check how many to rotate so the polgon lines are parallel to X and Y
        public static double GetRotationAngleToXOrY(Point2D p1, Point2D p2)
        {
            // Calculate the vector from p1 to p2
            double deltaX = p2.X - p1.X;
            double deltaY = p2.Y - p1.Y;

            // Calculate the angle degree between the vector and the X-axis
            double angleToXAxis = Math.Atan2(deltaY, deltaX) * (180.0 / Math.PI);

            // Calculate the rotation angle to make the line parallel to the X-axis (or Y-axis)
            double rotationAngle = 90.0 - angleToXAxis;

            return rotationAngle;
        }
    }
}