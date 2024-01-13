using System;
using System.Collections.Generic;

namespace HamzaCad.DrawingParameters
{
    public class BarsParam
    {
        public static double BarSpacing { get; set; } = 30.0;
        public static bool DrawVertical { get; set; } = true;
        public static bool DrawHorizontal { get; set; } = true;
        public static bool iSTopBars { get; set; } = false;
        public static double Diameter { get; set; } = 12.0;
        public static double SideCoverX { get; set; } = 2.5;
        public static double SideCoverY { get; set; } = 2.5;
        public static double MaxBarLength { get; set; } = 600;
        public static int IronColor { get; set; } = 4;
        public static int IronLineWeight { get; set; } = 35;
    }
}
