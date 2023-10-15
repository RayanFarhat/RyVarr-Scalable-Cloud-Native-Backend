using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace HamzaCad.BarsComputation
{
    public class BarsComputer
    {
        public static Editor ed;
        public static List<DrawingBar> bars;
        public static double BarSpacing = 30.0;
        public static bool isHorizontal;


        public static List<DrawingBar> getBars(Polyline shape)
        {
            List<Point2D> vertices = new List<Point2D>();
            int numVertices = shape.NumberOfVertices;
            for (int i = 0; i < numVertices; i++)
            {
                Point2d vertex = shape.GetPoint2dAt(i);
                Point2D p = new Point2D(vertex.X, vertex.Y);
                vertices.Add(p);
            }

            // rotate the polygon
            double angle = Rotator.GetRotationAngleToXOrY(vertices[0], vertices[1]);
            Rotator.RotatePoints(vertices, angle);

            /* now we work with Rectilinear polygon that his lines always parallel to X or Y */
            isHorizontal = false;
            bars =  VerticalBars.getVerticalBars(vertices);

            List<Point2D> clonedvertices = new List<Point2D>(vertices);
            Rotator.RotatePoints(clonedvertices, 90);
            isHorizontal = true;
            var Hbars = VerticalBars.getVerticalBars(clonedvertices);
            Rotator.RotatePolylinebars(Hbars, -90, vertices[0]);
            bars.AddRange(Hbars);
            //rotate to orginal shape
            Rotator.RotatePolylinebars(bars, -angle, vertices[0]);
            return bars;
        }
    }
}
