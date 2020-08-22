namespace MoocDownloader.App.Views
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
            this.CourseUrlTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SavePathTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FindPathButton = new System.Windows.Forms.Button();
            this.SDRadioButton = new System.Windows.Forms.RadioButton();
            this.HDRadioButton = new System.Windows.Forms.RadioButton();
            this.DownloadSubtitleCheckBox = new System.Windows.Forms.CheckBox();
            this.DownloadVideoCheckBox = new System.Windows.Forms.CheckBox();
            this.DownloadDocumentCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.StartDownloadButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RunningLogListBox = new System.Windows.Forms.ListBox();
            this.TotalProgressBar = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.CurrentTaskTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginMoocButton
            // 
            this.LoginMoocButton.Location = new System.Drawing.Point(70, 50);
            this.LoginMoocButton.Name = "LoginMoocButton";
            this.LoginMoocButton.Size = new System.Drawing.Size(178, 25);
            this.LoginMoocButton.TabIndex = 0;
            this.LoginMoocButton.Text = "登录中国大学 MOOC";
            this.LoginMoocButton.UseVisualStyleBackColor = true;
            this.LoginMoocButton.Click += new System.EventHandler(this.LoginMoocButton_Click);
            // 
            // CourseUrlTextBox
            // 
            this.CourseUrlTextBox.Location = new System.Drawing.Point(70, 113);
            this.CourseUrlTextBox.Name = "CourseUrlTextBox";
            this.CourseUrlTextBox.Size = new System.Drawing.Size(178, 20);
            this.CourseUrlTextBox.TabIndex = 1;
            this.CourseUrlTextBox.TextChanged += new System.EventHandler(this.CourseUrlTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "第一步:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "第二步:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "点击登录中国大学 MOOC";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "输入课程主页链接";
            // 
            // SavePathTextBox
            // 
            this.SavePathTextBox.Location = new System.Drawing.Point(70, 171);
            this.SavePathTextBox.Name = "SavePathTextBox";
            this.SavePathTextBox.Size = new System.Drawing.Size(132, 20);
            this.SavePathTextBox.TabIndex = 1;
            this.SavePathTextBox.TextChanged += new System.EventHandler(this.SavePathTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "设置课程保存目录";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "第三步:";
            // 
            // FindPathButton
            // 
            this.FindPathButton.Location = new System.Drawing.Point(208, 170);
            this.FindPathButton.Name = "FindPathButton";
            this.FindPathButton.Size = new System.Drawing.Size(40, 25);
            this.FindPathButton.TabIndex = 3;
            this.FindPathButton.Text = "浏览";
            this.FindPathButton.UseVisualStyleBackColor = true;
            this.FindPathButton.Click += new System.EventHandler(this.FindPathButton_Click);
            // 
            // SDRadioButton
            // 
            this.SDRadioButton.AutoSize = true;
            this.SDRadioButton.Location = new System.Drawing.Point(104, 21);
            this.SDRadioButton.Name = "SDRadioButton";
            this.SDRadioButton.Size = new System.Drawing.Size(49, 17);
            this.SDRadioButton.TabIndex = 4;
            this.SDRadioButton.Text = "标清";
            this.SDRadioButton.UseVisualStyleBackColor = true;
            this.SDRadioButton.CheckedChanged += new System.EventHandler(this.SDRadioButton_CheckedChanged);
            // 
            // HDRadioButton
            // 
            this.HDRadioButton.AutoSize = true;
            this.HDRadioButton.Checked = true;
            this.HDRadioButton.Location = new System.Drawing.Point(163, 21);
            this.HDRadioButton.Name = "HDRadioButton";
            this.HDRadioButton.Size = new System.Drawing.Size(49, 17);
            this.HDRadioButton.TabIndex = 4;
            this.HDRadioButton.TabStop = true;
            this.HDRadioButton.Text = "高清";
            this.HDRadioButton.UseVisualStyleBackColor = true;
            this.HDRadioButton.CheckedChanged += new System.EventHandler(this.HDRadioButton_CheckedChanged);
            // 
            // DownloadSubtitleCheckBox
            // 
            this.DownloadSubtitleCheckBox.AutoSize = true;
            this.DownloadSubtitleCheckBox.Checked = true;
            this.DownloadSubtitleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadSubtitleCheckBox.Location = new System.Drawing.Point(104, 51);
            this.DownloadSubtitleCheckBox.Name = "DownloadSubtitleCheckBox";
            this.DownloadSubtitleCheckBox.Size = new System.Drawing.Size(74, 17);
            this.DownloadSubtitleCheckBox.TabIndex = 5;
            this.DownloadSubtitleCheckBox.Text = "下载字幕";
            this.DownloadSubtitleCheckBox.UseVisualStyleBackColor = true;
            this.DownloadSubtitleCheckBox.CheckedChanged += new System.EventHandler(this.DownloadSubtitleCheckBox_CheckedChanged);
            // 
            // DownloadVideoCheckBox
            // 
            this.DownloadVideoCheckBox.AutoSize = true;
            this.DownloadVideoCheckBox.Checked = true;
            this.DownloadVideoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadVideoCheckBox.Location = new System.Drawing.Point(15, 22);
            this.DownloadVideoCheckBox.Name = "DownloadVideoCheckBox";
            this.DownloadVideoCheckBox.Size = new System.Drawing.Size(74, 17);
            this.DownloadVideoCheckBox.TabIndex = 5;
            this.DownloadVideoCheckBox.Text = "下载视频";
            this.DownloadVideoCheckBox.UseVisualStyleBackColor = true;
            this.DownloadVideoCheckBox.CheckedChanged += new System.EventHandler(this.DownloadVideoCheckBox_CheckedChanged);
            // 
            // DownloadDocumentCheckBox
            // 
            this.DownloadDocumentCheckBox.AutoSize = true;
            this.DownloadDocumentCheckBox.Checked = true;
            this.DownloadDocumentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadDocumentCheckBox.Location = new System.Drawing.Point(15, 51);
            this.DownloadDocumentCheckBox.Name = "DownloadDocumentCheckBox";
            this.DownloadDocumentCheckBox.Size = new System.Drawing.Size(74, 17);
            this.DownloadDocumentCheckBox.TabIndex = 5;
            this.DownloadDocumentCheckBox.Text = "下载课件";
            this.DownloadDocumentCheckBox.UseVisualStyleBackColor = true;
            this.DownloadDocumentCheckBox.CheckedChanged += new System.EventHandler(this.DownloadDocumentCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DownloadVideoCheckBox);
            this.groupBox1.Controls.Add(this.DownloadDocumentCheckBox);
            this.groupBox1.Controls.Add(this.SDRadioButton);
            this.groupBox1.Controls.Add(this.DownloadSubtitleCheckBox);
            this.groupBox1.Controls.Add(this.HDRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(285, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 85);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下载选项";
            // 
            // StartDownloadButton
            // 
            this.StartDownloadButton.Location = new System.Drawing.Point(333, 182);
            this.StartDownloadButton.Name = "StartDownloadButton";
            this.StartDownloadButton.Size = new System.Drawing.Size(182, 40);
            this.StartDownloadButton.TabIndex = 7;
            this.StartDownloadButton.Text = "开始下载";
            this.StartDownloadButton.UseVisualStyleBackColor = true;
            this.StartDownloadButton.Click += new System.EventHandler(this.StartDownloadButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RunningLogListBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 171);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "运行日志";
            // 
            // RunningLogListBox
            // 
            this.RunningLogListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RunningLogListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RunningLogListBox.FormattingEnabled = true;
            this.RunningLogListBox.Location = new System.Drawing.Point(3, 16);
            this.RunningLogListBox.Name = "RunningLogListBox";
            this.RunningLogListBox.Size = new System.Drawing.Size(497, 152);
            this.RunningLogListBox.TabIndex = 0;
            // 
            // TotalProgressBar
            // 
            this.TotalProgressBar.Location = new System.Drawing.Point(333, 108);
            this.TotalProgressBar.Name = "TotalProgressBar";
            this.TotalProgressBar.Size = new System.Drawing.Size(182, 25);
            this.TotalProgressBar.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(292, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "进度:";
            // 
            // CurrentTaskTextBox
            // 
            this.CurrentTaskTextBox.Location = new System.Drawing.Point(333, 146);
            this.CurrentTaskTextBox.Name = "CurrentTaskTextBox";
            this.CurrentTaskTextBox.ReadOnly = true;
            this.CurrentTaskTextBox.Size = new System.Drawing.Size(182, 20);
            this.CurrentTaskTextBox.TabIndex = 11;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.LoginMoocButton);
            this.groupBox3.Controls.Add(this.CourseUrlTextBox);
            this.groupBox3.Controls.Add(this.SavePathTextBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.FindPathButton);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(12, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 209);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "课程设置";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(292, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "当前:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(527, 413);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.CurrentTaskTextBox);
            this.Controls.Add(this.TotalProgressBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.StartDownloadButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中国大学 MOOC 下载器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoginMoocButton;
        private System.Windows.Forms.TextBox CourseUrlTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SavePathTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button FindPathButton;
        private System.Windows.Forms.RadioButton SDRadioButton;
        private System.Windows.Forms.RadioButton HDRadioButton;
        private System.Windows.Forms.CheckBox DownloadSubtitleCheckBox;
        private System.Windows.Forms.CheckBox DownloadVideoCheckBox;
        private System.Windows.Forms.CheckBox DownloadDocumentCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button StartDownloadButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar TotalProgressBar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox CurrentTaskTextBox;
        private System.Windows.Forms.ListBox RunningLogListBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
    }
}

