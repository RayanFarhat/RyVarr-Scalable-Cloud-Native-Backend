using System;
using System.Collections.Generic;
using System.Windows.Media;
using Autodesk.AutoCAD.DatabaseServices;
using HamzaCad.Utils;
using HamzaCad.SlabDrawing;
using HamzaCad.AutoCADAdapter;


namespace HamzaCad.SlabDecomposition
{
    public partial class VerticalBars
    {
        public static List<DrawingBar> getVerticalBars(List<Point2D> vertices) {

            List<Line2D> lines = new List<Line2D>();
            List<Line2D> Hlines = new List<Line2D>();

            for (int i = 0; i < vertices.Count-1; i++)
            {
               
            }

            // get all Vertical lines
            for (int i = 0; i < vertices.Count -1; i++)
            {
                // compare if equal to last 4 digits
                if (Math.Abs(vertices[i].X - vertices[i + 1].X) < 0.0001)
                {
                    Line2D l = new Line2D(vertices[i], vertices[i + 1]);
                    lines.Add(l);
                }
                // if not then triangle or horizontol
                else if (Math.Abs(vertices[i].Y - vertices[i + 1].Y) < 0.0001)
                {
                    Line2D l = new Line2D(vertices[i], vertices[i + 1]);
                    // startpoint is always left
                    if (vertices[i].Y > vertices[i + 1].Y)
                    {
                        l = new Line2D(vertices[i + 1], vertices[i]);
                    }
                    Hlines.Add(l);
                }
                // then triangle
                else
                {
                    Line2D vl = getVerticalFromTriangle(vertices, i);
                    if (vl != null)
                    {
                        lines.Add(vl);
                    }
                    
                    Line2D hl = getHorizontolFromTriangle(vertices, i);
                    if (hl != null)
                    {
                        Hlines.Add(hl);
                    }
                }
            }

            // get the other Vertical lines that start from vertix and end with horizontal line Intersect point
            List<Line2D> AllVerticalLines = new List<Line2D>();// include Line from point to intersect, where startpoint is upperPoint

            for (int i = 0; i < lines.Count; i++)
            {
                Line2D l = findIntersectVerticalLine(vertices, Hlines, lines[i]);
                if (l != null)
                {
                    AllVerticalLines.Add(l);
                }
            }

            //for (int i = 0; i < AllVerticalLines.Count; i++)
            //{
            //    BarsComputer.ed.WriteMessage("_________________\n");9
            //    BarsComputer.ed.WriteMessage("x "+ AllVerticalLines[i].StartPoint.X + "\n");
            //    BarsComputer.ed.WriteMessage("y top " + AllVerticalLines[i].StartPoint.X + "\n");


            //}

            while (MergeVerticalLinesWithSameX(AllVerticalLines, vertices))
            {
                // keep merging until no merging left
            }
            MergeSortVerticalLine.Sort(AllVerticalLines);

            return DrawingBarGenerator.DrawingPolygons(
                RectangleBuilder.getRectangles(AllVerticalLines, Hlines, vertices)
                );
        }

        // merge lines with same X to one line if it is inside the polygon
        private static bool MergeVerticalLinesWithSameX(List<Line2D> lines, List<Point2D> vertices)
        {
            var sameXDic = new Dictionary<Line2D, Line2D>();
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    // compare if equal to last 4 digits   
                    if (Math.Abs(lines[i].StartPoint.X - lines[j].StartPoint.X) < 0.0001)
                    {
                        // we know that startpoint is the upperpoint
                        if (DoubleUtils.IsFirstSmallerOrEqualThanSecond(lines[i].StartPoint.Y, lines[j].EndPoint.Y))
                        {
                            Point2D p = new Point2D(lines[i].StartPoint.X, (lines[i].StartPoint.Y + lines[j].EndPoint.Y) / 2);
                            // if can be merged
                            if (PointInsidePolygon.checkInside(vertices, vertices.Count, p))
                            {
                                sameXDic.Add(lines[i], lines[j]);
                            }
                        }
                        else if (DoubleUtils.IsFirstSmallerOrEqualThanSecond(lines[j].StartPoint.Y, lines[i].EndPoint.Y))
                        {
                            Point2D p = new Point2D(lines[i].StartPoint.X, (lines[j].StartPoint.Y + lines[i].EndPoint.Y) / 2);
                            // if can be merged
                            if (PointInsidePolygon.checkInside(vertices, vertices.Count, p))
                            {
                                sameXDic.Add(lines[i], lines[j]);
                            }
                        }
                        else if(DoubleUtils.IsFirstBiggerOrEqualThanSecond(lines[i].StartPoint.Y, lines[j].EndPoint.Y) &&
                            DoubleUtils.IsFirstSmallerOrEqualThanSecond(lines[i].StartPoint.Y, lines[j].StartPoint.Y))
                        {
                            sameXDic.Add(lines[i], lines[j]);
                        }
                        else if (DoubleUtils.IsFirstBiggerOrEqualThanSecond(lines[j].StartPoint.Y, lines[i].EndPoint.Y) &&
                            DoubleUtils.IsFirstSmallerOrEqualThanSecond(lines[j].StartPoint.Y, lines[i].StartPoint.Y))
                        {
                            sameXDic.Add(lines[i], lines[j]);
                        }
                    }
                }
            }
            foreach (KeyValuePair<Line2D, Line2D> kvp in sameXDic)
            {
                Line2D line1 = kvp.Key;
                Line2D line2 = kvp.Value;
                List<Line2D> toRemove = new List<Line2D>
                {
                    line1,
                    line2
                };
                lines.RemoveAll(toRemove.Contains);

                Point2D upper;
                Point2D lower;

                if (line1.StartPoint.Y > line2.StartPoint.Y)
                {
                    upper = line1.StartPoint;
                }
                else { upper = line2.StartPoint; }
                if (line1.EndPoint.Y < line2.EndPoint.Y)
                {
                    lower = line1.EndPoint;
                }
                else { lower = line2.EndPoint; }

                lines.Add(new Line2D(upper, lower));
            }

            // if we merge lines this time
            if (sameXDic.Count > 0) return true;
            else return false;
        }

        private static Line2D getVerticalFromTriangle(List<Point2D> vertices,int i)
        {
            Line2D line = null;

            if (DoubleUtils.IsEqual(vertices[i-1].Y, vertices[i].Y))
            {
                    Point2D p = new Point2D(vertices[i].X, (vertices[i].Y+ vertices[i+1].Y)/2);
                    bool isInside = PointInsidePolygon.checkInside(vertices,vertices.Count,p);
                    if (isInside == true)
                    {
                        return new Line2D(vertices[i],
                            new Point2D(vertices[i].X, vertices[i+1].Y));
                    }
                    else
                    {
                        p.X = vertices[i + 1].X;
                        isInside = PointInsidePolygon.checkInside(vertices, vertices.Count, p);
                        if (isInside == true)
                        {
                            return new Line2D(new Point2D(vertices[i + 1].X, vertices[i].Y), vertices[i+1]);
                        }
                    }
            }
            return line;
        }
        private static Line2D getHorizontolFromTriangle(List<Point2D> vertices, int i)
        {
            Line2D Hline = null;

            if (DoubleUtils.IsEqual(vertices[i - 1].X, vertices[i].X))
            {
                Point2D p = new Point2D((vertices[i].X + vertices[i + 1].X) / 2, vertices[i].Y);
                bool isInside = PointInsidePolygon.checkInside(vertices, vertices.Count, p);
                if (isInside == true)
                {
                    return new Line2D(vertices[i],
                        new Point2D(vertices[i+1].X, vertices[i].Y));
                }
                else
                {
                    p.Y = vertices[i + 1].Y;
                    isInside = PointInsidePolygon.checkInside(vertices, vertices.Count, p);
                    if (isInside == true)
                    {
                        return new Line2D(new Point2D(vertices[i].X, vertices[i + 1].Y), vertices[i+1]);
                    }
                }
            }
            return Hline;
        }
        private static bool existOnLines(List<Line2D> lines, Line2D l)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if (l.StartPoint.X == lines[i].StartPoint.X && l.StartPoint.Y == lines[i].StartPoint.Y
                    && l.EndPoint.X == lines[i].EndPoint.X && l.EndPoint.Y == lines[i].EndPoint.Y) { return true; }
                if (l.StartPoint.X == lines[i].EndPoint.X && l.StartPoint.Y == lines[i].EndPoint.Y
                    && l.EndPoint.X == lines[i].StartPoint.X && l.EndPoint.Y == lines[i].StartPoint.Y) { return true; }
            }
            return false;
        }

        private static double getLowestY(List<Line2D> horizontalLines)
        {
            double lowestY = horizontalLines[0].StartPoint.Y;

            for (int i = 1; i < horizontalLines.Count; i++)
            {
                if (lowestY > horizontalLines[i].StartPoint.Y)
                    lowestY = horizontalLines[i].StartPoint.Y;
            }
            return lowestY;
        }

        private static double getHighestY(List<Line2D> horizontalLines)
        {
            double highestY = horizontalLines[0].StartPoint.Y;

            for (int i = 1; i < horizontalLines.Count; i++)
            {
                if (highestY < horizontalLines[i].StartPoint.Y)
                    highestY = horizontalLines[i].StartPoint.Y;
            }
            return highestY;
        }
    }
}
