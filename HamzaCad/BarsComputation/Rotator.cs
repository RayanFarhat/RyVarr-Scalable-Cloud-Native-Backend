using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Internal;

namespace HamzaCad.BarsComputation
{
    public class Rotator
    {
        public static void RotatePolylinebars(
              List<DrawingBar> bars,
              double angle,
              Point2D orginPoint)
        {
            angle = angle * Math.PI / 180;//convert degrees to radians

            double cx = orginPoint.X;
            double cy = orginPoint.Y;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double temp;
            for (int i = 0; i < bars.Count;i++)
            {
                int pointsNum = 0;
                if (BarsComputer.withEar)
                    pointsNum=3;
                else pointsNum=2;
                // rotate polyline
                for (int j = 0; j < pointsNum; j++)
                {
                    Point2d vertex = bars[i].Polygon.GetPoint2dAt(j);
                    temp = ((vertex.X - cx) * cos - (vertex.Y - cy) * sin) + cx;
                    double Y = ((vertex.X - cx) * sin + (vertex.Y - cy) * cos) + cy;
                    double X = temp;
                    bars[i].Polygon.RemoveVertexAt(j);
                    bars[i].Polygon.AddVertexAt(j, new Point2d(X, Y), 0, 0, 0);
                }
                Point3d pos = bars[i].Text.Position;
                temp = ((pos.X - cx) * cos - (pos.Y - cy) * sin) + cx;
                double Y2 = ((pos.X - cx) * sin + (pos.Y - cy) * cos) + cy;
                double X2 = temp;
                bars[i].Text.Position = new Point3d(X2, Y2, 0);
            }
            
        }
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