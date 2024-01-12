namespace HamzaCad.src.Winforms
{
    partial class UC_barShapes
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
            this.BarShapesPanelContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // BarShapesPanelContainer
            // 
            this.BarShapesPanelContainer.Location = new System.Drawing.Point(3, 3);
            this.BarShapesPanelContainer.Name = "BarShapesPanelContainer";
            this.BarShapesPanelContainer.Size = new System.Drawing.Size(390, 439);
            this.BarShapesPanelContainer.TabIndex = 1;
            // 
            // UC_barShapes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BarShapesPanelContainer);
            this.Name = "UC_barShapes";
            this.Size = new System.Drawing.Size(1200, 445);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel BarShapesPanelContainer;
    }
}
