using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HamzaCad.src.Winforms
{
    public partial class UC_carefulNotes : UserControl
    {
        public UC_carefulNotes()
        {
            InitializeComponent();
            examplePanel.Paint += paint;
        }
        private void paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.Black, 2);
            Point[] curvePoints = new Point[] { };
            Point p1 = new Point(examplePanel.Width /3, 20);
            Point p2 = new Point(examplePanel.Width / 6, 20);
            Point p3 = new Point(examplePanel.Width / 6, examplePanel.Height - examplePanel.Height / 6);
            Point p4 = new Point(examplePanel.Width - examplePanel.Width / 6, examplePanel.Height - examplePanel.Height / 6);
            curvePoints = new Point[] { p1, p2,p3,p4 };
            e.Graphics.DrawLines(pen1, curvePoints);
            Pen pen2 = new Pen(Color.Red, 3);
            e.Graphics.DrawLine(pen2, examplePanel.Width / 3, 20, examplePanel.Width - examplePanel.Width / 6, examplePanel.Height - examplePanel.Height / 6);

        }
    }
}
