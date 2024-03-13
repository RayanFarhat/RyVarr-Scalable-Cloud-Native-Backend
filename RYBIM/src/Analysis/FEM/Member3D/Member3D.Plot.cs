using RYBIM.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Analysis
{
    public partial class Member3D
    {
        public void plot_Shear(Direction D, string combo_name = "Combo 1")
        {
            Check_segments(combo_name);
            string SeriesName="somthing wrong";
            if (D == Direction.Fy) {
                SeriesName = "Shear on Y-axis";
            }
            else if (D == Direction.Fz)
            {
                SeriesName = "Shear on Z-axis";
            }
            var min = Min_Shear(D, combo_name);
            var max = Max_Shear(D, combo_name);
            var f = new MemberPlot(
                SeriesName,
                Shear_Array(D, 999, combo_name),
                L(),
                min,
                max,
                $"Min Shear is {min}",
                $"Max Shear is {max}"
                );
            f.Show();
        }
        public void plot_Moment(Direction D, string combo_name = "Combo 1")
        {
            Check_segments(combo_name);
            string SeriesName = "somthing wrong";
            if (D == Direction.Mz)
            {
                SeriesName = "Moment on XY-axis";
            }
            else if (D == Direction.Fz)
            {
                SeriesName = "Moment on XZ-axis";
            }
            var min = Min_Moment(D, combo_name);
            var max = Max_Moment(D, combo_name);
            var f = new MemberPlot(
                SeriesName,
                Moment_Array(D, 999, combo_name),
                L(),
                min,
                max,
                $"Min Moment is {min}",
                $"Max Moment is {max}"
                );
            f.Show();
        }
        public void plot_Deflection(Direction D, string combo_name = "Combo 1")
        {
            Check_segments(combo_name);
            string SeriesName = "somthing wrong";
            if (D == Direction.Fy)
            {
                SeriesName = "Deflection on Y-axis";
            }
            else if (D == Direction.Fz)
            {
                SeriesName = "Deflection on Z-axis";
            }
            else if (D == Direction.Fx)
            {
                SeriesName = "Deflection on x-axis";
            }
            var min = Min_Deflection(D, combo_name);
            var max = Max_Deflection(D, combo_name);
            var f = new MemberPlot(
                SeriesName,
                Deflection_Array(D, 999, combo_name),
                L(),
                min,
                max,
                $"Min Deflection is {min}",
                $"Max Deflection is {max}"
                );
            f.Show();
        }
    }
}
