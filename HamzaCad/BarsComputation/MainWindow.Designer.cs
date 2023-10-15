namespace HamzaCad.BarsComputation
{
    partial class MainWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.drawVertical = new System.Windows.Forms.CheckBox();
            this.drawHorizontal = new System.Windows.Forms.CheckBox();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.spacingLabel = new System.Windows.Forms.Label();
            this.SpacingInput = new System.Windows.Forms.MaskedTextBox();
            this.withEar = new System.Windows.Forms.CheckBox();
            this.earLabel = new System.Windows.Forms.Label();
            this.EarLength = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // drawVertical
            // 
            this.drawVertical.AutoSize = true;
            this.drawVertical.Location = new System.Drawing.Point(259, 25);
            this.drawVertical.Name = "drawVertical";
            this.drawVertical.Size = new System.Drawing.Size(104, 20);
            this.drawVertical.TabIndex = 0;
            this.drawVertical.Text = "draw vertical";
            this.drawVertical.UseVisualStyleBackColor = true;
            // 
            // drawHorizontal
            // 
            this.drawHorizontal.AutoSize = true;
            this.drawHorizontal.Location = new System.Drawing.Point(259, 64);
            this.drawHorizontal.Name = "drawHorizontal";
            this.drawHorizontal.Size = new System.Drawing.Size(118, 20);
            this.drawHorizontal.TabIndex = 1;
            this.drawHorizontal.Text = "draw horizontal";
            this.drawHorizontal.UseVisualStyleBackColor = true;
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(140, 398);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(75, 23);
            this.ExitBtn.TabIndex = 2;
            this.ExitBtn.Text = "Draw";
            this.ExitBtn.UseVisualStyleBackColor = true;
            // 
            // spacingLabel
            // 
            this.spacingLabel.Location = new System.Drawing.Point(49, 20);
            this.spacingLabel.Name = "spacingLabel";
            this.spacingLabel.Size = new System.Drawing.Size(86, 27);
            this.spacingLabel.TabIndex = 0;
            this.spacingLabel.Text = "Spacing";
            // 
            // SpacingInput
            // 
            this.SpacingInput.Location = new System.Drawing.Point(35, 41);
            this.SpacingInput.Name = "SpacingInput";
            this.SpacingInput.Size = new System.Drawing.Size(100, 22);
            this.SpacingInput.TabIndex = 4;
            // 
            // withEar
            // 
            this.withEar.AutoSize = true;
            this.withEar.Location = new System.Drawing.Point(47, 128);
            this.withEar.Name = "withEar";
            this.withEar.Size = new System.Drawing.Size(79, 20);
            this.withEar.TabIndex = 5;
            this.withEar.Text = "With Ear";
            this.withEar.UseVisualStyleBackColor = true;
            // 
            // earLabel
            // 
            this.earLabel.AutoSize = true;
            this.earLabel.Location = new System.Drawing.Point(44, 164);
            this.earLabel.Name = "earLabel";
            this.earLabel.Size = new System.Drawing.Size(71, 16);
            this.earLabel.TabIndex = 8;
            this.earLabel.Text = "Ear Length";
            // 
            // EarLength
            // 
            this.EarLength.Location = new System.Drawing.Point(47, 184);
            this.EarLength.Name = "EarLength";
            this.EarLength.Size = new System.Drawing.Size(100, 22);
            this.EarLength.TabIndex = 9;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 453);
            this.Controls.Add(this.EarLength);
            this.Controls.Add(this.earLabel);
            this.Controls.Add(this.withEar);
            this.Controls.Add(this.SpacingInput);
            this.Controls.Add(this.spacingLabel);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.drawHorizontal);
            this.Controls.Add(this.drawVertical);
            this.Name = "MainWindow";
            this.Text = "Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.CheckBox drawVertical;
        private System.Windows.Forms.CheckBox drawHorizontal;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Label spacingLabel;
        private System.Windows.Forms.MaskedTextBox SpacingInput;
        private System.Windows.Forms.CheckBox withEar;
        private System.Windows.Forms.Label earLabel;
        private System.Windows.Forms.MaskedTextBox EarLength;
    }
}