namespace MoocDownloader.App.Views
{
    partial class LoginForm
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
            this.MoocWebBrowser = new Gecko.GeckoWebBrowser();
            this.SuspendLayout();
            // 
            // MoocWebBrowser
            // 
            this.MoocWebBrowser.ConsoleMessageEventReceivesConsoleLogCalls = true;
            this.MoocWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MoocWebBrowser.FrameEventsPropagateToMainWindow = false;
            this.MoocWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.MoocWebBrowser.Name = "MoocWebBrowser";
            this.MoocWebBrowser.Size = new System.Drawing.Size(1334, 611);
            this.MoocWebBrowser.TabIndex = 0;
            this.MoocWebBrowser.UseHttpActivityObserver = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 611);
            this.Controls.Add(this.MoocWebBrowser);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中国大学 MOOC - 登录";
            this.ResumeLayout(false);

        }

        #endregion

        private Gecko.GeckoWebBrowser MoocWebBrowser;
    }
}