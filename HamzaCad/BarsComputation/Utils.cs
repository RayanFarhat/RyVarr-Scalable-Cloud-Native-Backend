using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
