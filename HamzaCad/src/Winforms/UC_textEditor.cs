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

namespace HamzaCad.src.Winforms
{
    public partial class UC_textEditor : UserControl
    {
        public UC_textEditor()
        {
            InitializeComponent();
            TopText.Text = TextEdirotParam.TopText;
            TopText.ValidatingType = typeof(string);
            TopText.TextChanged += onUpperText;
            BottomText.Text = TextEdirotParam.BottomText;
            BottomText.ValidatingType = typeof(string);
            BottomText.TextChanged += onLowerText;

            TopBarSymbol.Text = TextEdirotParam.TopBarSymbol;
            TopBarSymbol.ValidatingType = typeof(string);
            TopBarSymbol.TextChanged += onTopBarSymbol;
            BottomBarSymbol.Text = TextEdirotParam.BottomBarSymbol;
            BottomBarSymbol.ValidatingType = typeof(string);
            BottomBarSymbol.TextChanged += onLowerBarSymbol;
        }
        public void onUpperText(object sender, EventArgs e)
        {
            try
            {
                TextEdirotParam.TopText = TopText.Text;
            }
            catch
            {
                MessageBox.Show("Input is invalid.");
            }
        }
        public void onLowerText(object sender, EventArgs e)
        {
            try
            {
                TextEdirotParam.BottomText = BottomText.Text;
            }
            catch
            {
                MessageBox.Show("Input is invalid.");
            }
        }
        public void onTopBarSymbol(object sender, EventArgs e)
        {
            TextEdirotParam.TopBarSymbol = TopBarSymbol.Text;
        }
        public void onLowerBarSymbol(object sender, EventArgs e)
        {
            TextEdirotParam.BottomBarSymbol = BottomBarSymbol.Text;
        }
    }
}
