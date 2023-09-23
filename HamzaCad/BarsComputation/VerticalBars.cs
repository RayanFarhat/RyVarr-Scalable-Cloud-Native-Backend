using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.AutoCAD.DatabaseServices;


namespace HamzaCad.BarsComputation
{
    public partial class VerticalBars
    {
        public static List<Polyline> getVerticalBars(List<Point2D> vertices) {
            List<Polyline> verticalBars = new List<Polyline>();
            List<Line2D> lines = new List<Line2D>();
            // get all Vertical lines
            BarsComputer.ed.WriteMessage(vertices.Count.ToString() + "\n");
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
            List<Line2D> AllVerticalLines = new List<Line2D>();// include Line from point to intersect, where startpoint is upperPoint

            int orginalLen = lines.Count;
            for (int i = 0; i < orginalLen; i++)
            {
                BarsComputer.ed.WriteMessage("____________________________________________\n");

                BarsComputer.ed.WriteMessage("StartPoint x " + lines[i].StartPoint.X.ToString() +
                    "StartPoint y " + lines[i].StartPoint.Y.ToString() + "\n");
                BarsComputer.ed.WriteMessage("||||||" + "\n");
                BarsComputer.ed.WriteMessage("EndPoint x " + lines[i].EndPoint.X.ToString() +
                    "EndPoint y " + lines[i].EndPoint.Y.ToString() + "\n");

                Line2D l = findIntersectVerticalLine(vertices, Hlines, lines[i]);
                if (l != null)
                {
                    AllVerticalLines.Add(l);
                }
            }
            MergeSortVerticalLine.Sort(AllVerticalLines);

            for (int i = 0; i < AllVerticalLines.Count; i++)
            {

            }
            return verticalBars;
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
