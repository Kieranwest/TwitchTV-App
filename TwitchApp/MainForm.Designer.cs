namespace WindowsFormsApplication1
{
    partial class MainForm
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
            this.twitchLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // twitchLogin
            // 
            this.twitchLogin.Location = new System.Drawing.Point(12, 12);
            this.twitchLogin.Name = "twitchLogin";
            this.twitchLogin.Size = new System.Drawing.Size(260, 51);
            this.twitchLogin.TabIndex = 0;
            this.twitchLogin.Text = "Login to Twitch";
            this.twitchLogin.UseVisualStyleBackColor = true;
            this.twitchLogin.Click += new System.EventHandler(this.twitchLogin_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 244);
            this.Controls.Add(this.twitchLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button twitchLogin;
    }
}