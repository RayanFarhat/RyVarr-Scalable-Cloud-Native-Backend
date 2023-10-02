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
        private static List<Rectangle> getRectangles(List<Line2D> lines)
        {
            //todo  still lines isnt right when i tried rectangle poyline
            for (int i = 0; i < lines.Count; i++)
            {
                BarsComputer.ed.WriteMessage("________" + "\n");
                BarsComputer.ed.WriteMessage("upper Y " + lines[i].StartPoint.Y + "\n");
                BarsComputer.ed.WriteMessage("lower Y " + lines[i].EndPoint.Y + "\n");
                BarsComputer.ed.WriteMessage("X " + lines[i].EndPoint.X + "\n");

            }

            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i+1; j < lines.Count; j++)
                {
                    if (DoLinesMakeRectangle(lines[i], lines[j]))
                    {
                        rectangles.Add(new Rectangle(lines[i].StartPoint.Y, lines[i].EndPoint.Y, lines[i].StartPoint.X, lines[j].StartPoint.X));
                        BarsComputer.ed.WriteMessage("________" + "\n");
                        BarsComputer.ed.WriteMessage("upper Y "+ lines[i].StartPoint.Y + "\n");
                        BarsComputer.ed.WriteMessage("lower Y " + lines[i].EndPoint.Y + "\n");
                        BarsComputer.ed.WriteMessage("left X " + lines[i].StartPoint.X + "\n");
                        BarsComputer.ed.WriteMessage("right X " + lines[j].StartPoint.Y + "\n");
                        break;

                    }
                }
            }
            return rectangles;
        }

        public static bool DoLinesMakeRectangle(Line2D line1, Line2D line2)
        {
            double x = line1.StartPoint.X; // Assuming both lines have the same X coordinate.

            // Check if the lines are coincident (same Y coordinates for both endpoints).
            if (line1.StartPoint.Y.ToString() == line2.StartPoint.Y.ToString() &&
                line1.EndPoint.Y.ToString() == line2.EndPoint.Y.ToString())
            {
                return true; // Lines are coincident and will intersect.
            }

            // Check if one line is completely above the other.
            if ((line1.StartPoint.Y > line2.EndPoint.Y && line1.EndPoint.Y > line2.EndPoint.Y) ||
                (line1.StartPoint.Y < line2.EndPoint.Y && line1.EndPoint.Y < line2.EndPoint.Y))
            {
                return false; // Lines do not intersect.
            }

            // Check if one line is completely below the other.
            if ((line1.StartPoint.Y > line2.StartPoint.Y && line1.EndPoint.Y > line2.StartPoint.Y) ||
                (line1.StartPoint.Y < line2.StartPoint.Y && line1.EndPoint.Y < line2.StartPoint.Y))
            {
                return false; // Lines do not intersect.
            }

            // Lines have different Y coordinates and will intersect.
            return true;
        }
    }
}
