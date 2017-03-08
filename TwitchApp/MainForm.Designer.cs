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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.twitchLogin = new System.Windows.Forms.Button();
            this.labelCurrentGame = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // twitchLogin
            // 
            this.twitchLogin.Location = new System.Drawing.Point(12, 12);
            this.twitchLogin.Name = "twitchLogin";
            this.twitchLogin.Size = new System.Drawing.Size(361, 51);
            this.twitchLogin.TabIndex = 0;
            this.twitchLogin.Text = "Login to Twitch";
            this.twitchLogin.UseVisualStyleBackColor = true;
            this.twitchLogin.Click += new System.EventHandler(this.twitchLogin_Click);
            // 
            // labelCurrentGame
            // 
            this.labelCurrentGame.AutoSize = true;
            this.labelCurrentGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentGame.Location = new System.Drawing.Point(8, 66);
            this.labelCurrentGame.Name = "labelCurrentGame";
            this.labelCurrentGame.Size = new System.Drawing.Size(144, 20);
            this.labelCurrentGame.TabIndex = 1;
            this.labelCurrentGame.Text = "Current Game: N/A";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 94);
            this.Controls.Add(this.labelCurrentGame);
            this.Controls.Add(this.twitchLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Westie1010\'s TwitchTV App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button twitchLogin;
        private System.Windows.Forms.Label labelCurrentGame;
    }
}