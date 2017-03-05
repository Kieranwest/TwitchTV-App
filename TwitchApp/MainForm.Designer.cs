namespace TwitchTV_App
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
            this.labelFollowers = new System.Windows.Forms.Label();
            this.updateLabel = new System.Windows.Forms.Button();
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
            // labelFollowers
            // 
            this.labelFollowers.AutoSize = true;
            this.labelFollowers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFollowers.Location = new System.Drawing.Point(87, 215);
            this.labelFollowers.Name = "labelFollowers";
            this.labelFollowers.Size = new System.Drawing.Size(110, 20);
            this.labelFollowers.TabIndex = 1;
            this.labelFollowers.Text = "Followers: N/A";
            // 
            // updateLabel
            // 
            this.updateLabel.Location = new System.Drawing.Point(12, 69);
            this.updateLabel.Name = "updateLabel";
            this.updateLabel.Size = new System.Drawing.Size(260, 50);
            this.updateLabel.TabIndex = 2;
            this.updateLabel.Text = "Update Label";
            this.updateLabel.UseVisualStyleBackColor = true;
            this.updateLabel.Click += new System.EventHandler(this.updateLabel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 244);
            this.Controls.Add(this.updateLabel);
            this.Controls.Add(this.labelFollowers);
            this.Controls.Add(this.twitchLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button twitchLogin;
        private System.Windows.Forms.Label labelFollowers;
        private System.Windows.Forms.Button updateLabel;
    }
}