using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HamzaCad.Utils;
using HamzaCad.SlabDecomposition;
using HamzaCad.DrawingParameters;


namespace HamzaCad.SlabDrawing
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
                var top = rectangles[i].Yupper - BarsParam.SideCoverY;
                var down = rectangles[i].Ylower + BarsParam.SideCoverY;
                // if quantity is 0 then no need to draw bar
                if (((rectangles[i].Xright - rectangles[i].Xleft) / BarsParam.BarSpacing) != 0)
                {
                    verticalBars.Add(new DrawingBar(getBarpolyline(top, down, xPosition), getTexts(rectangles[i], top, down, xPosition),
                        getArrows(rectangles[i]), getBlockingLines(rectangles[i]),
                        getMeetingCircle(rectangles[i], xPosition)));
                }
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
            polyline.ColorIndex = BarsParam.IronColor;
            polyline.LineWeight = (LineWeight)BarsParam.IronLineWeight;
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
            rightArrow.AppendVertex(new Point3d(rect.Xright - BarsParam.SideCoverY, y, 0));
            rightArrow.AppendVertex(new Point3d(xMiddle, y, 0));
            rightArrow.HasArrowHead = true;
            //rightArrow.DimensionStyle = db.Dimstyle;
            rightArrow.Dimscale = DrawingParam.ArrowSize;
            arrows.Add(rightArrow);
            var leftArrow = new Leader();
            leftArrow.AppendVertex(new Point3d(rect.Xleft + BarsParam.SideCoverY, y, 0));
            leftArrow.AppendVertex(new Point3d(xMiddle, y, 0));
            leftArrow.HasArrowHead = true;
            //rightArrow.DimensionStyle = db.Dimstyle;
            leftArrow.Dimscale = DrawingParam.ArrowSize;
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
                (new Point3d(rect.Xright - BarsParam.SideCoverY, y + DrawingParam.ArrowBlockingLineLength/2, 0), 
                new Point3d(rect.Xright - BarsParam.SideCoverY, y - DrawingParam.ArrowBlockingLineLength / 2, 0)
                );
            Line leftPlockingLine = new Line(
                new Point3d(rect.Xleft + BarsParam.SideCoverY, y + DrawingParam.ArrowBlockingLineLength / 2, 0),
                new Point3d(rect.Xleft + BarsParam.SideCoverY, y - DrawingParam.ArrowBlockingLineLength / 2, 0)
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
            Cir.Radius = DrawingParam.IntersectCircleRadius;
            return Cir;
        }

        private static List<DBText> getTexts(Rectangle rect,double top, double down, double x)
        {
            var texts = new List<DBText>();

            DBText upperText = new DBText();
            //upperText.TextString = "<>"+ quantity + "%%C"+diameter+"@"+spacing+" "+ barType;
            upperText.TextString = getFinalUpperText(rect);
            upperText.Position = new Point3d(x + DrawingParam.TextSize*0.1, (top + down) / 2, 0.0);
            upperText.Height = DrawingParam.TextSize;
            if (BarsComputer.isVertical)
                upperText.Rotation = 90 * Math.PI / 180;

            DBText lowerText = new DBText();
            //lowerText.TextString = "L= " + (top-down).ToString("0.##");
            lowerText.TextString = getFinalLowerText(rect);
            lowerText.Position = new Point3d(x - DrawingParam.TextSize * 1.2, (top + down) / 2, 0.0);
            // fix when lower text is on top of upper text
            if (lowerText.Position.X < upperText.Position.X)
            {
                lowerText.Position = new Point3d(x + DrawingParam.TextSize * 1.3, (top + down) / 2, 0.0);
            }
            lowerText.Height = DrawingParam.TextSize;
            if (BarsComputer.isVertical)
                lowerText.Rotation = 90 * Math.PI / 180;

            texts.Add(upperText);
            texts.Add(lowerText);
            return texts;
        }

        private static string getFinalUpperText(Rectangle rect)
        {
            string result = TextEdirotParam.TopText;

            var quantity = ((rect.Xright - rect.Xleft) / BarsParam.BarSpacing).ToString("0");
            string pattern = @"\{Q\}";
            result = Regex.Replace(result, pattern, quantity);

            var diameter = BarsParam.Diameter.ToString("0.##");
            pattern = @"\{D\}";
            result = Regex.Replace(result, pattern, diameter);

            var spacing = BarsParam.BarSpacing.ToString("0");
            pattern = @"\{S\}";
            result = Regex.Replace(result, pattern, spacing);

            var barType = BarsParam.iSTopBars ? TextEdirotParam.TopBarSymbol : TextEdirotParam.BottomBarSymbol;
            pattern = @"\{TB\}";
            result = Regex.Replace(result, pattern, barType);

            var len = rect.Yupper - rect.Ylower - (BarsParam.SideCoverY * 2);
            int len2 = BarsParam.RoundLen * (int)Math.Round(len / BarsParam.RoundLen);
            pattern = @"\{L\}";
            result = Regex.Replace(result, pattern, len2.ToString());

            return result;
        }
        private static string getFinalLowerText(Rectangle rect)
        {
            string result = TextEdirotParam.BottomText;

            var quantity = ((rect.Xright - rect.Xleft) / BarsParam.BarSpacing).ToString("0");
            string pattern = @"\{Q\}";
            result = Regex.Replace(result, pattern, quantity);

            var diameter = BarsParam.Diameter.ToString("0.##");
            pattern = @"\{D\}";
            result = Regex.Replace(result, pattern, diameter);

            var spacing = BarsParam.BarSpacing.ToString("0");
            pattern = @"\{S\}";
            result = Regex.Replace(result, pattern, spacing);

            var barType = BarsParam.iSTopBars ? TextEdirotParam.TopBarSymbol : TextEdirotParam.BottomBarSymbol;
            pattern = @"\{TB\}";
            result = Regex.Replace(result, pattern, barType);

            var len = rect.Yupper - rect.Ylower - (BarsParam.SideCoverY * 2);
            int len2 = BarsParam.RoundLen * (int)Math.Round(len / BarsParam.RoundLen);
            pattern = @"\{L\}";
            result = Regex.Replace(result, pattern, len2.ToString());

            return result;
        }
    }
}
