using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RYBIM.Analysis
{
    public partial class Form1 : Form
    {
        public Form1(double[][] xy)
        {
            InitializeComponent();
            var objChart = chart1.ChartAreas[0];
            objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            objChart.AxisX.Minimum = 0;
            objChart.AxisX.Maximum = 10;
            objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            objChart.AxisY.Minimum = -0.005;
            objChart.AxisY.Maximum = 0.005;
            chart1.Series.Clear();
            Series series = new Series("Series Name") { ChartType = SeriesChartType.Line};
            chart1.Series.Add(series);
            for (int i = 0; i < xy[0].Length; i++)
            {
            chart1.Series["Series Name"].Points.AddXY(xy[0][i], xy[1][i]);
            }
            objChart.AxisX.MajorGrid.Enabled = false;
            objChart.AxisY.MajorGrid.Enabled = false;
        }
    }
}
