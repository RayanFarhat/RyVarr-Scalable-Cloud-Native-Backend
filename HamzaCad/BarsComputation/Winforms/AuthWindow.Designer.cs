namespace HamzaCad.BarsComputation.Winforms
{
    partial class AuthWindow
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
            this.AuthToken = new System.Windows.Forms.MaskedTextBox();
            this.AuthLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AuthWinCloseBtn = new System.Windows.Forms.Button();
            this.AuthTokenClearBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AuthToken
            // 
            this.AuthToken.Location = new System.Drawing.Point(246, 64);
            this.AuthToken.Name = "AuthToken";
            this.AuthToken.Size = new System.Drawing.Size(436, 22);
            this.AuthToken.TabIndex = 6;
            // 
            // AuthLabel
            // 
            this.AuthLabel.Location = new System.Drawing.Point(70, 67);
            this.AuthLabel.Name = "AuthLabel";
            this.AuthLabel.Size = new System.Drawing.Size(149, 27);
            this.AuthLabel.TabIndex = 5;
            this.AuthLabel.Text = "Authorization Token";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(73, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(586, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "You need to copy the token from our website to here in order to authorize you and" +
    " use the program!";
            // 
            // AuthWinCloseBtn
            // 
            this.AuthWinCloseBtn.Location = new System.Drawing.Point(315, 281);
            this.AuthWinCloseBtn.Name = "AuthWinCloseBtn";
            this.AuthWinCloseBtn.Size = new System.Drawing.Size(119, 88);
            this.AuthWinCloseBtn.TabIndex = 8;
            this.AuthWinCloseBtn.Text = "Close";
            this.AuthWinCloseBtn.UseVisualStyleBackColor = true;
            // 
            // AuthTokenClearBtn
            // 
            this.AuthTokenClearBtn.BackColor = System.Drawing.Color.LightCoral;
            this.AuthTokenClearBtn.Location = new System.Drawing.Point(703, 62);
            this.AuthTokenClearBtn.Name = "AuthTokenClearBtn";
            this.AuthTokenClearBtn.Size = new System.Drawing.Size(75, 23);
            this.AuthTokenClearBtn.TabIndex = 9;
            this.AuthTokenClearBtn.Text = "Clear";
            this.AuthTokenClearBtn.UseVisualStyleBackColor = false;
            // 
            // AuthWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AuthTokenClearBtn);
            this.Controls.Add(this.AuthWinCloseBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AuthToken);
            this.Controls.Add(this.AuthLabel);
            this.Name = "AuthWindow";
            this.Text = "AuthWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox AuthToken;
        private System.Windows.Forms.Label AuthLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AuthWinCloseBtn;
        private System.Windows.Forms.Button AuthTokenClearBtn;
    }
}