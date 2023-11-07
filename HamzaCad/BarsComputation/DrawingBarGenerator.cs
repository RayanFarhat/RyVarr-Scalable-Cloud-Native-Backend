using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                var xPosition = (rectangles[i].Xleft + rectangles[i].Xright) / 2 - ((rectangles[i].Xright - rectangles[i].Xleft)/4);
                var top = rectangles[i].Yupper - BarsComputer.BarPolySpace;
                var down = rectangles[i].Ylower + BarsComputer.BarPolySpace;
   
                verticalBars.Add(new DrawingBar(getBarpolyline(top, down, xPosition), getTexts(rectangles[i],top,down,xPosition),
                    getArrows(rectangles[i]), getBlockingLines(rectangles[i])));
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
        private static List<Leader> getArrows(Rectangle rect)
        {
            var xMiddle = (rect.Xleft + rect.Xright) / 2;
            var yMiddle = (rect.Ylower + rect.Yupper) / 2 - ((rect.Xright - rect.Xleft) / 10);

            var arrows = new List<Leader>();
            var rightArrow = new Leader();
            rightArrow.AppendVertex(new Point3d(rect.Xright - BarsComputer.BarPolySpace, yMiddle, 0));
            rightArrow.AppendVertex(new Point3d(xMiddle, yMiddle, 0));
            rightArrow.HasArrowHead = true;
            //rightArrow.DimensionStyle = db.Dimstyle;
            rightArrow.Dimscale = 10.0;
            arrows.Add(rightArrow);
            var leftArrow = new Leader();
            leftArrow.AppendVertex(new Point3d(rect.Xleft + BarsComputer.BarPolySpace, yMiddle, 0));
            leftArrow.AppendVertex(new Point3d(xMiddle, yMiddle, 0));
            leftArrow.HasArrowHead = true;
            //rightArrow.DimensionStyle = db.Dimstyle;
            leftArrow.Dimscale = BarsComputer.arrowScale;
            arrows.Add(leftArrow);
            return arrows;
        }
        private static List<Line> getBlockingLines(Rectangle rect)
        {
            var yMiddle = (rect.Ylower + rect.Yupper) / 2 - ((rect.Xright - rect.Xleft) / 10);
            var lines = new List<Line>();
            Line rightPlockingLine = new Line
                (new Point3d(rect.Xright - BarsComputer.BarPolySpace, yMiddle + BarsComputer.arrowBlockingLineLength/2, 0), 
                new Point3d(rect.Xright - BarsComputer.BarPolySpace, yMiddle - BarsComputer.arrowBlockingLineLength/2, 0)
                );
            Line leftPlockingLine = new Line(
                new Point3d(rect.Xleft + BarsComputer.BarPolySpace, yMiddle + BarsComputer.arrowBlockingLineLength / 2, 0),
                new Point3d(rect.Xleft + BarsComputer.BarPolySpace, yMiddle - BarsComputer.arrowBlockingLineLength / 2, 0)
                );
            lines.Add(rightPlockingLine);
            lines.Add(leftPlockingLine);
            return lines;
        }

        private static List<DBText> getTexts(Rectangle rect,double top, double down, double x)
        {
            var texts = new List<DBText>();

            DBText upperText = new DBText();
            //upperText.TextString = "<>"+ quantity + "%%C"+diameter+"@"+spacing+" "+ barType;
            upperText.TextString = getFinalUpperText(rect);
            upperText.Position = new Point3d(x + BarsComputer.fontSize*0.1, (top + down) / 2, 0.0);
            upperText.Height = BarsComputer.fontSize;
            if (BarsComputer.isVertical)
                upperText.Rotation = 90 * Math.PI / 180;

            DBText lowerText = new DBText();
            //lowerText.TextString = "L= " + (top-down).ToString("0.##");
            lowerText.TextString = getFinalLowerText(rect);
            lowerText.Position = new Point3d(x - BarsComputer.fontSize*1.1, (top + down) / 2, 0.0);
            lowerText.Height = BarsComputer.fontSize;
            if (BarsComputer.isVertical)
                lowerText.Rotation = 90 * Math.PI / 180;

            texts.Add(upperText);
            texts.Add(lowerText);
            return texts;
        }

        private static string getFinalUpperText(Rectangle rect)
        {
            string result = BarsComputer.upperText;

            var quantity = ((rect.Xright - rect.Xleft) / BarsComputer.BarSpacing).ToString("0");
            string pattern = @"\{Q\}";
            result = Regex.Replace(result, pattern, quantity);

            var diameter = BarsComputer.Diameter.ToString("0.##");
            pattern = @"\{D\}";
            result = Regex.Replace(result, pattern, diameter);

            var spacing = BarsComputer.BarSpacing.ToString("0");
            pattern = @"\{S\}";
            result = Regex.Replace(result, pattern, spacing);

            var barType = BarsComputer.iSTopBars ? BarsComputer.topBarSymbol : BarsComputer.bottomrBarSymbol;
            pattern = @"\{TB\}";
            result = Regex.Replace(result, pattern, barType);

            var len = rect.Yupper - rect.Ylower - (BarsComputer.BarPolySpace*2);
            pattern = @"\{L\}";
            result = Regex.Replace(result, pattern, len.ToString("0"));

            return result;
        }
        private static string getFinalLowerText(Rectangle rect)
        {
            string result = BarsComputer.lowerText;

            var quantity = ((rect.Xright - rect.Xleft) / BarsComputer.BarSpacing).ToString("0");
            string pattern = @"\{Q\}";
            result = Regex.Replace(result, pattern, quantity);

            var diameter = BarsComputer.Diameter.ToString("0.##");
            pattern = @"\{D\}";
            result = Regex.Replace(result, pattern, diameter);

            var spacing = BarsComputer.BarSpacing.ToString("0");
            pattern = @"\{S\}";
            result = Regex.Replace(result, pattern, spacing);

            var barType = BarsComputer.iSTopBars ? BarsComputer.topBarSymbol : BarsComputer.bottomrBarSymbol;
            pattern = @"\{TB\}";
            result = Regex.Replace(result, pattern, barType);

            var len = rect.Yupper - rect.Ylower - (BarsComputer.BarPolySpace * 2);
            pattern = @"\{L\}";
            result = Regex.Replace(result, pattern, len.ToString("0"));

            return result;
        }
    }
}
