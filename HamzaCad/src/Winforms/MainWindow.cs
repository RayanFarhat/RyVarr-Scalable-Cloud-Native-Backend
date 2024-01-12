using HamzaCad.SlabDrawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using HamzaCad.Utils;

namespace HamzaCad.Winforms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {

            InitializeComponent();
            setLang();
            Eng.Checked = BarsComputer.lang == "Eng"? true:false;
            Heb.Checked = BarsComputer.lang == "Heb"? true:false;
            Eng.CheckedChanged += onCheckEng;
            Heb.CheckedChanged += onCheckHeb;

            drawVertical.Checked = BarsComputer.drawVertical;
            drawHorizontal.Checked = BarsComputer.drawHorizontal;
            drawVertical.CheckedChanged += onCheck;
            drawHorizontal.CheckedChanged += onCheck;

            ExitBtn.Click += onExitBtn;

            SpacingInput.Text = BarsComputer.BarSpacing.ToString();
            SpacingInput.ValidatingType = typeof(double);
            SpacingInput.TextChanged += onSpacing;

            withEar.Checked = BarsComputer.withEar;
            withEar.CheckedChanged += onCheck;

            EarLength.Text = BarsComputer.earLength.ToString();
            EarLength.ValidatingType = typeof(double);
            EarLength.TextChanged += onEar;

            ArrowSize.Text = BarsComputer.arrowScale.ToString();
            ArrowSize.ValidatingType = typeof(double);
            ArrowSize.TextChanged += onArrowScale;

            ArrowBlockingLineLength.Text = BarsComputer.arrowBlockingLineLength.ToString();
            ArrowBlockingLineLength.ValidatingType = typeof(double);
            ArrowBlockingLineLength.TextChanged += onArrowBlockingLineLength;

            FontSize.Text = BarsComputer.fontSize.ToString();
            FontSize.ValidatingType = typeof(double);
            FontSize.TextChanged += onFontSize;

            Diameter.Text = BarsComputer.Diameter.ToString();
            Diameter.ValidatingType = typeof(double);
            Diameter.TextChanged += onDiameter;

            TopBars.Checked = BarsComputer.iSTopBars;
            BottomBars.Checked = !BarsComputer.iSTopBars;
            TopBars.CheckedChanged += onTopBottomBars;
            BottomBars.CheckedChanged += onTopBottomBars;

            BarPolySpace.Text = BarsComputer.SideCoverY.ToString();
            BarPolySpace.ValidatingType = typeof(double);
            BarPolySpace.TextChanged += onBarPolySpace;

            TextEditorBtn.Click += onTextEditorBtn;

            AuthWinBtn.Click += onAuthWinBtn;

            MaxBarLength.Text = BarsComputer.MaxBarLength.ToString();
            MaxBarLength.ValidatingType = typeof(double);
            MaxBarLength.TextChanged += onMaxBarLength;

            MeetingCircleRadius.Text = BarsComputer.MeetingCircleRadius.ToString();
            MeetingCircleRadius.ValidatingType = typeof(double);
            MeetingCircleRadius.TextChanged += onMeetingCircleRadius;

            ComboboxItem black = new ComboboxItem();
            black.Text = "black";
            black.Value = 0;
            IronColor.Items.Add(black);
            ComboboxItem red = new ComboboxItem();
            red.Text = "red";
            red.Value = 1;
            IronColor.Items.Add(red);
            ComboboxItem yellow = new ComboboxItem();
            yellow.Text = "yellow";
            yellow.Value = 2;
            IronColor.Items.Add(yellow);
            ComboboxItem green = new ComboboxItem();
            green.Text = "green";
            green.Value = 3;
            IronColor.Items.Add(green);
            ComboboxItem cyan = new ComboboxItem();
            cyan.Text = "cyan";
            cyan.Value = 4;
            IronColor.Items.Add(cyan);
            IronColor.SelectedIndex = 4;
            ComboboxItem blue = new ComboboxItem();
            blue.Text = "blue";
            blue.Value = 5;
            IronColor.Items.Add(blue);
            ComboboxItem pink = new ComboboxItem();
            pink.Text = "pink";
            pink.Value = 6;
            IronColor.Items.Add(pink);
            ComboboxItem white = new ComboboxItem();
            white.Text = "white";
            white.Value = 7;
            IronColor.Items.Add(white);
            for (int i = 0; i < IronColor.Items.Count; i++)
            {
                if (int.Parse((IronColor.Items[i] as ComboboxItem).Value.ToString()) == BarsComputer.ironColor){
                    IronColor.SelectedIndex = i;
                }
            }
            IronColor.SelectedIndexChanged += onIronColor;

            ComboboxItem Hairline = new ComboboxItem();
            Hairline.Text = "Hairline 0.25mm";
            Hairline.Value = 25;
            IronLineWeight.Items.Add(Hairline);
            ComboboxItem Light = new ComboboxItem();
            Light.Text = "Light 0.35mm";
            Light.Value = 35;
            IronLineWeight.Items.Add(Light);
            ComboboxItem Medium = new ComboboxItem();
            Medium.Text = "Medium 0.50mm";
            Medium.Value = 50;
            IronLineWeight.Items.Add(Medium);
            ComboboxItem Bold = new ComboboxItem();
            Bold.Text = "Bold 0.70mm";
            Bold.Value = 70;
            IronLineWeight.Items.Add(Bold);
            ComboboxItem Thick = new ComboboxItem();
            Thick.Text = "Thick 1.00mm";
            Thick.Value = 100;
            IronLineWeight.Items.Add(Thick);
            for (int i = 0; i < IronLineWeight.Items.Count; i++)
            {
                if (int.Parse((IronLineWeight.Items[i] as ComboboxItem).Value.ToString())
                    == BarsComputer.IronLineWeight)
                {
                    IronLineWeight.SelectedIndex = i;
                }
            }
            IronLineWeight.SelectedIndexChanged += onIronLineWeight;
        }
        public void onCheck(object sender, EventArgs e)
        {
            BarsComputer.drawVertical = drawVertical.Checked;
            BarsComputer.drawHorizontal = drawHorizontal.Checked;
            BarsComputer.withEar = withEar.Checked;
        }
        public void onExitBtn(object sender, EventArgs e)
        {
           this.Close();
        }
        private void onSpacing(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.BarSpacing = Double.Parse(SpacingInput.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onEar(object sender, EventArgs e)
        {
            bool allowInput = withEar.Checked;
            if (!allowInput)
            {
                EarLength.Clear();
                MessageBox.Show("Input is not allowed because the checkbox is not checked.");
            }
            else if (string.IsNullOrWhiteSpace(EarLength.Text))
            {
                MessageBox.Show("Ear Length Input cannot be empty because the checkbox is checked.");
            }
            else
            {
                try
                {
                    BarsComputer.earLength = Double.Parse(EarLength.Text);
                }
                catch {
                    MessageBox.Show("Input must be a number.");
                }
            }
        }
        private void onArrowScale(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.arrowScale = Double.Parse(ArrowSize.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onArrowBlockingLineLength(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.arrowBlockingLineLength = Double.Parse(ArrowBlockingLineLength.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onFontSize(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.fontSize = Double.Parse(FontSize.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onDiameter(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.Diameter = Double.Parse(Diameter.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onTopBottomBars(object sender, EventArgs e)
        {
            BarsComputer.iSTopBars = TopBars.Checked;
        }
        private void onBarPolySpace(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.SideCoverY = Double.Parse(BarPolySpace.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        
        public void onTextEditorBtn(object sender, EventArgs e)
        {
            var m = new TextEditorWindow();
            m.Show();// will continue the proccess of other window
        }
        public void onAuthWinBtn(object sender, EventArgs e)
        {
            var m = new AuthWindow();
            m.Show();// will continue the proccess of other window
        }

        private void onMaxBarLength(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.MaxBarLength = Double.Parse(MaxBarLength.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onMeetingCircleRadius(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.MeetingCircleRadius = Double.Parse(MeetingCircleRadius.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onIronColor(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.ironColor = int.Parse((IronColor.SelectedItem as ComboboxItem).Value.ToString());
            }
            catch
            {
                MessageBox.Show("Error happen when applying the color");
            }
        }
        private void onIronLineWeight(object sender, EventArgs e)
        {
            try
            {
                BarsComputer.IronLineWeight = int.Parse((IronLineWeight.SelectedItem as ComboboxItem).Value.ToString());
            }
            catch
            {
                MessageBox.Show("Error happen when applying the line weight");
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            FileHandler.writeFile();
        }
    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text.ToString();
        }
    }
}
