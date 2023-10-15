using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamzaCad.BarsComputation
{
    public class DrawingBarGenerator
    {
        public static List<DrawingBar> DrawingPolygons(List<Rectangle> rectangles)
        {

            List<DrawingBar> verticalBars = new List<DrawingBar>();
            for (int i = 0; i < rectangles.Count; i++)
            {
                var x = (rectangles[i].Xleft + rectangles[i].Xright) / 2;
                var top = rectangles[i].Yupper - BarsComputer.BarSpacing;
                var down = rectangles[i].Ylower + BarsComputer.BarSpacing;
                DBText dbText = new DBText();
                dbText.TextString = ((rectangles[i].Xright - rectangles[i].Xleft) / BarsComputer.BarSpacing).ToString("0");
                dbText.Position = new Point3d(x + (BarsComputer.BarSpacing / 4), (top + down) / 2, 0.0);
                dbText.Height = 12.0;
                verticalBars.Add(new DrawingBar(getBarpolyline(top, down, x), dbText));
            }
            return verticalBars;
        }
        // the bar is vertical
        //               
        //    5 _________  6 0
        //      |        |
        //     4 -----|  |
        //           3|  |
        //            |  |
        //            |  |
        //          2 ---- 1
        private static Polyline getBarpolyline(double top, double down, double x)
        {
            Polyline polyline = new Polyline();
            polyline.AddVertexAt(0, new Point2d(x, down), 0, 0, 0);
            polyline.AddVertexAt(1, new Point2d(x, top), 0, 0, 0);
            if (BarsComputer.withEar)
            {
                polyline.AddVertexAt(2, new Point2d(x - BarsComputer.earLength, top), 0, 0, 0);
            }
            return polyline;
        }
    }
}
