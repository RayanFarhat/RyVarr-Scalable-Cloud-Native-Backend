using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using HamzaCad.DrawingParameters;
using HamzaCad.SlabDecomposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamzaCad.BarsModification
{
    public class BarsEditor
    {
        public static void modifyBar(Polyline p) {
            List<Point2D> vertices = new List<Point2D>();
            double maxLen = 0;
            Point2d p1 = new Point2d();
            Point2d p2 = new Point2d();
            for (int i = 0; i < p.NumberOfVertices-1; i++)
            {
                double dis = GetDistance(p.GetPoint2dAt(i), p.GetPoint2dAt(i + 1));
                if (dis > maxLen)
                {
                    maxLen = dis;
                    p1 = p.GetPoint2dAt(i);
                    p2 = p.GetPoint2dAt(i+1);
                }
                Point2D p2d = new Point2D(p.GetPoint2dAt(i).X, p.GetPoint2dAt(i).Y);
                vertices.Add(p2d);
            }
            Point2D p2dLast = new Point2D(p.GetPoint2dAt(p.NumberOfVertices - 1).X, p.GetPoint2dAt(p.NumberOfVertices - 1).Y);
            vertices.Add(p2dLast);

            //double resAngle = Rotator.GetRotationAngleToXOrY(new Point2D(p1.X,p1.Y),new Point2D(p2.X, p2.Y));
            //Rotator.RotatePoints(vertices, resAngle);

            for (int i = p.NumberOfVertices-1; i >= 2; i--)
            {
                p.RemoveVertexAt(i);
            }
            switch (BarsParam.BarShapeCode)
            {
                case "l":
                    {
                        p.AddVertexAt(0, p1, 0, 0, 0);
                        p.AddVertexAt(1, p2, 0, 0, 0);
                    }
                    break;

                case "sh1":
                    {
                        p.AddVertexAt(0, p1, 0, 0, 0);
                        p.AddVertexAt(1, p2, 0, 0, 0);
                        p.AddVertexAt(2, new Point2d(p2.X + BarsParam.ALength,p2.Y), 0, 0, 0);
                    }
                    break;

                case "sh2":
                    {
                        p.AddVertexAt(0, new Point2d(p1.X + BarsParam.ALength, p1.Y), 0, 0, 0);
                        p.AddVertexAt(1, p1, 0, 0, 0);
                        p.AddVertexAt(2, p2, 0, 0, 0);
                    }
                    break;
                default:
                    break;
            }
        }
        private static double GetDistance(Point2d p1, Point2d p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }
    }
}
