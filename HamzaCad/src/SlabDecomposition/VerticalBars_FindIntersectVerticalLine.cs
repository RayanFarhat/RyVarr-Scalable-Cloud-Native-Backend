using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.GraphicsInterface;
using HamzaCad.Utils;
using HamzaCad.SlabDrawing;

namespace HamzaCad.SlabDecomposition
{
    public partial class VerticalBars
    {
        // check if l if it expand to infinite will intersect with one or more of the horizontal lines and this line is inside the polygon
        private static Line2D findIntersectVerticalLine(List<Point2D> vertices, List<Line2D> Hlines, Line2D l)
        {
            Point2D upperPoint, lowerPoint;
            if (l.StartPoint.Y < l.EndPoint.Y)
            {
                upperPoint = new Point2D(l.EndPoint.X, l.EndPoint.Y);
                lowerPoint = new Point2D(l.StartPoint.X, l.StartPoint.Y);
            }
            else
            {
                upperPoint = new Point2D(l.StartPoint.X, l.StartPoint.Y);
                lowerPoint = new Point2D(l.EndPoint.X, l.EndPoint.Y);
            }
            Line2D lowestLineUpportPoint = getLowestLineUpportPoint(vertices, Hlines, upperPoint);
            Line2D highestLineLowerPoint = getHighestLineLowerPoint(vertices, Hlines, lowerPoint);

            if (lowestLineUpportPoint != null && highestLineLowerPoint != null)
            {
                // if this then the two points are like this
                // __________*__________ lowestLineUpportPoint
                //           |
                //           | inside poly
                //-----------*upperPoint
                //           |
                //           |
                //           |
                //___________*lowerPoint
                //           |
                //           | inside poly
                // __________*__________ highestLineLowerPoint

                Point2D s = new Point2D(upperPoint.X, lowestLineUpportPoint.StartPoint.Y);
                Point2D e = new Point2D(upperPoint.X, highestLineLowerPoint.StartPoint.Y);
                return new Line2D(s, e);
            }
            else if (lowestLineUpportPoint != null && highestLineLowerPoint == null)
            {
                // if this then the two points are like this
                // __________*__________ lowestLineUpportPoint
                //           |
                //           | inside poly
                //-----------*upperPoint
                //           |
                //           |
                //           |
                //___________*lowerPoint
                //     outside poly

                Point2D s = new Point2D(upperPoint.X, lowestLineUpportPoint.StartPoint.Y);
                Point2D e = new Point2D(upperPoint.X, lowerPoint.Y);
                return new Line2D(s, e);
            }
            else if (lowestLineUpportPoint == null && highestLineLowerPoint != null)
            {
                // if this then the two points are like this
                //       outside poly
                //-----------*upperPoint
                //           |
                //           |
                //           |
                //___________*lowerPoint
                //           |
                //           | inside poly
                // __________*__________ highestLineLowerPoint

                Point2D s = new Point2D(upperPoint.X, upperPoint.Y);
                Point2D e = new Point2D(upperPoint.X, highestLineLowerPoint.StartPoint.Y);
                return new Line2D(s, e);
            }
            // if null then the two points are like this
            //       outside poly
            //-----------*upperPoint
            //           |
            //           |
            //           |
            //___________*lowerPoint
            //      outside poly
            return new Line2D(upperPoint, lowerPoint);
        }
        private static Line2D getLowestLineUpportPoint(List<Point2D> poly, List<Line2D> horizontalLines, Point2D upperPoint)
        {
            // check  horizontal line that is on the top of the upper point
            double? Y = null;
            Line2D chosenLine = null;
            for (int i = 0; i < horizontalLines.Count; i++)
            {
                // if line is not intersect the x of the line then ignore
                if (!isPointXbetweenHorizontalLine(horizontalLines[i], upperPoint))
                    continue;
                if (Y == null)
                {
                    if (upperPoint.Y < horizontalLines[i].StartPoint.Y && !DoubleUtils.IsEqual(upperPoint.Y, horizontalLines[i].StartPoint.Y))
                    {
                        Y = horizontalLines[i].StartPoint.Y;
                        chosenLine = horizontalLines[i];
                    }
                }
                else
                {
                    if (upperPoint.Y < horizontalLines[i].StartPoint.Y && Y > horizontalLines[i].StartPoint.Y &&
                        !DoubleUtils.IsEqual(upperPoint.Y, horizontalLines[i].StartPoint.Y) && !DoubleUtils.IsEqual((double)Y, horizontalLines[i].StartPoint.Y))
                    {
                        Y = horizontalLines[i].StartPoint.Y;
                        chosenLine = horizontalLines[i];
                    }
                }
            }
            // check if the line between this line and upper point is inside or outside the polygon
            //_______________
            //    P
            // upperPoint
            // p is in middle
            if (Y != null)
            {
                Point2D p = new Point2D(upperPoint.X, ((double)Y + upperPoint.Y) / 2);
                if (PointInsidePolygon.checkInside(poly, poly.Count, p))
                {
                    return chosenLine;
                }
            }
            return null;
        }
        private static Line2D getHighestLineLowerPoint(List<Point2D> poly, List<Line2D> horizontalLines, Point2D lowerPoint)
        {
            // check  horizontal line that is on the lower of the upper point
            double? Y = null;
            Line2D chosenLine = null;
            for (int i = 0; i < horizontalLines.Count; i++)
            {
                // if line is not intersect the x of the line then ignore
                if (!isPointXbetweenHorizontalLine(horizontalLines[i], lowerPoint))
                    continue;
                if (Y == null)
                {
                    if (lowerPoint.Y > horizontalLines[i].StartPoint.Y && !DoubleUtils.IsEqual(lowerPoint.Y, horizontalLines[i].StartPoint.Y))
                    {
                        Y = horizontalLines[i].StartPoint.Y;
                        chosenLine = horizontalLines[i];
                    }
                }
                else
                {
                    if (lowerPoint.Y > horizontalLines[i].StartPoint.Y && Y < horizontalLines[i].StartPoint.Y &&
                        !DoubleUtils.IsEqual(lowerPoint.Y, horizontalLines[i].StartPoint.Y) && !DoubleUtils.IsEqual((double)Y, horizontalLines[i].StartPoint.Y))
                    {
                        Y = horizontalLines[i].StartPoint.Y;
                        chosenLine = horizontalLines[i];
                    }
                }
                
            }
            // check if the line between this line and upper point is inside or outside the polygon
            // lowerPoint
            //    P
            // _______________
            // p is in middle
            if (Y != null) {
                Point2D p = new Point2D(lowerPoint.X, ((double)Y + lowerPoint.Y) / 2);
                if (PointInsidePolygon.checkInside(poly, poly.Count, p))
                {
                    return chosenLine;
                }
            }

            return null;
        }
        
        private static bool isPointXbetweenHorizontalLine(Line2D horizontalLine,Point2D p)
        {
            if (horizontalLine.StartPoint.X > horizontalLine.EndPoint.X)
            {
                if (p.X < horizontalLine.StartPoint.X && p.X > horizontalLine.EndPoint.X)
                {
                   return true;
                }
            }
            if (horizontalLine.StartPoint.X < horizontalLine.EndPoint.X)
            {
                if (p.X > horizontalLine.StartPoint.X && p.X < horizontalLine.EndPoint.X)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
