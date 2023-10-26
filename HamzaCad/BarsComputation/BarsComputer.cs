using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace HamzaCad.BarsComputation
{
    public class BarsComputer
    {
        public static Editor ed;
        public static List<DrawingBar> bars;
        public static bool isVertical = true;

        // for form
        public static string lang = "Eng";

        public static double BarSpacing = 30.0;
        public static bool drawVertical { get; set; } = true ;
        public static bool drawHorizantal { get; set; } = true;
        public static bool withEar { get; set; } = true;
        public static double earLength { get; set; } = 15.0;
        public static double arrowScale { get; set; } = 5.0;
        public static double arrowBlockingLineLength { get; set; } = 15.0;
        public static double fontSize { get; set; } = 15.0;



        public static List<DrawingBar> getBars(Polyline shape)
        {
            var m = new MainWindow();
            m.ShowDialog();// will stop the proccess until window is closed
            
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

            bars = new List<DrawingBar>();
            if (drawVertical) {
                isVertical = true;
                bars =  VerticalBars.getVerticalBars(vertices);
            }
            if (drawHorizantal)
            {
                isVertical = false;
                List<Point2D> clonedvertices = new List<Point2D>(vertices);
                Rotator.RotatePoints(clonedvertices, 90);
                var Hbars = VerticalBars.getVerticalBars(clonedvertices);
                Rotator.RotatePolylinebars(Hbars, -90, vertices[0]);
                bars.AddRange(Hbars);
            }
            //rotate to orginal shape
            Rotator.RotatePolylinebars(bars, -angle, vertices[0]);
            return bars;
        }
    }
}
