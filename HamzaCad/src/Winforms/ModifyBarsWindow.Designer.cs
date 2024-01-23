namespace HamzaCad.src.Winforms
{
    partial class ModifyBarsWindow
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
            this.barShapesNav = new System.Windows.Forms.RadioButton();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.DrawBtn = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNav
            // 
            this.panelNav.BackColor = System.Drawing.Color.White;
            this.panelNav.Controls.Add(this.barShapesNav);
            this.panelNav.Controls.Add(this.CancelBtn);
            this.panelNav.Controls.Add(this.DrawBtn);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNav.Location = new System.Drawing.Point(0, 0);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(1200, 49);
            this.panelNav.TabIndex = 1;
            // 
            // barShapesNav
            // 
            this.barShapesNav.Appearance = System.Windows.Forms.Appearance.Button;
            this.barShapesNav.AutoSize = true;
            this.barShapesNav.Checked = true;
            this.barShapesNav.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGray;
            this.barShapesNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.barShapesNav.Location = new System.Drawing.Point(12, 12);
            this.barShapesNav.Name = "barShapesNav";
            this.barShapesNav.Size = new System.Drawing.Size(83, 28);
            this.barShapesNav.TabIndex = 7;
            this.barShapesNav.TabStop = true;
            this.barShapesNav.Text = "Bar Shape";
            this.barShapesNav.UseVisualStyleBackColor = true;
            this.barShapesNav.CheckedChanged += new System.EventHandler(this.barShapesNav_CheckedChanged);
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
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelContainer.Location = new System.Drawing.Point(0, 46);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1200, 445);
            this.panelContainer.TabIndex = 2;
            // 
            // ModifyBarsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 491);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelNav);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ModifyBarsWindow";
            this.Text = "ModifyBarsWindow";
            this.panelNav.ResumeLayout(false);
            this.panelNav.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.RadioButton barShapesNav;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button DrawBtn;
        private System.Windows.Forms.Panel panelContainer;
    }
}