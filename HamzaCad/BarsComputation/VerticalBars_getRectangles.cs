using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.GraphicsInterface;
using System.Windows.Media.Animation;

namespace HamzaCad.BarsComputation
{
    public partial class VerticalBars
    {
        // startpoint is upper point
        private static List<Rectangle> getRectangles(List<Line2D> lines, List<Line2D> Hlines, List<Point2D> vertices)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i+1; j < lines.Count; j++)
                {
         
                    if (DoLinesMakeRectangle(lines[i], lines[j]))
                    {
                        var y1 = lines[i].StartPoint.Y;
                        var y2 = lines[i].EndPoint.Y;

                        rectangles.Add(new Rectangle(y1, y2, lines[i].StartPoint.X, lines[j].StartPoint.X));
                        break;
                    }
                }
            }
            //for now the X of the rectangles are right but the Y may be wrong, so we fix it with the horizantal lines
            for (int i = 0; i < rectangles.Count; i++)
            {
                for (int j = 0; j < Hlines.Count; j++)
                {
                    // if there is Hline that is start and end from the rect
                    if (
                        (isEqual(Hlines[j].StartPoint.X, rectangles[i].Xleft) && isEqual(Hlines[j].EndPoint.X, rectangles[i].Xright)) ||
                        (isEqual(Hlines[j].StartPoint.X, rectangles[i].Xright) && isEqual(Hlines[j].EndPoint.X, rectangles[i].Xleft))
                        )
                    {
                        //if line is inside the rect
                        if(rectangles[i].Yupper > Hlines[j].StartPoint.Y && Hlines[j].StartPoint.Y > rectangles[i].Ylower)
                        {
                            var middleX = (rectangles[i].Xleft + rectangles[i].Xright) / 2;
                            // check where to cut the rectangle
                            Point2D topP = new Point2D(middleX, (rectangles[i].Yupper+Hlines[j].StartPoint.Y)/2);
                            Point2D downP = new Point2D(middleX, (rectangles[i].Ylower + Hlines[j].StartPoint.Y)/2);
                            bool topInside = PointInsidePolygoncs.checkInside(vertices, vertices.Count, topP);
                            bool downInside = PointInsidePolygoncs.checkInside(vertices, vertices.Count, downP);

                            if (topInside && !downInside)
                            {
                                rectangles[i].Ylower = Hlines[j].StartPoint.Y;
                            }
                            else if (!topInside && downInside)
                            {
                                rectangles[i].Yupper = Hlines[j].StartPoint.Y;
                            }
                        }

                    }
                }
            }
            return rectangles;
        }

        public static bool DoLinesMakeRectangle(Line2D line1, Line2D line2)
        {
            // Check if the lines are coincident (same Y coordinates for both endpoints).
            if (isEqual(line1.StartPoint.Y, line2.StartPoint.Y) &&
                isEqual(line1.EndPoint.Y, line2.EndPoint.Y))
            {
                return true; // Lines are coincident and will intersect.
            }

            // Check if one line is completely above the other.
            if (line1.EndPoint.Y > line2.StartPoint.Y || line1.StartPoint.Y < line2.EndPoint.Y)
            {
                return false; // Lines do not intersect.
            }

            // Lines have different Y coordinates and will intersect.
            return true;
        }
    }
}
