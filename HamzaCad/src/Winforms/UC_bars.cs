using System;
using System.Windows.Forms;
using HamzaCad.DrawingParameters;


namespace HamzaCad.src.Winforms
{
    public partial class UC_bars : UserControl
    {
        public UC_bars()
        {
            InitializeComponent();

            drawVertical.Checked = BarsParam.DrawVertical;
            drawHorizontal.Checked = BarsParam.DrawHorizontal;

            MaxLen.Text = BarsParam.MaxBarLength.ToString();
            MaxLen.ValidatingType = typeof(double);
            MaxLen.TextChanged += onMaxBarLength;

            Diameter.Text = BarsParam.Diameter.ToString();
            Diameter.ValidatingType = typeof(double);
            Diameter.TextChanged += onDiameter;

            Spacing.Text = BarsParam.BarSpacing.ToString();
            Spacing.ValidatingType = typeof(double);
            Spacing.TextChanged += onSpacing;

            SideCoverX.Text = BarsParam.SideCoverX.ToString();
            SideCoverX.ValidatingType = typeof(double);
            SideCoverX.TextChanged += onSideCoverX;

            SideCoverY.Text = BarsParam.SideCoverY.ToString();
            SideCoverY.ValidatingType = typeof(double);
            SideCoverY.TextChanged += onSideCoverY;

            RoundLen.Text = BarsParam.RoundLen.ToString();
            RoundLen.ValidatingType = typeof(int);
            RoundLen.TextChanged += onRoundLen;


            TopBars.Checked = BarsParam.iSTopBars;
            BottomBars.Checked = !BarsParam.iSTopBars;
            TopBars.CheckedChanged += onTopBottomBars;
            BottomBars.CheckedChanged += onTopBottomBars;

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
                if (int.Parse((IronColor.Items[i] as ComboboxItem).Value.ToString()) == BarsParam.IronColor)
                {
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
                    == BarsParam.IronLineWeight)
                {
                    IronLineWeight.SelectedIndex = i;
                }
            }
            IronLineWeight.SelectedIndexChanged += onIronLineWeight;
        }

        private void onMaxBarLength(object sender, EventArgs e)
        {
            try
            {
                BarsParam.MaxBarLength = Double.Parse(MaxLen.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }
        private void onTopBottomBars(object sender, EventArgs e)
        {
            BarsParam.iSTopBars = TopBars.Checked;
        }

        private void onDiameter(object sender, EventArgs e)
        {
            try
            {
                BarsParam.Diameter = Double.Parse(Diameter.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }

        private void onSpacing(object sender, EventArgs e)
        {
            try
            {
                BarsParam.BarSpacing = Double.Parse(Spacing.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }

        private void onSideCoverX(object sender, EventArgs e)
        {
            try
            {
                BarsParam.SideCoverX = Double.Parse(SideCoverX.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }

        private void onSideCoverY(object sender, EventArgs e)
        {
            try
            {
                BarsParam.SideCoverY = Double.Parse(SideCoverY.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
            }
        }

        private void drawVertical_CheckedChanged(object sender, EventArgs e)
        {
            BarsParam.DrawVertical = drawVertical.Checked;
        }

        private void drawHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            BarsParam.DrawHorizontal = drawHorizontal.Checked;
        }
        private void onIronColor(object sender, EventArgs e)
        {
            try
            {
                BarsParam.IronColor = int.Parse((IronColor.SelectedItem as ComboboxItem).Value.ToString());
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
                BarsParam.IronLineWeight = int.Parse((IronLineWeight.SelectedItem as ComboboxItem).Value.ToString());
            }
            catch
            {
                MessageBox.Show("Error happen when applying the line weight");
            }
        }
        private void onRoundLen(object sender, EventArgs e)
        {
            try
            {
                BarsParam.RoundLen = int.Parse(RoundLen.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number.");
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
}
