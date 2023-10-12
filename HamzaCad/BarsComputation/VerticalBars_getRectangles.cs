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
            for (int i = 0; i < lines.Count; i++)
            {
                BarsComputer.ed.WriteMessage("____________________________\n");

                BarsComputer.ed.WriteMessage("top y " + lines[i].StartPoint.Y + "\n");
                BarsComputer.ed.WriteMessage("down y " + lines[i].EndPoint.Y + "\n");
                BarsComputer.ed.WriteMessage("x " + lines[i].StartPoint.X + "\n");

            }
            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i+1; j < lines.Count; j++)
                {
                    if (DoLinesMakeRectangle(lines[i], lines[j]))
                    {
                        var y1 = lines[i].StartPoint.Y < lines[j].StartPoint.Y ? lines[i].StartPoint.Y : lines[j].StartPoint.Y;
                        var y2 = lines[i].EndPoint.Y > lines[j].EndPoint.Y ? lines[i].EndPoint.Y : lines[j].EndPoint.Y;
                        var rect = new Rectangle(y1, y2, lines[i].StartPoint.X, lines[j].StartPoint.X);
                        // check of already that is rectangle that take some area of the new rectangle, if yes then no need to add the rectangle
                        var canADD = true;
                        if (rectangles.Count > 0)
                        {
                            for (int k = 0; k < rectangles.Count; k++)
                            {
                                // if x of rectangles meet AND new rectangle y  is inside the old one 
                                if (
                                    (isEqual(rectangles[k].Xleft, rect.Xleft) || isEqual(rectangles[k].Xright, rect.Xright)) &&
                                    (
                                    isEqual(rectangles[k].Ylower, rect.Ylower) || isEqual(rectangles[k].Yupper, rect.Yupper) ||
                                    (rectangles[k].Yupper > rect.Yupper && rect.Ylower > rectangles[k].Ylower) ||
                                    (rectangles[k].Yupper > rect.Yupper && rect.Ylower > rectangles[k].Ylower)
                                    )
                                   )
                                {
                                    canADD = false;
                                }
                            }
                        }

                        if (canADD == true)
                            rectangles.Add(rect);
                    }
                }
            }

            //for (int i = 0; i < rectangles.Count; i++)
            //{
            //    BarsComputer.ed.WriteMessage("____________________________\n");

            //    BarsComputer.ed.WriteMessage("top y " + rectangles[i].Yupper + "\n");
            //    BarsComputer.ed.WriteMessage("down y " + rectangles[i].Ylower + "\n");
            //    BarsComputer.ed.WriteMessage("left x " + rectangles[i].Xleft + "\n");
            //    BarsComputer.ed.WriteMessage("right x " + rectangles[i].Xright + "\n");
            //}

            //for now the X of the rectangles are right but the Y may be wrong, so we fix it with the horizantal lines
            for (int i = 0; i < rectangles.Count; i++)
            {
                for (int j = 0; j < Hlines.Count; j++)
                {
                    // if there is Hline that is start or end from the rect
                    if (
                        isEqual(Hlines[j].StartPoint.X, rectangles[i].Xleft) || isEqual(Hlines[j].EndPoint.X, rectangles[i].Xright) ||
                        isEqual(Hlines[j].StartPoint.X, rectangles[i].Xright) || isEqual(Hlines[j].EndPoint.X, rectangles[i].Xleft)
                        )
                    {
                        //if line is inside the rect
                        if (rectangles[i].Yupper > Hlines[j].StartPoint.Y && Hlines[j].StartPoint.Y > rectangles[i].Ylower &&
                            (Hlines[j].EndPoint.X < rectangles[i].Xright || isEqual(Hlines[j].EndPoint.X, rectangles[i].Xright))
                            )
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
