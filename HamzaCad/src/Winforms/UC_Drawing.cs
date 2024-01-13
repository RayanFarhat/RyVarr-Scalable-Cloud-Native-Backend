using HamzaCad.SlabDrawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HamzaCad.DrawingParameters;
using Autodesk.AutoCAD.Windows.Data;

namespace HamzaCad.src.Winforms
{
    public partial class UC_Drawing : UserControl
    {
        public UC_Drawing()
        {
            InitializeComponent();

            TextSize.Text = DrawingParam.TextSize.ToString();
            TextSize.ValidatingType = typeof(double);
            TextSize.TextChanged += onTextSize;

            IntersectCircleRadius.Text = DrawingParam.IntersectCircleRadius.ToString();
            IntersectCircleRadius.ValidatingType = typeof(double);
            IntersectCircleRadius.TextChanged += onIntersectCircleRadius;

            ArrowSize.Text = DrawingParam.ArrowSize.ToString();
            ArrowSize.ValidatingType = typeof(double);
            ArrowSize.TextChanged += onArrowSize;

            BlockingLineEnabled.Checked = DrawingParam.BlockingLineEnabled;
            BlockingLineEnabled.CheckedChanged += onBlockingLineEnabled;

            ArrowBlockingLineLength.Text = DrawingParam.ArrowBlockingLineLength.ToString();
            ArrowBlockingLineLength.ValidatingType = typeof(double);
            ArrowBlockingLineLength.TextChanged += onArrowBlockingLineLength;
        }
        private void onTextSize(object sender, EventArgs e)
        {
            try
            {
                DrawingParam.TextSize = Double.Parse(TextSize.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }

        private void onIntersectCircleRadius(object sender, EventArgs e)
        {
            try
            {
                DrawingParam.IntersectCircleRadius = Double.Parse(IntersectCircleRadius.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onArrowSize(object sender, EventArgs e)
        {
            try
            {
                DrawingParam.ArrowSize = Double.Parse(ArrowSize.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onBlockingLineEnabled(object sender, EventArgs e)
        {
            DrawingParam.BlockingLineEnabled = BlockingLineEnabled.Checked;
        }
        private void onArrowBlockingLineLength(object sender, EventArgs e)
        {
            try
            {
                DrawingParam.ArrowBlockingLineLength = Double.Parse(ArrowBlockingLineLength.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
    }
}
