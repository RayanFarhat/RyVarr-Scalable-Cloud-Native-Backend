using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;

namespace HamzaCad.BarsComputation
{
    public class Point2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    public class Line2D
    {
        public Point2D StartPoint { get; set; }
        public Point2D EndPoint { get; set; }

        public Line2D(Point2D startPoint, Point2D endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
    public class DrawingBar
    {
        public Polyline Polygon { get; set; }
        public List<DBText> Texts { get; set; }
        public List<Leader> Arrows { get; set; }
        public List<Line> ArrowsBlockingLines { get; set; }


        public DrawingBar(Polyline Polygon, List<DBText> texts, List<Leader> arrows, List<Line> arrowsBlockingLines)
        {
            this.Polygon = Polygon;
            this.Texts = texts;
            Arrows = arrows;
            ArrowsBlockingLines = arrowsBlockingLines;
        }
    }

    public class Rectangle
    {
        public double Yupper { get; set; }
        public double Ylower { get; set; }
        public double Xleft { get; set; }
        public double Xright { get; set; }

        public Rectangle(double yu, double yl,double xl,double xr)
        {
            Yupper = yu;
            Ylower = yl;
            Xleft = xl;
            Xright = xr;
        }
    }
    public class DoubleUtils
    {
        public static bool IsFirstBiggerOrEqualThanSecond(double d1, double d2)
        {
            if (d1 > d2)
                return true;
            if (Math.Abs(d1 - d2) < 0.0001)
                return true;
            return false;
        }
        public static bool IsFirstSmallerOrEqualThanSecond(double d1, double d2)
        {
            if (d1 < d2)
                return true;
            if (Math.Abs(d1 - d2) < 0.0001)
                return true;
            return false;
        }
        public static bool IsEqual(double d1, double d2)
        {
            if (Math.Abs(d1 - d2) < 0.0001)
                return true;
            return false;
        }
    }
}
