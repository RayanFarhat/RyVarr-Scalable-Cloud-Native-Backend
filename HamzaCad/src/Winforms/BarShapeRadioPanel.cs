using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace HamzaCad.src.Winforms
{
    public class BarShapeRadioPanel: Panel
    {
        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value) {
                    isChecked = value;
                    OnCheckedChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler CheckedChanged;

        public BarShapeRadioPanel()
        {
            this.Paint += panel1_Paint;
        }

        public virtual void OnCheckedChanged(EventArgs e)
        {
            CheckedChanged?.Invoke(this, e);
            UpdateRadioPanelAppearance();
        }

        private void UpdateRadioPanelAppearance()
        {
            // Update the appearance based on the checked state
            BackColor = isChecked ? SystemColors.Highlight : SystemColors.Control;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            IsChecked = true;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.Black, 2);
            Point point1 = new Point(50, 50);
            Point point2 = new Point(300, 50);
            Point point3 = new Point(300, 20);
            Point point4 = new Point(250, 50);
            Point point5 = new Point(300, 100);
            Point point6 = new Point(350, 200);
            Point[] curvePoints =
                     {
                 point1,
                 point2,
                 point3,

             };

            // Draw polygon to screen.
            e.Graphics.DrawLines(pen1, curvePoints);
        }
    }
}
