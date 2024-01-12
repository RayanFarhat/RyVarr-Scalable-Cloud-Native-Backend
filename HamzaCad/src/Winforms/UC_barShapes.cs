using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HamzaCad.src.AutoCADAdapter;

namespace HamzaCad.src.Winforms
{
    public partial class UC_barShapes : UserControl
    {
        public UC_barShapes()
        {
            InitializeComponent();
            BarShapesPanelContainer.AutoScroll = true;
            BarShapeRadioPanel s1 = new BarShapeRadioPanel();
            BarShapeRadioPanel s2 = new BarShapeRadioPanel();
            BarShapeRadioPanel s3 = new BarShapeRadioPanel();
            BarShapeRadioPanel s4 = new BarShapeRadioPanel();
            BarShapeRadioPanel s5 = new BarShapeRadioPanel();
            s1.CheckedChanged += RadioPanel_CheckedChanged;
            s2.CheckedChanged += RadioPanel_CheckedChanged;
            s3.CheckedChanged += RadioPanel_CheckedChanged;
            s4.CheckedChanged += RadioPanel_CheckedChanged;
            s5.CheckedChanged += RadioPanel_CheckedChanged;
            BarShapesPanelContainer.Controls.Add(s1);
            BarShapesPanelContainer.Controls.Add(s2);
            BarShapesPanelContainer.Controls.Add(s3);
            BarShapesPanelContainer.Controls.Add(s4);
            BarShapesPanelContainer.Controls.Add(s5);
        }
        private void RadioPanel_CheckedChanged(object sender, EventArgs e)
        {
            // Uncheck other panels when one is checked
            foreach (Control control in BarShapesPanelContainer.Controls)
            {
                if (control is BarShapeRadioPanel radioPanel && radioPanel != sender)
                {
                AutoCADAdapter.AutoCADAdapter.ed.WriteMessage(radioPanel.IsChecked+"\n");
                    radioPanel.IsChecked = false;
                }
            }
        }
    }
}
