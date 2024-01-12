namespace HamzaCad.src.Winforms
{
    partial class UC_bars
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MaxLenLabel = new System.Windows.Forms.Label();
            this.MaxLen = new System.Windows.Forms.MaskedTextBox();
            this.Spacing = new System.Windows.Forms.MaskedTextBox();
            this.SpacingLabel = new System.Windows.Forms.Label();
            this.Diameter = new System.Windows.Forms.MaskedTextBox();
            this.DiameterLabel = new System.Windows.Forms.Label();
            this.drawHorizontal = new System.Windows.Forms.CheckBox();
            this.drawVertical = new System.Windows.Forms.CheckBox();
            this.SideCoverPanel = new System.Windows.Forms.Panel();
            this.SideCoverX = new System.Windows.Forms.MaskedTextBox();
            this.SideCoverXLabel = new System.Windows.Forms.Label();
            this.SideCoverY = new System.Windows.Forms.MaskedTextBox();
            this.SideCoverYLabel = new System.Windows.Forms.Label();
            this.SideCoverLabel = new System.Windows.Forms.Label();
            this.IronLineWeightLabel = new System.Windows.Forms.Label();
            this.IronLineWeight = new System.Windows.Forms.ComboBox();
            this.IronColorLabel = new System.Windows.Forms.Label();
            this.IronColor = new System.Windows.Forms.ComboBox();
            this.SideCoverPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MaxLenLabel
            // 
            this.MaxLenLabel.AutoSize = true;
            this.MaxLenLabel.Location = new System.Drawing.Point(41, 41);
            this.MaxLenLabel.Name = "MaxLenLabel";
            this.MaxLenLabel.Size = new System.Drawing.Size(111, 16);
            this.MaxLenLabel.TabIndex = 0;
            this.MaxLenLabel.Text = "MaximumLength=";
            // 
            // MaxLen
            // 
            this.MaxLen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaxLen.Location = new System.Drawing.Point(158, 39);
            this.MaxLen.Name = "MaxLen";
            this.MaxLen.Size = new System.Drawing.Size(100, 22);
            this.MaxLen.TabIndex = 27;
            // 
            // Spacing
            // 
            this.Spacing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Spacing.Location = new System.Drawing.Point(158, 97);
            this.Spacing.Name = "Spacing";
            this.Spacing.Size = new System.Drawing.Size(100, 22);
            this.Spacing.TabIndex = 29;
            // 
            // SpacingLabel
            // 
            this.SpacingLabel.AutoSize = true;
            this.SpacingLabel.Location = new System.Drawing.Point(76, 99);
            this.SpacingLabel.Name = "SpacingLabel";
            this.SpacingLabel.Size = new System.Drawing.Size(64, 16);
            this.SpacingLabel.TabIndex = 28;
            this.SpacingLabel.Text = "Spacing=";
            // 
            // Diameter
            // 
            this.Diameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Diameter.Location = new System.Drawing.Point(158, 156);
            this.Diameter.Name = "Diameter";
            this.Diameter.Size = new System.Drawing.Size(100, 22);
            this.Diameter.TabIndex = 31;
            // 
            // DiameterLabel
            // 
            this.DiameterLabel.AutoSize = true;
            this.DiameterLabel.Location = new System.Drawing.Point(76, 158);
            this.DiameterLabel.Name = "DiameterLabel";
            this.DiameterLabel.Size = new System.Drawing.Size(69, 16);
            this.DiameterLabel.TabIndex = 30;
            this.DiameterLabel.Text = "Diameter=";
            // 
            // drawHorizontal
            // 
            this.drawHorizontal.AutoSize = true;
            this.drawHorizontal.BackColor = System.Drawing.Color.White;
            this.drawHorizontal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawHorizontal.Location = new System.Drawing.Point(323, 80);
            this.drawHorizontal.Name = "drawHorizontal";
            this.drawHorizontal.Size = new System.Drawing.Size(114, 20);
            this.drawHorizontal.TabIndex = 33;
            this.drawHorizontal.Text = "draw horizontal";
            this.drawHorizontal.UseVisualStyleBackColor = false;
            this.drawHorizontal.CheckedChanged += new System.EventHandler(this.drawHorizontal_CheckedChanged);
            // 
            // drawVertical
            // 
            this.drawVertical.AutoSize = true;
            this.drawVertical.BackColor = System.Drawing.Color.White;
            this.drawVertical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawVertical.Location = new System.Drawing.Point(323, 41);
            this.drawVertical.Name = "drawVertical";
            this.drawVertical.Size = new System.Drawing.Size(100, 20);
            this.drawVertical.TabIndex = 32;
            this.drawVertical.Text = "draw vertical";
            this.drawVertical.UseVisualStyleBackColor = false;
            this.drawVertical.CheckedChanged += new System.EventHandler(this.drawVertical_CheckedChanged);
            // 
            // SideCoverPanel
            // 
            this.SideCoverPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SideCoverPanel.Controls.Add(this.SideCoverX);
            this.SideCoverPanel.Controls.Add(this.SideCoverXLabel);
            this.SideCoverPanel.Controls.Add(this.SideCoverY);
            this.SideCoverPanel.Controls.Add(this.SideCoverYLabel);
            this.SideCoverPanel.Location = new System.Drawing.Point(318, 130);
            this.SideCoverPanel.Name = "SideCoverPanel";
            this.SideCoverPanel.Size = new System.Drawing.Size(200, 48);
            this.SideCoverPanel.TabIndex = 34;
            // 
            // SideCoverX
            // 
            this.SideCoverX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SideCoverX.Location = new System.Drawing.Point(35, 16);
            this.SideCoverX.Name = "SideCoverX";
            this.SideCoverX.Size = new System.Drawing.Size(54, 22);
            this.SideCoverX.TabIndex = 41;
            // 
            // SideCoverXLabel
            // 
            this.SideCoverXLabel.AutoSize = true;
            this.SideCoverXLabel.Location = new System.Drawing.Point(7, 18);
            this.SideCoverXLabel.Name = "SideCoverXLabel";
            this.SideCoverXLabel.Size = new System.Drawing.Size(22, 16);
            this.SideCoverXLabel.TabIndex = 40;
            this.SideCoverXLabel.Text = "X=";
            // 
            // SideCoverY
            // 
            this.SideCoverY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SideCoverY.Location = new System.Drawing.Point(135, 16);
            this.SideCoverY.Name = "SideCoverY";
            this.SideCoverY.Size = new System.Drawing.Size(54, 22);
            this.SideCoverY.TabIndex = 39;
            // 
            // SideCoverYLabel
            // 
            this.SideCoverYLabel.AutoSize = true;
            this.SideCoverYLabel.Location = new System.Drawing.Point(106, 18);
            this.SideCoverYLabel.Name = "SideCoverYLabel";
            this.SideCoverYLabel.Size = new System.Drawing.Size(23, 16);
            this.SideCoverYLabel.TabIndex = 38;
            this.SideCoverYLabel.Text = "Y=";
            // 
            // SideCoverLabel
            // 
            this.SideCoverLabel.AutoSize = true;
            this.SideCoverLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SideCoverLabel.Location = new System.Drawing.Point(320, 121);
            this.SideCoverLabel.Name = "SideCoverLabel";
            this.SideCoverLabel.Size = new System.Drawing.Size(74, 16);
            this.SideCoverLabel.TabIndex = 35;
            this.SideCoverLabel.Text = "Side Cover";
            // 
            // IronLineWeightLabel
            // 
            this.IronLineWeightLabel.AutoSize = true;
            this.IronLineWeightLabel.Location = new System.Drawing.Point(1059, 28);
            this.IronLineWeightLabel.Name = "IronLineWeightLabel";
            this.IronLineWeightLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.IronLineWeightLabel.Size = new System.Drawing.Size(99, 16);
            this.IronLineWeightLabel.TabIndex = 39;
            this.IronLineWeightLabel.Text = "Iron LineWeight";
            // 
            // IronLineWeight
            // 
            this.IronLineWeight.FormattingEnabled = true;
            this.IronLineWeight.Location = new System.Drawing.Point(1057, 52);
            this.IronLineWeight.Name = "IronLineWeight";
            this.IronLineWeight.Size = new System.Drawing.Size(121, 24);
            this.IronLineWeight.TabIndex = 38;
            // 
            // IronColorLabel
            // 
            this.IronColorLabel.AutoSize = true;
            this.IronColorLabel.Location = new System.Drawing.Point(904, 28);
            this.IronColorLabel.Name = "IronColorLabel";
            this.IronColorLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.IronColorLabel.Size = new System.Drawing.Size(64, 16);
            this.IronColorLabel.TabIndex = 37;
            this.IronColorLabel.Text = "Iron Color";
            // 
            // IronColor
            // 
            this.IronColor.FormattingEnabled = true;
            this.IronColor.Location = new System.Drawing.Point(904, 50);
            this.IronColor.Name = "IronColor";
            this.IronColor.Size = new System.Drawing.Size(121, 24);
            this.IronColor.TabIndex = 36;
            // 
            // UC_bars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.IronLineWeightLabel);
            this.Controls.Add(this.IronLineWeight);
            this.Controls.Add(this.IronColorLabel);
            this.Controls.Add(this.IronColor);
            this.Controls.Add(this.SideCoverLabel);
            this.Controls.Add(this.SideCoverPanel);
            this.Controls.Add(this.drawHorizontal);
            this.Controls.Add(this.drawVertical);
            this.Controls.Add(this.Diameter);
            this.Controls.Add(this.DiameterLabel);
            this.Controls.Add(this.Spacing);
            this.Controls.Add(this.SpacingLabel);
            this.Controls.Add(this.MaxLen);
            this.Controls.Add(this.MaxLenLabel);
            this.Name = "UC_bars";
            this.Size = new System.Drawing.Size(1200, 445);
            this.SideCoverPanel.ResumeLayout(false);
            this.SideCoverPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MaxLenLabel;
        private System.Windows.Forms.MaskedTextBox MaxLen;
        private System.Windows.Forms.MaskedTextBox Spacing;
        private System.Windows.Forms.Label SpacingLabel;
        private System.Windows.Forms.MaskedTextBox Diameter;
        private System.Windows.Forms.Label DiameterLabel;
        private System.Windows.Forms.CheckBox drawHorizontal;
        private System.Windows.Forms.CheckBox drawVertical;
        private System.Windows.Forms.Panel SideCoverPanel;
        private System.Windows.Forms.Label SideCoverLabel;
        private System.Windows.Forms.MaskedTextBox SideCoverX;
        private System.Windows.Forms.Label SideCoverXLabel;
        private System.Windows.Forms.MaskedTextBox SideCoverY;
        private System.Windows.Forms.Label SideCoverYLabel;
        private System.Windows.Forms.Label IronLineWeightLabel;
        private System.Windows.Forms.ComboBox IronLineWeight;
        private System.Windows.Forms.Label IronColorLabel;
        private System.Windows.Forms.ComboBox IronColor;
    }
}
