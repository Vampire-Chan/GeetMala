namespace MusicPlayerWinForms
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button finishLoginButton;

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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.finishLoginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 12);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(760, 450);
            this.webBrowser.TabIndex = 0;
            // 
            // finishLoginButton
            // 
            this.finishLoginButton.Location = new System.Drawing.Point(12, 480);
            this.finishLoginButton.Name = "finishLoginButton";
            this.finishLoginButton.Size = new System.Drawing.Size(150, 30);
            this.finishLoginButton.TabIndex = 1;
            this.finishLoginButton.Text = "Finish Login";
            this.finishLoginButton.UseVisualStyleBackColor = true;
            this.finishLoginButton.Click += new System.EventHandler(this.finishLoginButton_Click);
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 531);
            this.Controls.Add(this.finishLoginButton);
            this.Controls.Add(this.webBrowser);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.ResumeLayout(false);
        }

        #endregion
    }
}