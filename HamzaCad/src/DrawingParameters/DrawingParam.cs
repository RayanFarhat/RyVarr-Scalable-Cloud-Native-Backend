using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamzaCad.DrawingParameters
{
    public class DrawingParam
    {
        public static double TextSize { get; set; } = 15.0;
        public static double IntersectCircleRadius { get; set; } = 3.0;
        public static double ArrowSize { get; set; } = 5.0;
        public static bool BlockingLineEnabled { get; set; } = true;
        public static double ArrowBlockingLineLength { get; set; } = 15.0;
    }
}
