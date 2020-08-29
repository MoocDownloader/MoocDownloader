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
            this.DownloadAttachmentCheckBox = new System.Windows.Forms.CheckBox();
            this.StartDownloadButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RunningLogListBox = new System.Windows.Forms.ListBox();
            this.TotalProgressBar = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.CurrentTaskTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CancelDownloadButton = new System.Windows.Forms.Button();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteCourseLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetCourseSavePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FeedbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadAttachmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadSubtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CourseQualityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
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
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "设置课程下载保存路径";
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
            this.SDRadioButton.Location = new System.Drawing.Point(183, 51);
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
            this.HDRadioButton.Location = new System.Drawing.Point(183, 22);
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
            this.DownloadSubtitleCheckBox.Location = new System.Drawing.Point(99, 51);
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
            this.groupBox1.Controls.Add(this.DownloadAttachmentCheckBox);
            this.groupBox1.Controls.Add(this.DownloadSubtitleCheckBox);
            this.groupBox1.Controls.Add(this.HDRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(285, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 85);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下载选项";
            // 
            // DownloadAttachmentCheckBox
            // 
            this.DownloadAttachmentCheckBox.AutoSize = true;
            this.DownloadAttachmentCheckBox.Checked = true;
            this.DownloadAttachmentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadAttachmentCheckBox.Location = new System.Drawing.Point(99, 22);
            this.DownloadAttachmentCheckBox.Name = "DownloadAttachmentCheckBox";
            this.DownloadAttachmentCheckBox.Size = new System.Drawing.Size(74, 17);
            this.DownloadAttachmentCheckBox.TabIndex = 5;
            this.DownloadAttachmentCheckBox.Text = "下载附件";
            this.DownloadAttachmentCheckBox.UseVisualStyleBackColor = true;
            this.DownloadAttachmentCheckBox.CheckedChanged += new System.EventHandler(this.DownloadAttachmentCheckBox_CheckedChanged);
            // 
            // StartDownloadButton
            // 
            this.StartDownloadButton.Location = new System.Drawing.Point(333, 196);
            this.StartDownloadButton.Name = "StartDownloadButton";
            this.StartDownloadButton.Size = new System.Drawing.Size(125, 40);
            this.StartDownloadButton.TabIndex = 7;
            this.StartDownloadButton.Text = "开始下载";
            this.StartDownloadButton.UseVisualStyleBackColor = true;
            this.StartDownloadButton.Click += new System.EventHandler(this.StartDownloadButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RunningLogListBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 242);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(515, 149);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "运行日志";
            // 
            // RunningLogListBox
            // 
            this.RunningLogListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RunningLogListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RunningLogListBox.FormattingEnabled = true;
            this.RunningLogListBox.HorizontalScrollbar = true;
            this.RunningLogListBox.Location = new System.Drawing.Point(3, 16);
            this.RunningLogListBox.Name = "RunningLogListBox";
            this.RunningLogListBox.Size = new System.Drawing.Size(509, 130);
            this.RunningLogListBox.TabIndex = 0;
            // 
            // TotalProgressBar
            // 
            this.TotalProgressBar.Location = new System.Drawing.Point(333, 122);
            this.TotalProgressBar.Name = "TotalProgressBar";
            this.TotalProgressBar.Size = new System.Drawing.Size(194, 25);
            this.TotalProgressBar.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(292, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "进度:";
            // 
            // CurrentTaskTextBox
            // 
            this.CurrentTaskTextBox.Location = new System.Drawing.Point(333, 160);
            this.CurrentTaskTextBox.Name = "CurrentTaskTextBox";
            this.CurrentTaskTextBox.ReadOnly = true;
            this.CurrentTaskTextBox.Size = new System.Drawing.Size(194, 20);
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
            this.groupBox3.Location = new System.Drawing.Point(12, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 209);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "课程设置";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(292, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "当前:";
            // 
            // CancelDownloadButton
            // 
            this.CancelDownloadButton.Location = new System.Drawing.Point(464, 196);
            this.CancelDownloadButton.Name = "CancelDownloadButton";
            this.CancelDownloadButton.Size = new System.Drawing.Size(63, 40);
            this.CancelDownloadButton.TabIndex = 7;
            this.CancelDownloadButton.Text = "取消";
            this.CancelDownloadButton.UseVisualStyleBackColor = true;
            this.CancelDownloadButton.Click += new System.EventHandler(this.StartDownloadButton_Click);
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.SettingToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(539, 24);
            this.MainMenuStrip.TabIndex = 13;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartDownloadToolStripMenuItem,
            this.CancelDownloadToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.FileToolStripMenuItem.Text = "文件";
            // 
            // StartDownloadToolStripMenuItem
            // 
            this.StartDownloadToolStripMenuItem.Name = "StartDownloadToolStripMenuItem";
            this.StartDownloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.StartDownloadToolStripMenuItem.Text = "开始下载";
            // 
            // CancelDownloadToolStripMenuItem
            // 
            this.CancelDownloadToolStripMenuItem.Name = "CancelDownloadToolStripMenuItem";
            this.CancelDownloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.CancelDownloadToolStripMenuItem.Text = "取消下载";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            // 
            // SettingToolStripMenuItem
            // 
            this.SettingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoginToolStripMenuItem,
            this.PasteCourseLinkToolStripMenuItem,
            this.SetCourseSavePathToolStripMenuItem,
            this.DownloadOptionToolStripMenuItem,
            this.CourseQualityToolStripMenuItem});
            this.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem";
            this.SettingToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.SettingToolStripMenuItem.Text = "设置";
            // 
            // LoginToolStripMenuItem
            // 
            this.LoginToolStripMenuItem.Name = "LoginToolStripMenuItem";
            this.LoginToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.LoginToolStripMenuItem.Text = "登录中国大学 MOOC";
            // 
            // PasteCourseLinkToolStripMenuItem
            // 
            this.PasteCourseLinkToolStripMenuItem.Name = "PasteCourseLinkToolStripMenuItem";
            this.PasteCourseLinkToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.PasteCourseLinkToolStripMenuItem.Text = "粘贴课程链接";
            // 
            // SetCourseSavePathToolStripMenuItem
            // 
            this.SetCourseSavePathToolStripMenuItem.Name = "SetCourseSavePathToolStripMenuItem";
            this.SetCourseSavePathToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.SetCourseSavePathToolStripMenuItem.Text = "设置课程下载保存路径";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewHelpToolStripMenuItem,
            this.UpdateToolStripMenuItem,
            this.toolStripSeparator2,
            this.FeedbackToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.HelpToolStripMenuItem.Text = "帮助";
            // 
            // ViewHelpToolStripMenuItem
            // 
            this.ViewHelpToolStripMenuItem.Name = "ViewHelpToolStripMenuItem";
            this.ViewHelpToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ViewHelpToolStripMenuItem.Text = "查看帮助";
            // 
            // UpdateToolStripMenuItem
            // 
            this.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem";
            this.UpdateToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.UpdateToolStripMenuItem.Text = "更新程序";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // FeedbackToolStripMenuItem
            // 
            this.FeedbackToolStripMenuItem.Name = "FeedbackToolStripMenuItem";
            this.FeedbackToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FeedbackToolStripMenuItem.Text = "反馈";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.AboutToolStripMenuItem.Text = "关于";
            // 
            // DownloadOptionToolStripMenuItem
            // 
            this.DownloadOptionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DownloadVideoToolStripMenuItem,
            this.DownloadAttachmentToolStripMenuItem,
            this.DownloadDocumentToolStripMenuItem,
            this.DownloadSubtitlesToolStripMenuItem});
            this.DownloadOptionToolStripMenuItem.Name = "DownloadOptionToolStripMenuItem";
            this.DownloadOptionToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.DownloadOptionToolStripMenuItem.Text = "下载选项";
            // 
            // DownloadVideoToolStripMenuItem
            // 
            this.DownloadVideoToolStripMenuItem.Checked = true;
            this.DownloadVideoToolStripMenuItem.CheckOnClick = true;
            this.DownloadVideoToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadVideoToolStripMenuItem.Name = "DownloadVideoToolStripMenuItem";
            this.DownloadVideoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DownloadVideoToolStripMenuItem.Text = "下载视频";
            // 
            // DownloadAttachmentToolStripMenuItem
            // 
            this.DownloadAttachmentToolStripMenuItem.Checked = true;
            this.DownloadAttachmentToolStripMenuItem.CheckOnClick = true;
            this.DownloadAttachmentToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadAttachmentToolStripMenuItem.Name = "DownloadAttachmentToolStripMenuItem";
            this.DownloadAttachmentToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DownloadAttachmentToolStripMenuItem.Text = "下载附件";
            // 
            // DownloadDocumentToolStripMenuItem
            // 
            this.DownloadDocumentToolStripMenuItem.Checked = true;
            this.DownloadDocumentToolStripMenuItem.CheckOnClick = true;
            this.DownloadDocumentToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadDocumentToolStripMenuItem.Name = "DownloadDocumentToolStripMenuItem";
            this.DownloadDocumentToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DownloadDocumentToolStripMenuItem.Text = "下载课件";
            // 
            // DownloadSubtitlesToolStripMenuItem
            // 
            this.DownloadSubtitlesToolStripMenuItem.Checked = true;
            this.DownloadSubtitlesToolStripMenuItem.CheckOnClick = true;
            this.DownloadSubtitlesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadSubtitlesToolStripMenuItem.Name = "DownloadSubtitlesToolStripMenuItem";
            this.DownloadSubtitlesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DownloadSubtitlesToolStripMenuItem.Text = "下载字幕";
            // 
            // CourseQualityToolStripMenuItem
            // 
            this.CourseQualityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HDToolStripMenuItem,
            this.SDToolStripMenuItem});
            this.CourseQualityToolStripMenuItem.Name = "CourseQualityToolStripMenuItem";
            this.CourseQualityToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.CourseQualityToolStripMenuItem.Text = "课程视频质量";
            // 
            // HDToolStripMenuItem
            // 
            this.HDToolStripMenuItem.Checked = true;
            this.HDToolStripMenuItem.CheckOnClick = true;
            this.HDToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HDToolStripMenuItem.Name = "HDToolStripMenuItem";
            this.HDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.HDToolStripMenuItem.Text = "高清";
            // 
            // SDToolStripMenuItem
            // 
            this.SDToolStripMenuItem.CheckOnClick = true;
            this.SDToolStripMenuItem.Name = "SDToolStripMenuItem";
            this.SDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SDToolStripMenuItem.Text = "标清";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(539, 404);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.CurrentTaskTextBox);
            this.Controls.Add(this.TotalProgressBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.CancelDownloadButton);
            this.Controls.Add(this.StartDownloadButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中国大学 MOOC 下载器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
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
        private System.Windows.Forms.CheckBox DownloadAttachmentCheckBox;
        private System.Windows.Forms.Button CancelDownloadButton;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartDownloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CancelDownloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteCourseLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetCourseSavePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem FeedbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DownloadOptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DownloadVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DownloadAttachmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DownloadDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DownloadSubtitlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CourseQualityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SDToolStripMenuItem;
    }
}

