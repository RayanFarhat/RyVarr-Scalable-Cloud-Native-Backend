using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.Windows.Data;
using HamzaCad.AutoCADAdapter;
using HamzaCad.DrawingParameters;

namespace HamzaCad.src.Winforms
{
    public partial class UC_barShapes : UserControl
    {
        public UC_barShapes()
        {
            InitializeComponent();
            shapeWithLetters.Paint += shapeWithLetters_Paint;
            BarShapesPanelContainer.AutoScroll = true;

            BarShapeBtn s1 = new BarShapeBtn("l");
            s1.Checked = true;
            BarShapeBtn s2 = new BarShapeBtn("sh1");
            BarShapeBtn s3 = new BarShapeBtn("sh2");
            BarShapeBtn s4 = new BarShapeBtn("shBoth");
            BarShapeBtn s5 = new BarShapeBtn("dh1");
            BarShapeBtn s6 = new BarShapeBtn("dh2");
            BarShapeBtn s7 = new BarShapeBtn("dhBoth");
            BarShapeBtn s8 = new BarShapeBtn("sh1dh2");
            BarShapeBtn s9 = new BarShapeBtn("dh1sh2");
            s1.CheckedChanged += s1.RadioPanel_CheckedChanged;
            s2.CheckedChanged += s2.RadioPanel_CheckedChanged;
            s3.CheckedChanged += s3.RadioPanel_CheckedChanged;
            s4.CheckedChanged += s4.RadioPanel_CheckedChanged;
            s5.CheckedChanged += s5.RadioPanel_CheckedChanged;
            s6.CheckedChanged += s6.RadioPanel_CheckedChanged;
            s7.CheckedChanged += s7.RadioPanel_CheckedChanged;
            s8.CheckedChanged += s8.RadioPanel_CheckedChanged;
            s9.CheckedChanged += s9.RadioPanel_CheckedChanged;
            BarShapesPanelContainer.Controls.Add(s1);
            BarShapesPanelContainer.Controls.Add(s2);
            BarShapesPanelContainer.Controls.Add(s3);
            BarShapesPanelContainer.Controls.Add(s4);
            BarShapesPanelContainer.Controls.Add(s5);
            BarShapesPanelContainer.Controls.Add(s6);
            BarShapesPanelContainer.Controls.Add(s7);
            BarShapesPanelContainer.Controls.Add(s8);
            BarShapesPanelContainer.Controls.Add(s9);


            A.Text = BarsParam.ALength.ToString();
            A.ValidatingType = typeof(double);
            A.TextChanged += onA;
            B.Text = BarsParam.BLength.ToString();
            B.ValidatingType = typeof(double);
            B.TextChanged += onB;
        }
        private void shapeWithLetters_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.Black, 2);
            Point point1 = new Point(shapeWithLetters.Width - 20 - shapeWithLetters.Width / 4, shapeWithLetters.Height / 4);
            Point point2 = new Point(shapeWithLetters.Width - 20, shapeWithLetters.Height / 4);
            Point p3 = new Point(shapeWithLetters.Width - 20, shapeWithLetters.Height - shapeWithLetters.Height / 4);
            Point p4 = new Point(20, shapeWithLetters.Height - shapeWithLetters.Height / 4);
            Point[] curvePoints = new Point[] { point1, point2, p3, p4 };

            // Draw polygon to screen.
            e.Graphics.DrawLines(pen1, curvePoints);
        }
        private void onA(object sender, EventArgs e)
        {
            try
            {
                BarsParam.ALength = Double.Parse(A.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onB(object sender, EventArgs e)
        {
            try
            {
                BarsParam.BLength = Double.Parse(B.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
    }
}
