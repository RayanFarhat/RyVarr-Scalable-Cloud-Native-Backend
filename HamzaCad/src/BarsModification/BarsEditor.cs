using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using HamzaCad.AutoCADAdapter;
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
            }

            double resAngle = Rotator.GetRotationAngleToXOrY(new Point2D(p1.X,p1.Y),new Point2D(p2.X, p2.Y));
            // now p1 and rotatedP2 are on top of each other
            Point2d rotatedP2 = Rotator.RotatePoint2d(p2, resAngle, new Point2D(p1.X, p1.Y));
            p2 = rotatedP2;
            Adapter.ed.WriteMessage("p1 "+p1.X + " "+p1.Y+"\n");
            Adapter.ed.WriteMessage("p2 "+ rotatedP2.X + " "+ rotatedP2.Y+"\n");
            //Rotator.RotatePoints(vertices, resAngle);

            List<Point2D> rotatedVertices = new List<Point2D>();

            switch (BarsParam.BarShapeCode)
            {
                case "l":
                    {
                        rotatedVertices.Add(new Point2D(p1.X, p1.Y));
                        rotatedVertices.Add(new Point2D(p2.X, p2.Y));
                        updatePolyline(p, rotatedVertices, -resAngle);
                    }
                    break;

                case "sh1":
                    {
                        rotatedVertices.Add(new Point2D(p1.X, p1.Y));
                        rotatedVertices.Add(new Point2D(p2.X, p2.Y));
                        rotatedVertices.Add(new Point2D(p2.X + BarsParam.ALength, p2.Y));
                        updatePolyline(p, rotatedVertices, -resAngle);

                    }
                    break;

                case "sh2":
                    {
                        rotatedVertices.Add(new Point2D(p1.X + BarsParam.ALength, p1.Y));
                        rotatedVertices.Add(new Point2D(p1.X, p1.Y));
                        rotatedVertices.Add(new Point2D(p2.X, p2.Y));
                        updatePolyline(p, rotatedVertices, -resAngle);

                    }
                    break;
                case "shBoth":
                    {
                        rotatedVertices.Add(new Point2D(p1.X + BarsParam.ALength, p1.Y));
                        rotatedVertices.Add(new Point2D(p1.X, p1.Y));
                        rotatedVertices.Add(new Point2D(p2.X, p2.Y));
                        rotatedVertices.Add(new Point2D(p2.X + BarsParam.ALength, p2.Y));
                        updatePolyline(p, rotatedVertices, -resAngle);
                    }
                    break;
                case "dh1":
                    {
                        rotatedVertices.Add(new Point2D(p1.X, p1.Y));
                        rotatedVertices.Add(new Point2D(p2.X, p2.Y));
                        rotatedVertices.Add(new Point2D(p2.X + BarsParam.ALength, p2.Y));
                        rotatedVertices.Add(new Point2D(p2.X + BarsParam.ALength, p2.Y - BarsParam.BLength));
                        updatePolyline(p, rotatedVertices, -resAngle);
                    }
                    break;
                case "dh2":
                    {
                        rotatedVertices.Add(new Point2D(p1.X + BarsParam.ALength, p1.Y + BarsParam.BLength));
                        rotatedVertices.Add(new Point2D(p1.X + BarsParam.ALength, p1.Y));
                        rotatedVertices.Add(new Point2D(p1.X, p1.Y));
                        rotatedVertices.Add(new Point2D(p2.X, p2.Y));
                        updatePolyline(p, rotatedVertices, -resAngle);
                    }
                    break;
                case "dhBoth":
                    {
                        rotatedVertices.Add(new Point2D(p1.X + BarsParam.ALength, p1.Y + BarsParam.BLength));
                        rotatedVertices.Add(new Point2D(p1.X + BarsParam.ALength, p1.Y));
                        rotatedVertices.Add(new Point2D(p1.X, p1.Y));
                        rotatedVertices.Add(new Point2D(p2.X, p2.Y));
                        rotatedVertices.Add(new Point2D(p2.X + BarsParam.ALength, p2.Y));
                        rotatedVertices.Add(new Point2D(p2.X + BarsParam.ALength, p2.Y - BarsParam.BLength));
                        updatePolyline(p, rotatedVertices, -resAngle);
                    }
                    break;
                case "sh1dh2":
                    {
                        rotatedVertices.Add(new Point2D(p1.X + BarsParam.ALength, p1.Y + BarsParam.BLength));
                        rotatedVertices.Add(new Point2D(p1.X + BarsParam.ALength, p1.Y));
                        rotatedVertices.Add(new Point2D(p1.X, p1.Y));
                        rotatedVertices.Add(new Point2D(p2.X, p2.Y));
                        rotatedVertices.Add(new Point2D(p2.X + BarsParam.ALength, p2.Y));
                        updatePolyline(p, rotatedVertices, -resAngle);
                    }
                    break;
                case "dh1sh2":
                    {
                        rotatedVertices.Add(new Point2D(p1.X + BarsParam.ALength, p1.Y));
                        rotatedVertices.Add(new Point2D(p1.X, p1.Y));
                        rotatedVertices.Add(new Point2D(p2.X, p2.Y));
                        rotatedVertices.Add(new Point2D(p2.X + BarsParam.ALength, p2.Y));
                        rotatedVertices.Add(new Point2D(p2.X + BarsParam.ALength, p2.Y - BarsParam.BLength));
                        updatePolyline(p, rotatedVertices, -resAngle);
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
        private static void updatePolyline(Polyline p, List<Point2D> rotatedVertices, double angle)
        {
            Rotator.RotatePoints(rotatedVertices, angle);
            p.AddVertexAt(0, new Point2d(rotatedVertices[0].X, rotatedVertices[0].Y), 0, 0, 0);
            p.AddVertexAt(1, new Point2d(rotatedVertices[1].X, rotatedVertices[1].Y), 0, 0, 0);
            for (int i = p.NumberOfVertices - 1; i >= 2; i--)
            {
                p.RemoveVertexAt(i);
            }
            for (int i = 2; i < rotatedVertices.Count; i++)
            {
                p.AddVertexAt(i, new Point2d(rotatedVertices[i].X, rotatedVertices[i].Y), 0, 0, 0);
            }
        }
    }
}
