using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using HamzaCad.Utils;
using HttpClientHandler = HamzaCad.Utils.HttpClientHandler;

namespace HamzaCad.BarsComputation
{
    public class BarsComputer
    {
        public static Editor ed;
        public static List<DrawingBar> bars;
        public static bool isVertical = true;

        // for form
        public static string lang { get; set; } = "Eng";

        public static double BarSpacing { get; set; } = 30.0;
        public static bool drawVertical { get; set; } = true ;
        public static bool drawHorizantal { get; set; } = true;
        public static bool withEar { get; set; } = true;
        public static double earLength { get; set; } = 15.0;
        public static double arrowScale { get; set; } = 5.0;
        public static double arrowBlockingLineLength { get; set; } = 15.0;
        public static double fontSize { get; set; } = 15.0;
        public static bool iSTopBars { get; set; } = true;
        public static string topBarSymbol { get; set; } = "T.B";
        public static string bottomrBarSymbol { get; set; } = "B.B";
        public static double Diameter { get; set; } = 12.0; 
        public static double BarPolySpace { get; set; } = 5.0;
        public static string upperText { get; set; } = "<>{Q}%%C{D}@{S} {TB}";
        public static string lowerText { get; set; } = "L={L}";
        public static double MaxBarLength { get; set; } = 800;
        public static double MeetingCircleRadius { get; set; } = 3.0;

        public static int ironColor { get; set; } = 4; 
        public static int IronLineWeight { get; set; } = 35;

        public async static Task<List<DrawingBar>> getBars(Polyline shape)
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

            HttpClientHandler c = new HttpClientHandler();
            double resAngle = await c.Req(new Angle(vertices[0].X, vertices[0].Y, vertices[1].X, vertices[1].Y));
            if (resAngle == 1000)
            {
                return new List<DrawingBar>();
            }

            // rotate the polygon
            //double angle = Rotator.GetRotationAngleToXOrY(vertices[0], vertices[1]);
            Rotator.RotatePoints(vertices, resAngle);
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
            Rotator.RotatePolylinebars(bars, -resAngle, vertices[0]);
            return bars;
        }
    }
}
