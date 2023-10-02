using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;


namespace HamzaCad.BarsComputation
{
    public partial class VerticalBars
    {
        public static List<DrawingBar> getVerticalBars(List<Point2D> vertices) {

            List<Polyline> verticalBars = new List<Polyline>();
            List<Line2D> lines = new List<Line2D>();
            // get all Vertical lines
            BarsComputer.ed.WriteMessage(vertices.Count.ToString() + "\n");
            for (int i = 0; i < vertices.Count -1; i++)
            {
                if (vertices[i].X.ToString() == vertices[i + 1].X.ToString())
                {
                    Line2D l = new Line2D(vertices[i], vertices[i + 1]);
                    lines.Add(l);
                }
            }
            // get the horizontal lines
            List<Line2D> Hlines = new List<Line2D>();
            for (int i = 0; i < vertices.Count -1; i++)
            {

                if (vertices[i].Y.ToString() == vertices[i + 1].Y.ToString())
                {
                    Line2D l = new Line2D(vertices[i], vertices[i + 1]);
                    Hlines.Add(l);
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
            MergeVerticalLinesWithSameX(AllVerticalLines, vertices);
            MergeSortVerticalLine.Sort(AllVerticalLines);

            return DrawingPolygons(getRectangles(AllVerticalLines));
        }

        // merge lines with same X to one line if it is inside the polygon
        private static void MergeVerticalLinesWithSameX(List<Line2D> lines, List<Point2D> vertices)
        {
            var sameXDic = new Dictionary<Line2D, Line2D>();
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    if (lines[i].StartPoint.X.ToString() == lines[j].StartPoint.X.ToString())
                    {
                        // we know that startpoint is the upperpoint
                        if (lines[i].StartPoint.Y <= lines[j].EndPoint.Y)
                        {
                            Point2D p = new Point2D(lines[i].StartPoint.X, (lines[i].StartPoint.Y + lines[j].EndPoint.Y) / 2);
                            // if can be merged
                            if (PointInsidePolygoncs.checkInside(vertices, vertices.Count, p))
                            {
                                sameXDic.Add(lines[i], lines[j]);
                            }
                        }
                        else if (lines[j].StartPoint.Y <= lines[i].EndPoint.Y)
                        {
                            Point2D p = new Point2D(lines[i].StartPoint.X, (lines[j].StartPoint.Y + lines[i].EndPoint.Y) / 2);
                            // if can be merged
                            if (PointInsidePolygoncs.checkInside(vertices, vertices.Count, p))
                            {
                                sameXDic.Add(lines[i], lines[j]);
                            }
                        }
                        else if(lines[i].StartPoint.Y >= lines[j].EndPoint.Y && lines[i].StartPoint.Y <= lines[j].StartPoint.Y)
                        {
                            sameXDic.Add(lines[i], lines[j]);
                        }
                        else if (lines[j].StartPoint.Y >= lines[i].EndPoint.Y && lines[j].StartPoint.Y <= lines[i].StartPoint.Y)
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
