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
                    bars[i].Polygon.RemoveVertexAt(j);
                    bars[i].Polygon.AddVertexAt(j, RotatePoint2d(vertex, angle, orginPoint), 0, 0, 0);
                }
                // rotate texts
                for (int j = 0; j < bars[i].Texts.Count; j++)
                {
                    Point3d pos = bars[i].Texts[j].Position;
                    bars[i].Texts[j].Position = RotatePoint3d(pos, angle, orginPoint);
                }
                // rotate arrows
                for (int j = 0; j < bars[i].Arrows.Count; j++)
                {
                        Leader arr = bars[i].Arrows[j];
                        Leader newArr = new Leader();
                        newArr.AppendVertex(RotatePoint3d(arr.VertexAt(0), angle, orginPoint));
                        newArr.AppendVertex(RotatePoint3d(arr.VertexAt(1), angle, orginPoint));
                        newArr.HasArrowHead = true;
                        //newArr.DimensionStyle = db.Dimstyle;
                        newArr.Dimscale = BarsComputer.arrowScale;
                        bars[i].Arrows[j] = newArr;
                }
                // rotate blocking lines
                for (int j = 0; j < bars[i].ArrowsBlockingLines.Count; j++)
                {
                    Line line = bars[i].ArrowsBlockingLines[j];
                    bars[i].ArrowsBlockingLines[j] = new Line(RotatePoint3d(line.StartPoint,angle,orginPoint),
                        RotatePoint3d(line.EndPoint, angle, orginPoint));
                }
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
        private static Point2d RotatePoint2d(
             Point2d point,
              double angle,
               Point2D orginPoint)
        {
            angle = angle * Math.PI / 180;//convert degrees to radians

            double cx = orginPoint.X;
            double cy = orginPoint.Y;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double temp;
            temp = ((point.X - cx) * cos - (point.Y - cy) * sin) + cx;
            return new Point2d(temp, ((point.X - cx) * sin + (point.Y - cy) * cos) + cy);
        }
        private static Point3d RotatePoint3d(
             Point3d point,
              double angle,
               Point2D orginPoint)
        {
            angle = angle * Math.PI / 180;//convert degrees to radians

            double cx = orginPoint.X;
            double cy = orginPoint.Y;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double temp;
            temp = ((point.X - cx) * cos - (point.Y - cy) * sin) + cx;
            return new Point3d(temp, ((point.X - cx) * sin + (point.Y - cy) * cos) + cy,0);
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