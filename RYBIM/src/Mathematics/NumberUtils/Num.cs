using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Mathematics
{
    public static class Num
    {
        public static bool IsFirstBiggerThanSecond(double d1, double d2)
        {
            if (Math.Abs(d1 - d2) < 0.0001)
                return false;
            if (d1 > d2)
                return true;
            return false;
        }
        public static bool IsFirstSmallerThanSecond(double d1, double d2)
        {
            if (Math.Abs(d1 - d2) < 0.0001)
                return false;
            if (d1 < d2)
                return true;
            return false;
        }
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
