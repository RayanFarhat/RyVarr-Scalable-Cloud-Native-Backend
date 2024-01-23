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
    public partial class ModifyBarsWindow : Form
    {
        public ModifyBarsWindow()
        {
            InitializeComponent();
            UC_barShapes uc = new UC_barShapes();
            addUserControl(uc);
        }
        private void addUserControl(UserControl UC)
        {
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(UC);
            UC.BringToFront();
        }
        private void barShapesNav_CheckedChanged(object sender, EventArgs e)
        {
            UC_barShapes uc = new UC_barShapes();
            addUserControl(uc);
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DrawBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
