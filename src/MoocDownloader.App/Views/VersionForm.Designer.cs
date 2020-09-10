namespace MoocDownloader.App.Views
{
    partial class VersionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentVersionLabel = new System.Windows.Forms.Label();
            this.NewVersionLabel = new System.Windows.Forms.Label();
            this.CancelDownloadButton = new System.Windows.Forms.Button();
            this.DownloadNewVersionButton = new System.Windows.Forms.Button();
            this.UpdateMessageTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前版本:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "最新版本:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(16, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "更新说明:";
            // 
            // CurrentVersionLabel
            // 
            this.CurrentVersionLabel.AutoSize = true;
            this.CurrentVersionLabel.Location = new System.Drawing.Point(80, 14);
            this.CurrentVersionLabel.Name = "CurrentVersionLabel";
            this.CurrentVersionLabel.Size = new System.Drawing.Size(40, 13);
            this.CurrentVersionLabel.TabIndex = 0;
            this.CurrentVersionLabel.Text = "0.0.0.0";
            // 
            // NewVersionLabel
            // 
            this.NewVersionLabel.AutoSize = true;
            this.NewVersionLabel.Location = new System.Drawing.Point(80, 40);
            this.NewVersionLabel.Name = "NewVersionLabel";
            this.NewVersionLabel.Size = new System.Drawing.Size(40, 13);
            this.NewVersionLabel.TabIndex = 0;
            this.NewVersionLabel.Text = "0.0.0.0";
            // 
            // CancelDownloadButton
            // 
            this.CancelDownloadButton.Location = new System.Drawing.Point(286, 205);
            this.CancelDownloadButton.Name = "CancelDownloadButton";
            this.CancelDownloadButton.Size = new System.Drawing.Size(75, 23);
            this.CancelDownloadButton.TabIndex = 2;
            this.CancelDownloadButton.Text = "暂不更新";
            this.CancelDownloadButton.UseVisualStyleBackColor = true;
            this.CancelDownloadButton.Click += new System.EventHandler(this.CancelDownloadButton_Click);
            // 
            // DownloadNewVersionButton
            // 
            this.DownloadNewVersionButton.Location = new System.Drawing.Point(205, 205);
            this.DownloadNewVersionButton.Name = "DownloadNewVersionButton";
            this.DownloadNewVersionButton.Size = new System.Drawing.Size(75, 23);
            this.DownloadNewVersionButton.TabIndex = 2;
            this.DownloadNewVersionButton.Text = "下载更新";
            this.DownloadNewVersionButton.UseVisualStyleBackColor = true;
            this.DownloadNewVersionButton.Click += new System.EventHandler(this.DownloadNewVersionButton_Click);
            // 
            // UpdateMessageTextBox
            // 
            this.UpdateMessageTextBox.BackColor = System.Drawing.Color.White;
            this.UpdateMessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UpdateMessageTextBox.Location = new System.Drawing.Point(83, 66);
            this.UpdateMessageTextBox.Multiline = true;
            this.UpdateMessageTextBox.Name = "UpdateMessageTextBox";
            this.UpdateMessageTextBox.ReadOnly = true;
            this.UpdateMessageTextBox.Size = new System.Drawing.Size(278, 133);
            this.UpdateMessageTextBox.TabIndex = 3;
            // 
            // VersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(379, 240);
            this.Controls.Add(this.DownloadNewVersionButton);
            this.Controls.Add(this.CancelDownloadButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NewVersionLabel);
            this.Controls.Add(this.CurrentVersionLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpdateMessageTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VersionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "有新版本! - 更新程序";
            this.Load += new System.EventHandler(this.VersionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CurrentVersionLabel;
        private System.Windows.Forms.Label NewVersionLabel;
        private System.Windows.Forms.Button CancelDownloadButton;
        private System.Windows.Forms.Button DownloadNewVersionButton;
        private System.Windows.Forms.TextBox UpdateMessageTextBox;
    }
}