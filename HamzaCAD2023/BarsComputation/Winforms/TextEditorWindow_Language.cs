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
        public void setLang()
        {
            if (BarsComputer.lang == "Eng")
            {
                setEng();
            }
            else
            {
                setHeb();
            }
        }
        private void setEng()
        {
            ExitBtn.Text = "Exit";
            UpperTextLabel.Text = "Upper Text";
            LowerTextLabel.Text = "Lower Text";
            LLabel.Text = "{L} = length";
            QLabel.Text = "{Q} = quantity";
            SLabel.Text = "{S} = spacing";
            BRLabel.Text = "{TB} = Top/Bottom Bar Symbol";
            DLabel.Text = "{D} = diameter";
            TopBarSymbolLabel.Text = "Top Bar Symbol";
            LowerBarSymbolLabel.Text = "lower Bar Symbol";
        }
        private void setHeb()
        {
            ExitBtn.Text = "יציאה";
            UpperTextLabel.Text = "טקסט עליון";
            LowerTextLabel.Text = "טקסט תחתון";
            LLabel.Text = "{L} = אורך";
            QLabel.Text = "{Q} = כמות";
            SLabel.Text = "{S} = מרווח";
            BRLabel.Text = "{BR} = סוג ברזל";
            DLabel.Text = "{D} = קוטר";
            TopBarSymbolLabel.Text = "סמל ברזל עליון";
            LowerBarSymbolLabel.Text = "סמל ברזל תחתון";
        }
        public void onCheckEng(object sender, EventArgs e)
        {
            BarsComputer.lang = "Eng";
            setEng();
        }
        public void onCheckHeb(object sender, EventArgs e)
        {
            BarsComputer.lang = "Heb";
            setHeb();
        }
    }
}