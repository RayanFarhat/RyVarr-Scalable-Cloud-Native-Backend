namespace HamzaCad.src.Winforms
{
    partial class UC_Drawing
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
            this.TextSize = new System.Windows.Forms.MaskedTextBox();
            this.TextSizeLabel = new System.Windows.Forms.Label();
            this.ArrowSize = new System.Windows.Forms.MaskedTextBox();
            this.ArrowSizeLabel = new System.Windows.Forms.Label();
            this.IntersectCircleRadius = new System.Windows.Forms.MaskedTextBox();
            this.IntersectCircleRadiusLabel = new System.Windows.Forms.Label();
            this.BlockingLineEnabled = new System.Windows.Forms.CheckBox();
            this.ArrowBlockingLineLength = new System.Windows.Forms.MaskedTextBox();
            this.ArrowBlockingLineLengthLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextSize
            // 
            this.TextSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextSize.Location = new System.Drawing.Point(178, 31);
            this.TextSize.Name = "TextSize";
            this.TextSize.Size = new System.Drawing.Size(100, 22);
            this.TextSize.TabIndex = 29;
            // 
            // TextSizeLabel
            // 
            this.TextSizeLabel.AutoSize = true;
            this.TextSizeLabel.Location = new System.Drawing.Point(78, 33);
            this.TextSizeLabel.Name = "TextSizeLabel";
            this.TextSizeLabel.Size = new System.Drawing.Size(69, 16);
            this.TextSizeLabel.TabIndex = 28;
            this.TextSizeLabel.Text = "Text Size=";
            // 
            // ArrowSize
            // 
            this.ArrowSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ArrowSize.Location = new System.Drawing.Point(178, 178);
            this.ArrowSize.Name = "ArrowSize";
            this.ArrowSize.Size = new System.Drawing.Size(100, 22);
            this.ArrowSize.TabIndex = 31;
            // 
            // ArrowSizeLabel
            // 
            this.ArrowSizeLabel.AutoSize = true;
            this.ArrowSizeLabel.Location = new System.Drawing.Point(78, 184);
            this.ArrowSizeLabel.Name = "ArrowSizeLabel";
            this.ArrowSizeLabel.Size = new System.Drawing.Size(77, 16);
            this.ArrowSizeLabel.TabIndex = 30;
            this.ArrowSizeLabel.Text = "Arrow Size=";
            // 
            // IntersectCircleRadius
            // 
            this.IntersectCircleRadius.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IntersectCircleRadius.Location = new System.Drawing.Point(178, 104);
            this.IntersectCircleRadius.Name = "IntersectCircleRadius";
            this.IntersectCircleRadius.Size = new System.Drawing.Size(100, 22);
            this.IntersectCircleRadius.TabIndex = 33;
            // 
            // IntersectCircleRadiusLabel
            // 
            this.IntersectCircleRadiusLabel.AutoSize = true;
            this.IntersectCircleRadiusLabel.Location = new System.Drawing.Point(25, 106);
            this.IntersectCircleRadiusLabel.Name = "IntersectCircleRadiusLabel";
            this.IntersectCircleRadiusLabel.Size = new System.Drawing.Size(147, 16);
            this.IntersectCircleRadiusLabel.TabIndex = 32;
            this.IntersectCircleRadiusLabel.Text = "Intersect Circle Radius=";
            // 
            // BlockingLineEnabled
            // 
            this.BlockingLineEnabled.AutoSize = true;
            this.BlockingLineEnabled.Checked = true;
            this.BlockingLineEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BlockingLineEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BlockingLineEnabled.Location = new System.Drawing.Point(42, 246);
            this.BlockingLineEnabled.Name = "BlockingLineEnabled";
            this.BlockingLineEnabled.Size = new System.Drawing.Size(236, 20);
            this.BlockingLineEnabled.TabIndex = 34;
            this.BlockingLineEnabled.Text = "Blocking Line in the end of the arrow";
            this.BlockingLineEnabled.UseVisualStyleBackColor = true;
            // 
            // ArrowBlockingLineLength
            // 
            this.ArrowBlockingLineLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ArrowBlockingLineLength.Location = new System.Drawing.Point(205, 319);
            this.ArrowBlockingLineLength.Name = "ArrowBlockingLineLength";
            this.ArrowBlockingLineLength.Size = new System.Drawing.Size(100, 22);
            this.ArrowBlockingLineLength.TabIndex = 36;
            // 
            // ArrowBlockingLineLengthLabel
            // 
            this.ArrowBlockingLineLengthLabel.AutoSize = true;
            this.ArrowBlockingLineLengthLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ArrowBlockingLineLengthLabel.Location = new System.Drawing.Point(25, 321);
            this.ArrowBlockingLineLengthLabel.Name = "ArrowBlockingLineLengthLabel";
            this.ArrowBlockingLineLengthLabel.Size = new System.Drawing.Size(174, 16);
            this.ArrowBlockingLineLengthLabel.TabIndex = 35;
            this.ArrowBlockingLineLengthLabel.Text = "Arrow Blocking Line Length=";
            // 
            // UC_Drawing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ArrowBlockingLineLength);
            this.Controls.Add(this.ArrowBlockingLineLengthLabel);
            this.Controls.Add(this.BlockingLineEnabled);
            this.Controls.Add(this.IntersectCircleRadius);
            this.Controls.Add(this.IntersectCircleRadiusLabel);
            this.Controls.Add(this.ArrowSize);
            this.Controls.Add(this.ArrowSizeLabel);
            this.Controls.Add(this.TextSize);
            this.Controls.Add(this.TextSizeLabel);
            this.Name = "UC_Drawing";
            this.Size = new System.Drawing.Size(1200, 445);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox TextSize;
        private System.Windows.Forms.Label TextSizeLabel;
        private System.Windows.Forms.MaskedTextBox ArrowSize;
        private System.Windows.Forms.Label ArrowSizeLabel;
        private System.Windows.Forms.MaskedTextBox IntersectCircleRadius;
        private System.Windows.Forms.Label IntersectCircleRadiusLabel;
        private System.Windows.Forms.CheckBox BlockingLineEnabled;
        private System.Windows.Forms.MaskedTextBox ArrowBlockingLineLength;
        private System.Windows.Forms.Label ArrowBlockingLineLengthLabel;
    }
}
