using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using HamzaCad.Utils;
using HamzaCad.SlabDrawing;
using HamzaCad.DrawingParameters;
using HamzaCad.AutoCADAdapter;

namespace HamzaCad.SlabDecomposition
{
    public class RectangleBuilder
    {
        // startpoint is upper point
        public static List<Rectangle> getRectangles(List<Line2D> lines, List<Line2D> Hlines, List<Point2D> vertices)
        {
            //for (int i = 0; i < lines.Count; i++)
            //{
            //    BarsComputer.ed.WriteMessage("____________________________\n");

            //    BarsComputer.ed.WriteMessage("top y " + lines[i].StartPoint.Y + "\n");
            //    BarsComputer.ed.WriteMessage("down y " + lines[i].EndPoint.Y + "\n");
            //    BarsComputer.ed.WriteMessage("x " + lines[i].StartPoint.X + "\n");
            //}

            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    if (DoLinesMakeRectangle(i,j, lines))
                    {
                        var y1 = lines[i].StartPoint.Y < lines[j].StartPoint.Y ? lines[i].StartPoint.Y : lines[j].StartPoint.Y;
                        var y2 = lines[i].EndPoint.Y > lines[j].EndPoint.Y ? lines[i].EndPoint.Y : lines[j].EndPoint.Y;
                        var rect = new Rectangle(y1, y2, lines[i].StartPoint.X, lines[j].StartPoint.X);

                        //for now the X of the rectangles are right but the Y may be wrong, so we fix it with the horizantal lines
                        for (int k = 0; k < Hlines.Count; k++)
                        {
                            // if there is Hline that is start or end from the rect
                            if (
                                DoubleUtils.IsEqual(Hlines[k].StartPoint.X, rect.Xleft) ||
                                DoubleUtils.IsEqual(Hlines[k].EndPoint.X, rect.Xright) ||
                                DoubleUtils.IsEqual(Hlines[k].StartPoint.X, rect.Xright) ||
                                DoubleUtils.IsEqual(Hlines[k].EndPoint.X, rect.Xleft)
                                )
                            {
                                //if line is inside the rect
                                if (rect.Yupper > Hlines[k].StartPoint.Y && Hlines[k].StartPoint.Y > rect.Ylower &&
                                    (Hlines[k].EndPoint.X < rect.Xright || DoubleUtils.IsEqual(Hlines[k].EndPoint.X, rect.Xright))
                                    )
                                {
                                    var middleX = (rect.Xleft + rect.Xright) / 2;
                                    // check where to cut the rectangle
                                    Point2D topP = new Point2D(middleX, (rect.Yupper + Hlines[k].StartPoint.Y) / 2);
                                    Point2D downP = new Point2D(middleX, (rect.Ylower + Hlines[k].StartPoint.Y) / 2);
                                    bool topInside = PointInsidePolygon.checkInside(vertices, vertices.Count, topP);
                                    bool downInside = PointInsidePolygon.checkInside(vertices, vertices.Count, downP);

                                    if (topInside && !downInside)
                                    {
                                        rect.Ylower = Hlines[k].StartPoint.Y;
                                    }
                                    else if (!topInside && downInside)
                                    {
                                        rect.Yupper = Hlines[k].StartPoint.Y;
                                    }
                                }

                            }
                        }

                        // check of already that is rectangle that take some area of the new rectangle, if yes then no need to add the rectangle
                        var canADD = true;
                        if (rectangles.Count > 0)
                        {
                            for (int k = 0; k < rectangles.Count; k++)
                            {
                                // if x of rectangles meet AND new rectangle y  is inside the old one 
                                if (
                                        (
                                            DoubleUtils.IsEqual(rectangles[k].Xleft, rect.Xleft) ||
                                            DoubleUtils.IsEqual(rectangles[k].Xright, rect.Xright)
                                        ) &&
                                        (
                                            DoubleUtils.IsEqual(rectangles[k].Ylower, rect.Ylower) ||
                                            DoubleUtils.IsEqual(rectangles[k].Yupper, rect.Yupper) ||
                                            (rectangles[k].Yupper > rect.Yupper && rect.Yupper > rectangles[k].Ylower) ||
                                            (rectangles[k].Yupper > rect.Ylower && rect.Ylower > rectangles[k].Ylower)
                                        )
                                   )
                                {
                                    canADD = false;
                                }
                            }
                        }

                        if (canADD == true)
                        {
                            double smallX = (rect.Xright- rect.Xleft) / 10;
                            double centerY = (rect.Yupper + rect.Ylower) / 2;

                            // take two points inside the rect
                            // and check if the points in rectangle is inside the polygon
                            if (PointInsidePolygon.checkInside(vertices, vertices.Count, new Point2D(rect.Xleft + smallX, centerY))
                                &&
                                PointInsidePolygon.checkInside(vertices, vertices.Count, new Point2D(rect.Xright - smallX, centerY))
                                )
                            {
                                rectangles.Add(rect);
                            }
                        }
                    }
                }
            }

            //for (int i = 0; i < rectangles.Count; i++)
            //{
            //    Adapter.ed.WriteMessage("____________________________\n");

            //    Adapter.ed.WriteMessage("top y " + rectangles[i].Yupper + "\n");
            //    Adapter.ed.WriteMessage("down y " + rectangles[i].Ylower + "\n");
            //    Adapter.ed.WriteMessage("left x " + rectangles[i].Xleft + "\n");
            //    Adapter.ed.WriteMessage("right x " + rectangles[i].Xright + "\n");
            //}


            // spllit long rectangles
            for (int i = rectangles.Count - 1; i >= 0; i--)
            {
                var len = rectangles[i].Yupper - rectangles[i].Ylower;
                if (len > BarsParam.MaxBarLength)
                {
                    var connLen = BarsParam.Diameter * 6;//law
                    var rect1 = new Rectangle(rectangles[i].Yupper,
                        rectangles[i].Ylower+(len/2)-(connLen/2),
                        rectangles[i].Xleft, rectangles[i].Xright);
                    var rect2 = new Rectangle(rectangles[i].Yupper - (len / 2) + (connLen / 2),
                        rectangles[i].Ylower,
                        rectangles[i].Xleft, rectangles[i].Xright);
                    rectangles.RemoveAt(i);
                    rectangles.Add(rect1);
                    rectangles.Add(rect2);
                }
            }


            return rectangles;
        }

        // j have bigger x
        private static bool DoLinesMakeRectangle(int i, int j, List<Line2D> lines)
        {
            // Check if the lines are coincident (same Y coordinates for both endpoints).
            if (DoubleUtils.IsEqual(lines[i].StartPoint.Y, lines[j].StartPoint.Y) &&
                DoubleUtils.IsEqual(lines[i].EndPoint.Y, lines[j].EndPoint.Y))
            {
                return true; // Lines are coincident and will intersect.
            }

            // Check if one line is completely above the other.
            if (lines[i].EndPoint.Y > lines[j].StartPoint.Y || lines[i].StartPoint.Y < lines[j].EndPoint.Y)
            {
                return false; // Lines do not intersect.
            }

            // Lines have different Y coordinates and will intersect.
            return true;
        }
    }
}
