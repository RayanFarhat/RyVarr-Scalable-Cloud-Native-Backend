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
    public partial class SlabDrawWindow : Form
    {
        public SlabDrawWindow()
        {
            InitializeComponent();
            UC_bars uc = new UC_bars();
            addUserControl(uc);
        }

        private void addUserControl(UserControl UC)
        {
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(UC);
            UC.BringToFront();
        }

        private void BarsNav_CheckedChanged(object sender, EventArgs e)
        {
            UC_bars uc = new UC_bars();
            addUserControl(uc);
        }

        private void TextEditorNav_CheckedChanged(object sender, EventArgs e)
        {
            UC_textEditor uc = new UC_textEditor();
            addUserControl(uc);
        }

        private void DrawingNav_CheckedChanged(object sender, EventArgs e)
        {
            UC_Drawing uc = new UC_Drawing();
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

        private void barShapesNav_CheckedChanged(object sender, EventArgs e)
        {
            UC_barShapes uc = new UC_barShapes();
            addUserControl(uc);
        }

        private void carefulNotesNav_CheckedChanged(object sender, EventArgs e)
        {
            UC_carefulNotes uc = new UC_carefulNotes();
            addUserControl(uc);
        }
    }
}
