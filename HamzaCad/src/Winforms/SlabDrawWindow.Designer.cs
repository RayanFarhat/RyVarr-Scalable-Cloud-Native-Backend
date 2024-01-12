namespace HamzaCad.src.Winforms
{
    partial class SlabDrawWindow
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
            this.panelNav = new System.Windows.Forms.Panel();
            this.TextEditorNav = new System.Windows.Forms.RadioButton();
            this.BarsNav = new System.Windows.Forms.RadioButton();
            this.DrawingNav = new System.Windows.Forms.RadioButton();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.DrawBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.barShapesNav = new System.Windows.Forms.RadioButton();
            this.panelNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNav
            // 
            this.panelNav.BackColor = System.Drawing.Color.White;
            this.panelNav.Controls.Add(this.barShapesNav);
            this.panelNav.Controls.Add(this.CancelBtn);
            this.panelNav.Controls.Add(this.DrawBtn);
            this.panelNav.Controls.Add(this.DrawingNav);
            this.panelNav.Controls.Add(this.TextEditorNav);
            this.panelNav.Controls.Add(this.BarsNav);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNav.Location = new System.Drawing.Point(0, 0);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(1200, 49);
            this.panelNav.TabIndex = 0;
            // 
            // TextEditorNav
            // 
            this.TextEditorNav.Appearance = System.Windows.Forms.Appearance.Button;
            this.TextEditorNav.AutoSize = true;
            this.TextEditorNav.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGray;
            this.TextEditorNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TextEditorNav.Location = new System.Drawing.Point(81, 12);
            this.TextEditorNav.Name = "TextEditorNav";
            this.TextEditorNav.Size = new System.Drawing.Size(80, 28);
            this.TextEditorNav.TabIndex = 3;
            this.TextEditorNav.Text = "TextEditor";
            this.TextEditorNav.UseVisualStyleBackColor = true;
            this.TextEditorNav.CheckedChanged += new System.EventHandler(this.TextEditorNav_CheckedChanged);
            // 
            // BarsNav
            // 
            this.BarsNav.Appearance = System.Windows.Forms.Appearance.Button;
            this.BarsNav.AutoSize = true;
            this.BarsNav.Checked = true;
            this.BarsNav.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGray;
            this.BarsNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BarsNav.Location = new System.Drawing.Point(12, 12);
            this.BarsNav.Name = "BarsNav";
            this.BarsNav.Size = new System.Drawing.Size(47, 28);
            this.BarsNav.TabIndex = 2;
            this.BarsNav.TabStop = true;
            this.BarsNav.Text = "Bars";
            this.BarsNav.UseVisualStyleBackColor = true;
            this.BarsNav.CheckedChanged += new System.EventHandler(this.BarsNav_CheckedChanged);
            // 
            // DrawingNav
            // 
            this.DrawingNav.Appearance = System.Windows.Forms.Appearance.Button;
            this.DrawingNav.AutoSize = true;
            this.DrawingNav.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGray;
            this.DrawingNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DrawingNav.Location = new System.Drawing.Point(179, 12);
            this.DrawingNav.Name = "DrawingNav";
            this.DrawingNav.Size = new System.Drawing.Size(68, 28);
            this.DrawingNav.TabIndex = 4;
            this.DrawingNav.Text = "Drawing";
            this.DrawingNav.UseVisualStyleBackColor = true;
            this.DrawingNav.CheckedChanged += new System.EventHandler(this.DrawingNav_CheckedChanged);
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelContainer.Location = new System.Drawing.Point(0, 46);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1200, 445);
            this.panelContainer.TabIndex = 1;
            // 
            // DrawBtn
            // 
            this.DrawBtn.BackColor = System.Drawing.Color.Chartreuse;
            this.DrawBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DrawBtn.Location = new System.Drawing.Point(1113, 12);
            this.DrawBtn.Name = "DrawBtn";
            this.DrawBtn.Size = new System.Drawing.Size(75, 23);
            this.DrawBtn.TabIndex = 5;
            this.DrawBtn.Text = "Draw";
            this.DrawBtn.UseVisualStyleBackColor = false;
            this.DrawBtn.Click += new System.EventHandler(this.DrawBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.LightCoral;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBtn.Location = new System.Drawing.Point(1023, 12);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // barShapesNav
            // 
            this.barShapesNav.Appearance = System.Windows.Forms.Appearance.Button;
            this.barShapesNav.AutoSize = true;
            this.barShapesNav.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGray;
            this.barShapesNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.barShapesNav.Location = new System.Drawing.Point(263, 12);
            this.barShapesNav.Name = "barShapesNav";
            this.barShapesNav.Size = new System.Drawing.Size(83, 28);
            this.barShapesNav.TabIndex = 7;
            this.barShapesNav.Text = "Bar Shape";
            this.barShapesNav.UseVisualStyleBackColor = true;
            this.barShapesNav.CheckedChanged += new System.EventHandler(this.barShapesNav_CheckedChanged);
            // 
            // SlabDrawWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 491);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelNav);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SlabDrawWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SlabDrawWindow";
            this.panelNav.ResumeLayout(false);
            this.panelNav.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.RadioButton TextEditorNav;
        private System.Windows.Forms.RadioButton BarsNav;
        private System.Windows.Forms.RadioButton DrawingNav;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button DrawBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.RadioButton barShapesNav;
    }
}