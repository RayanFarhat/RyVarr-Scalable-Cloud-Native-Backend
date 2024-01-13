using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HamzaCad.AutoCADAdapter;

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


            //BarShapeRadioPanel s4 = new BarShapeRadioPanel();
            //BarShapeRadioPanel s5 = new BarShapeRadioPanel();
            s1.CheckedChanged += RadioPanel_CheckedChanged;
            s2.CheckedChanged += RadioPanel_CheckedChanged;
            //s3.CheckedChanged += RadioPanel_CheckedChanged;
            //s4.CheckedChanged += RadioPanel_CheckedChanged;
            //s5.CheckedChanged += RadioPanel_CheckedChanged;
            BarShapesPanelContainer.Controls.Add(s1);
            BarShapesPanelContainer.Controls.Add(s2);
            BarShapesPanelContainer.Controls.Add(s3);
            BarShapesPanelContainer.Controls.Add(s4);
            BarShapesPanelContainer.Controls.Add(s5);
            BarShapesPanelContainer.Controls.Add(s6);
            BarShapesPanelContainer.Controls.Add(s7);
            BarShapesPanelContainer.Controls.Add(s8);
            BarShapesPanelContainer.Controls.Add(s9);
        }
        private void shapeWithLetters_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.Black, 2);
            Point point1 = new Point(shapeWithLetters.Width - 20 - shapeWithLetters.Width / 2, shapeWithLetters.Height / 4);
            Point point2 = new Point(shapeWithLetters.Width - 20, shapeWithLetters.Height / 4);
            Point p3 = new Point(shapeWithLetters.Width - 20, shapeWithLetters.Height - shapeWithLetters.Height / 4);
            Point p4 = new Point(20, shapeWithLetters.Height - shapeWithLetters.Height / 4);
            Point[] curvePoints = new Point[] { point1, point2, p3, p4 };

            // Draw polygon to screen.
            e.Graphics.DrawLines(pen1, curvePoints);
        }
        private void RadioPanel_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
