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
        private static List<double> arrowsY = new List<double>();
        public static List<DrawingBar> DrawingPolygons(List<Rectangle> rectangles)
        {
            arrowsY.Clear();
            Random random = new Random();
            int randomNumber = 0;
            List<DrawingBar> verticalBars = new List<DrawingBar>();
            for (int i = 0; i < rectangles.Count; i++)
            {
                int prev = randomNumber;
                while(prev == randomNumber)
                    randomNumber = random.Next(3, 10);
                var xPosition = 0.0;
                if (i%2==0)
                     xPosition = ((rectangles[i].Xleft + rectangles[i].Xright) / 2) - ((rectangles[i].Xright - rectangles[i].Xleft) / randomNumber);
                else
                     xPosition = ((rectangles[i].Xleft + rectangles[i].Xright) / 2) + ((rectangles[i].Xright - rectangles[i].Xleft) / randomNumber);
                var top = rectangles[i].Yupper - BarsComputer.BarPolySpace;
                var down = rectangles[i].Ylower + BarsComputer.BarPolySpace;
   
                verticalBars.Add(new DrawingBar(getBarpolyline(top, down, xPosition), getTexts(rectangles[i],top,down,xPosition),
                    getArrows(rectangles[i]), getBlockingLines(rectangles[i]),
                    getMeetingCircle(rectangles[i], xPosition)));
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
            polyline.ColorIndex = BarsComputer.ironColor;
            polyline.LineWeight = (LineWeight)BarsComputer.IronLineWeight;
            return polyline;
        }
        private static List<Leader> getArrows(Rectangle rect)
        {
            double y=(rect.Ylower +rect.Yupper) / 2 - ((rect.Xright - rect.Xleft) / 10);
            bool Yfount = false;
            for (int i = 0; i < arrowsY.Count; i++)
            {
                if (arrowsY[i] < rect.Yupper && arrowsY[i] > rect.Ylower)
                {
                    y = arrowsY[i];
                    Yfount = true;
                    break;
                }
            }
            if (!Yfount)
            {
                y = (rect.Ylower + rect.Yupper) / 2 - ((rect.Xright - rect.Xleft) / 10);
                arrowsY.Add(y);
            }


            var xMiddle = (rect.Xleft + rect.Xright) / 2;

            var arrows = new List<Leader>();
            var rightArrow = new Leader();
            rightArrow.AppendVertex(new Point3d(rect.Xright - BarsComputer.BarPolySpace, y, 0));
            rightArrow.AppendVertex(new Point3d(xMiddle, y, 0));
            rightArrow.HasArrowHead = true;
            //rightArrow.DimensionStyle = db.Dimstyle;
            rightArrow.Dimscale = 10.0;
            arrows.Add(rightArrow);
            var leftArrow = new Leader();
            leftArrow.AppendVertex(new Point3d(rect.Xleft + BarsComputer.BarPolySpace, y, 0));
            leftArrow.AppendVertex(new Point3d(xMiddle, y, 0));
            leftArrow.HasArrowHead = true;
            //rightArrow.DimensionStyle = db.Dimstyle;
            leftArrow.Dimscale = BarsComputer.arrowScale;
            arrows.Add(leftArrow);
            return arrows;
        }
        private static List<Line> getBlockingLines(Rectangle rect)
        {
            double y = (rect.Ylower + rect.Yupper) / 2 - ((rect.Xright - rect.Xleft) / 10);
            bool Yfount = false;
            for (int i = 0; i < arrowsY.Count; i++)
            {
                if (arrowsY[i] < rect.Yupper && arrowsY[i] > rect.Ylower)
                {
                    y = arrowsY[i];
                    Yfount = true;
                    break;
                }
            }
            if (!Yfount)
            {
                y = (rect.Ylower + rect.Yupper) / 2 - ((rect.Xright - rect.Xleft) / 10);
                arrowsY.Add(y);
            }
            var lines = new List<Line>();
            Line rightPlockingLine = new Line
                (new Point3d(rect.Xright - BarsComputer.BarPolySpace, y + BarsComputer.arrowBlockingLineLength/2, 0), 
                new Point3d(rect.Xright - BarsComputer.BarPolySpace, y - BarsComputer.arrowBlockingLineLength/2, 0)
                );
            Line leftPlockingLine = new Line(
                new Point3d(rect.Xleft + BarsComputer.BarPolySpace, y + BarsComputer.arrowBlockingLineLength / 2, 0),
                new Point3d(rect.Xleft + BarsComputer.BarPolySpace, y - BarsComputer.arrowBlockingLineLength / 2, 0)
                );
            lines.Add(rightPlockingLine);
            lines.Add(leftPlockingLine);
            return lines;
        }
        private static Circle getMeetingCircle(Rectangle rect,double xpos)
        {
            double y = (rect.Ylower + rect.Yupper) / 2 - ((rect.Xright - rect.Xleft) / 10);
            bool Yfount = false;
            for (int i = 0; i < arrowsY.Count; i++)
            {
                if (arrowsY[i] < rect.Yupper && arrowsY[i] > rect.Ylower)
                {
                    y = arrowsY[i];
                    Yfount = true;
                    break;
                }
            }
            if (!Yfount)
            {
                y = (rect.Ylower + rect.Yupper) / 2 - ((rect.Xright - rect.Xleft) / 10);
                arrowsY.Add(y);
            }
            Circle Cir = new Circle();
            Cir.Center = new Point3d(xpos, y, 0);
            Cir.Radius = BarsComputer.MeetingCircleRadius;
            return Cir;
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
