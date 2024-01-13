using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;

namespace HamzaCad.src.Winforms
{
    public class BarShapeBtn : RadioButton
    {
        public string ShapeType {  get; set; }
        public BarShapeBtn(string shapeType) { 
            this.Appearance = Appearance.Button;
            this.Text = "";
            this.Paint += panel1_Paint;
            this.Size = new System.Drawing.Size(280,150);
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.LightGray;
            this.FlatAppearance.CheckedBackColor = Color.White;
            ShapeType = shapeType;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.Black, 2);
            Point[] curvePoints = new Point[] {};
            switch (ShapeType)
            {
                case "l":
                    {
                    Point point1 = new Point(this.Width - 20, this.Height / 2);
                    Point point2 = new Point(20, this.Height / 2);
                    curvePoints =new Point[] { point1, point2 };
                    }
                    break;

                case "sh1":
                    {
                        Point point1 = new Point(this.Width - 20, this.Height / 2);
                        Point point2 = new Point(20, this.Height / 2);
                        Point p3 = new Point(20, this.Height / 4);
                        curvePoints = new Point[] { point1, point2, p3 };
                    }
                    break;


                case "sh2":
                    {
                        Point point1 = new Point(this.Width - 20, this.Height / 4);
                        Point point2 = new Point(this.Width - 20, this.Height / 2);
                        Point p3 = new Point(20, this.Height / 2);
                        curvePoints = new Point[] { point1, point2, p3 };
                    }
                    break;

                case "shBoth":
                    {
                        Point point1 = new Point(this.Width - 20, this.Height / 4);
                        Point point2 = new Point(this.Width - 20, this.Height / 2);
                        Point p3 = new Point(20, this.Height / 2);
                        Point p4 = new Point(20, this.Height / 4);
                        curvePoints = new Point[] { point1, point2, p3,p4 };
                    }
                    break;

                case "dh1":
                    {
                        Point point1 = new Point(this.Width - 20, this.Height / 2);
                        Point point2 = new Point(20, this.Height / 2);
                        Point p3 = new Point(20, this.Height / 4);
                        Point p4 = new Point(this.Width/4 + 20, this.Height / 4);
                        curvePoints = new Point[] { point1, point2, p3, p4 };
                    }
                    break;


                case "dh2":
                    {
                        Point point1 = new Point(this.Width - 20 - this.Width/4, this.Height / 4);
                        Point point2 = new Point(this.Width - 20, this.Height / 4);
                        Point p3 = new Point(this.Width - 20, this.Height / 2);
                        Point p4 = new Point(20, this.Height / 2);
                        curvePoints = new Point[] { point1, point2, p3,p4 };
                    }
                    break;

                case "dhBoth":
                    {

                        Point point1 = new Point(this.Width - 20 - this.Width / 4, this.Height / 4);
                        Point point2 = new Point(this.Width - 20, this.Height / 4);
                        Point p3 = new Point(this.Width - 20, this.Height / 2);
                        Point p4 = new Point(20, this.Height / 2);
                        Point p5 = new Point(20, this.Height / 4);
                        Point p6 = new Point(this.Width / 4 + 20, this.Height / 4);
                        curvePoints = new Point[] { point1, point2, p3, p4,p5,p6 };
                    }
                    break;
                case "sh1dh2":
                    {

                        Point p1 = new Point(this.Width - 20, this.Height / 4);
                        Point p2 = new Point(this.Width - 20, this.Height / 2);
                        Point p3 = new Point(20, this.Height / 2);
                        Point p4 = new Point(20, this.Height / 4);
                        Point p5 = new Point(this.Width / 4 + 20, this.Height / 4);
                        curvePoints = new Point[] { p1, p2, p3, p4, p5 };
                    }
                    break;
                case "dh1sh2":
                    {

                        Point point1 = new Point(this.Width - 20 - this.Width / 4, this.Height / 4);
                        Point point2 = new Point(this.Width - 20, this.Height / 4);
                        Point p3 = new Point(this.Width - 20, this.Height / 2);
                        Point p4 = new Point(20, this.Height / 2);
                        Point p5 = new Point(20, this.Height / 4);
                        curvePoints = new Point[] { point1, point2, p3, p4, p5 };
                    }
                    break;

                default:
                    // Code to be executed if none of the cases match the expression
                    break;
            }

            // Draw polygon to screen.
            e.Graphics.DrawLines(pen1, curvePoints);
        }
    }
}
