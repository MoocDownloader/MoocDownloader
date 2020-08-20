namespace MoocDownloader.App
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
            this.LoginMoocButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoginMoocButton
            // 
            this.LoginMoocButton.Location = new System.Drawing.Point(12, 12);
            this.LoginMoocButton.Name = "LoginMoocButton";
            this.LoginMoocButton.Size = new System.Drawing.Size(151, 23);
            this.LoginMoocButton.TabIndex = 0;
            this.LoginMoocButton.Text = "登录中国大学 MOOC";
            this.LoginMoocButton.UseVisualStyleBackColor = true;
            this.LoginMoocButton.Click += new System.EventHandler(this.LoginMoocButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 499);
            this.Controls.Add(this.LoginMoocButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "中国大学 MOOC 下载器";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoginMoocButton;
    }
}

