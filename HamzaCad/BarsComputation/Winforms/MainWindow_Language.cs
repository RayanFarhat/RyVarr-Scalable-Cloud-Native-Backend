using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HamzaCad.BarsComputation
{
    public partial class MainWindow : Form
    {
        public void setLang()
        {
            if(BarsComputer.lang == "Eng")
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
            spacingLabel.Text = "Spacing";
            drawVertical.Text = "draw vertical";
            drawHorizontal.Text = "draw horizontal";
            FontSizeLabel.Text = "Font Size";
            withEar.Text = "With Ear";
            earLabel.Text = "Ear Length";
            arrowSizelabel.Text = "Arrow Size";
            ArrowBlockingLineLengthLabel.Text = "Arrow Blocking Line Length";
            ExitBtn.Text = "Draw";
            TopBars.Text = "T.B.";
            BottomBars.Text = "B.B.";
            DiameterLabel.Text = "Diameter";
            BarPolySpaceLabel.Text = "Space between bar and polyline";
            TextEditorBtn.Text = "Text Editor";
            MaxBarLengthLabel.Text = "Max Bar Length";
            MeetingCircleRadiusLabel.Text = "Meeting Circle Radius";
            IronColorLabel.Text = "Iron Color";
            IronLineWeightLabel.Text = "Iron LineWeight";
        }
        private void setHeb()
        {
            spacingLabel.Text = "מרווחים";
            drawVertical.Text = "צייר אנכי";
            drawHorizontal.Text = "צייר אופקי";
            FontSizeLabel.Text = "גודל גופן";
            withEar.Text = "עם אוזן";
            earLabel.Text = "אורך האוזן";
            arrowSizelabel.Text = "גודל החץ";
            ArrowBlockingLineLengthLabel.Text = "אורך שורה חוסם חץ";
            ExitBtn.Text = "צייר";
            TopBars.Text = ".ב.ע";
            BottomBars.Text = ".ב.ת";
            DiameterLabel.Text = "קוטר";
            BarPolySpaceLabel.Text = "רווח בין ברזל לפוליליין";
            TextEditorBtn.Text = "עורך טקסט";
            MaxBarLengthLabel.Text = "אורך ברזל המקסימלי";
            MeetingCircleRadiusLabel.Text = "רַדִיוּס מעגל ההתנגשות";
            IronColorLabel.Text = "צבע הברזל";
            IronLineWeightLabel.Text = "משקל קו הברזל";
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
