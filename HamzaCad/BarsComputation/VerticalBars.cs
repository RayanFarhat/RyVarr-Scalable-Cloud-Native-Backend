using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;


namespace HamzaCad.BarsComputation
{
    public class VerticalBars
    {
        public static List<Polyline> getVerticalBars(List<Point2D> vertices) {
            List<Polyline> verticalBars = new List<Polyline>();
            List<Line2D> lines = new List<Line2D>();

            // get all Vertical lines
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                if (vertices[i].X == vertices[i + 1].X)
                {
                    Line2D l = new Line2D(vertices[i], vertices[i + 1]);
                    lines.Add(l);
                }
            }

            // get the horizontal lines
            List<Line2D> Hlines = new List<Line2D>();
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                if (vertices[i].Y == vertices[i + 1].Y)
                {
                    Line2D l = new Line2D(vertices[i], vertices[i + 1]);
                    Hlines.Add(l);
                }
            }

            // get the other Vertical lines that start from vertix and end with horizontal line Intersect point
            int orginalLen = lines.Count;
            for (int i = 0; i < orginalLen; i++)
            {
                Line2D l = findIntersectVerticalLine(vertices, Hlines, lines[i]);
                if (l != null)
                {
                    lines.Add(l);
                }
            }
            return verticalBars;
        }

        private static Line2D findIntersectVerticalLine(List<Point2D> vertices,List<Line2D> Hlines, Line2D l) {
            Point2D upperPointY,lowerPointY;
            if(l.StartPoint.Y < l.EndPoint.Y)
            {
                upperPointY = new Point2D(l.EndPoint.X, l.EndPoint.Y);
                lowerPointY = new Point2D(l.StartPoint.X,l.StartPoint.Y);
            }
            else
            {
                upperPointY = new Point2D(l.StartPoint.X, l.StartPoint.Y);
                lowerPointY = new Point2D(l.EndPoint.X, l.EndPoint.Y);
            }


            return null;
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
    }
}
