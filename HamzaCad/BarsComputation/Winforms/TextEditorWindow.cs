using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HamzaCad.BarsComputation
{
    public partial class TextEditorWindow : Form
    {
        public TextEditorWindow()
        {
            InitializeComponent();
            setLang();

            ExitBtn.Click += onExitBtn;

            UpperText.Text = BarsComputer.upperText;
            UpperText.ValidatingType = typeof(string);
            UpperText.TextChanged += onUpperText;
            LowerText.Text = BarsComputer.lowerText;
            LowerText.ValidatingType = typeof(string);
            LowerText.TextChanged += onLowerText;

            TopBarSymbol.Text = BarsComputer.topBarSymbol;
            TopBarSymbol.ValidatingType = typeof(string);
            TopBarSymbol.TextChanged += onTopBarSymbol;
            LowerBarSymbol.Text = BarsComputer.lowerBarSymbol; 
            LowerBarSymbol.ValidatingType = typeof(string);
            LowerBarSymbol.TextChanged += onLowerBarSymbol;
        }

        public void onExitBtn(object sender, EventArgs e)
        {
            this.Close();
        }
        public void onUpperText(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.upperText = UpperText.Text;
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
                BarsComputer.lowerText = LowerText.Text;
            }
            catch
            {
                MessageBox.Show("Input is invalid.");
            }
        }
        public void onTopBarSymbol(object sender, EventArgs e)
        {
            BarsComputer.topBarSymbol = TopBarSymbol.Text;
        }
        public void onLowerBarSymbol(object sender, EventArgs e)
        {
            BarsComputer.lowerBarSymbol = LowerBarSymbol.Text;
        }
    }
}
