using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace HamzaCad.BarsComputation
{
    public partial class VerticalBars
    {
        private static List<DrawingBar> DrawingPolygons(List<Rectangle> rectangles)
        {
            for (int i = 0; i < rectangles.Count; i++)
            {
                BarsComputer.ed.WriteMessage("____________________________\n");

                BarsComputer.ed.WriteMessage("top y " + rectangles[i].Yupper +"\n");
                BarsComputer.ed.WriteMessage("down y " + rectangles[i].Ylower + "\n");
                BarsComputer.ed.WriteMessage("left y " + rectangles[i].Xleft + "\n");
                BarsComputer.ed.WriteMessage("right y " + rectangles[i].Xright + "\n");

            }
            List<DrawingBar> verticalBars = new List<DrawingBar>();
            for (int i = 0; i < rectangles.Count; i++)
            {
                var x = (rectangles[i].Xleft + rectangles[i].Xright) / 2;
                var top = rectangles[i].Yupper - BarsComputer.BarSpacing;
                var down = rectangles[i].Ylower + BarsComputer.BarSpacing;
                DBText dbText = new DBText();
                dbText.TextString = ((rectangles[i].Xright -rectangles[i].Xleft) /BarsComputer.BarSpacing).ToString("0");
                dbText.Position = new Point3d(x+ (BarsComputer.BarSpacing/5), (top+down)/2, 0.0);
                dbText.Height = 12.0;
                verticalBars.Add(new DrawingBar(getBarpolyline(top,down,x), dbText));
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
        private static Polyline getBarpolyline(double top, double down,double x)
        {
            Polyline polyline = new Polyline();
            polyline.AddVertexAt(0, new Point2d(x, top), 0, 0, 0);
            polyline.AddVertexAt(1, new Point2d(x, down), 0, 0, 0);
            polyline.AddVertexAt(2, new Point2d(x - (BarsComputer.BarSpacing / 3), down), 0, 0, 0);
            polyline.AddVertexAt(3, new Point2d(x - (BarsComputer.BarSpacing / 3), top -(BarsComputer.BarSpacing / 3) ), 0, 0, 0);
            polyline.AddVertexAt(4, new Point2d(x - BarsComputer.BarSpacing, top - (BarsComputer.BarSpacing / 3)), 0, 0, 0);
            polyline.AddVertexAt(5, new Point2d(x - BarsComputer.BarSpacing, top ), 0, 0, 0);
            polyline.AddVertexAt(6, new Point2d(x, top), 0, 0, 0);
            return polyline;
        }
    }
}
