using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HamzaCad.BarsComputation
{
    public partial class MainWindow : Form
    {
        public void onCheckEng(object sender, EventArgs e)
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
        }
        public void onCheckHeb(object sender, EventArgs e)
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
        }
    }
}
