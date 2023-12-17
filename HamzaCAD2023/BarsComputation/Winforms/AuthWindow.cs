using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HamzaCad.BarsComputation.Winforms
{
    public partial class AuthWindow : Form
    {
        public AuthWindow()
        {
            InitializeComponent();
            AuthWinCloseBtn.Click += onExitBtn;
            AuthTokenClearBtn.Click += onClearBtn;

            AuthToken.Text = HamzaCad.Utils.HttpClientHandler.AuthToken;
            AuthToken.ValidatingType = typeof(string);
            AuthToken.TextChanged += onAuthToken;
        }
        public void onExitBtn(object sender, EventArgs e)
        {
            this.Close();
        }
        public void onAuthToken(object sender, EventArgs e)
        {
            try
            {
                HamzaCad.Utils.HttpClientHandler.AuthToken = AuthToken.Text;
            }
            catch
            {
                MessageBox.Show("Input is invalid.");
            }
        }
        public void onClearBtn(object sender, EventArgs e)
        {
            AuthToken.Text = "";
        }
    }
}
