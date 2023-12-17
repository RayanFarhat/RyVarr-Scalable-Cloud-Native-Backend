using System;
using System.Collections.Generic;

namespace HamzaCad.BarsComputation
{
    public class PointInsidePolygon
    {
        static bool onLine(Line2D l1, Point2D p)
        {
            var maxX = Math.Max(l1.StartPoint.X, l1.EndPoint.X);
            var minX = Math.Min(l1.StartPoint.X, l1.EndPoint.X);
            var maxY = Math.Max(l1.StartPoint.Y, l1.EndPoint.Y);
            var minY = Math.Min(l1.StartPoint.Y, l1.EndPoint.Y);
            // Check whether p is on the line or not
            if ((p.X < maxX || Math.Abs(p.X - maxX) < 0.0001)
                && (p.X > minX || Math.Abs(p.X - minX) < 0.0001)
                && (
                (p.Y < maxY || Math.Abs(p.Y - maxY) < 0.0001)
                    && (p.Y > minY || Math.Abs(p.Y - minY) < 0.0001)
                    ))
                return true;

            return false;
        }

        static int direction(Point2D a, Point2D b, Point2D c)
        {
            double val = (b.Y - a.Y) * (c.X - b.X)
              - (b.X - a.X) * (c.Y - b.Y);

            //if (val == 0)
            if (Math.Abs(val) < 0.0001)
                    // Collinear
                    return 0;

            else if (val < 0)

                // Anti-clockwise direction
                return 2;

            // Clockwise direction
            return 1;
        }

        static int isIntersect(Line2D l1, Line2D l2)
        {
            // Four direction for two lines and points of other line
            int dir1 = direction(l1.StartPoint, l1.EndPoint, l2.StartPoint);
            int dir2 = direction(l1.StartPoint, l1.EndPoint, l2.EndPoint);
            int dir3 = direction(l2.StartPoint, l2.EndPoint, l1.StartPoint);
            int dir4 = direction(l2.StartPoint, l2.EndPoint, l1.EndPoint);

            // When intersecting
            if (dir1 != dir2 && dir3 != dir4)
                return 1;

            // When p2 of line2 are on the line1
            if (dir1 == 0 && onLine(l1, l2.StartPoint))
                return 1;

            // When p1 of line2 are on the line1
            if (dir2 == 0 && onLine(l1, l2.EndPoint))
                return 1;

            // When p2 of line1 are on the line2
            if (dir3 == 0 && onLine(l2, l1.StartPoint))
                return 1;

            // When p1 of line1 are on the line2
            if (dir4 == 0 && onLine(l2, l1.EndPoint))
                return 1;

            return 0;
        }

        public static bool checkInside(List<Point2D> poly, int n, Point2D p)
        {

            // When polygon has less than 3 edge, it is not polygon
            if (n < 3)
                return false;

            // Create a point at infinity, y is same as point p
            Point2D pt = new Point2D(999999.0, p.Y);
            Line2D exline = new Line2D(p, pt);
            int count = 0;
            int i = 0;
            do
            {

                // Forming a line from two consecutive points of
                // poly
                Line2D side = new Line2D(poly[i], poly[(i + 1) % n]);
                // If side is intersects exline
                if (isIntersect(side, exline) == 1)
                {
                    if (direction(side.StartPoint, p, side.EndPoint) == 0)
                    {
                        // this not 100% right
                        return !onLine(side, p);
                    }
                    count++;
                }
                i = (i + 1) % n;
            } while (i != 0);

            // When count is even
            if ((count & 1) == 0) return false;

            // When count is odd
            return true;
        }
    }
}
