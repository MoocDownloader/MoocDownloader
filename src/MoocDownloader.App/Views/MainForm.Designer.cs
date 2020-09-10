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
            this.components = new System.ComponentModel.Container();
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ClearCourseUrlButton = new System.Windows.Forms.Button();
            this.CancelDownloadButton = new System.Windows.Forms.Button();
            this.MainFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteCourseLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetCourseSavePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadAttachmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadSubtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CourseQualityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UHDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FeedbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.UHDRadioButton = new System.Windows.Forms.RadioButton();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TotalStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CurrentToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AutoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.DownloadTimeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.MainFormMenuStrip.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginMoocButton
            // 
            this.LoginMoocButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LoginMoocButton.Location = new System.Drawing.Point(18, 50);
            this.LoginMoocButton.Name = "LoginMoocButton";
            this.LoginMoocButton.Size = new System.Drawing.Size(184, 25);
            this.LoginMoocButton.TabIndex = 0;
            this.LoginMoocButton.Text = "登录中国大学 MOOC";
            this.LoginMoocButton.UseVisualStyleBackColor = true;
            this.LoginMoocButton.Click += new System.EventHandler(this.LoginMoocButton_Click);
            // 
            // CourseUrlTextBox
            // 
            this.CourseUrlTextBox.Location = new System.Drawing.Point(18, 113);
            this.CourseUrlTextBox.Name = "CourseUrlTextBox";
            this.CourseUrlTextBox.Size = new System.Drawing.Size(184, 20);
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
            this.SavePathTextBox.Location = new System.Drawing.Point(18, 171);
            this.SavePathTextBox.Name = "SavePathTextBox";
            this.SavePathTextBox.Size = new System.Drawing.Size(184, 20);
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
            this.FindPathButton.Location = new System.Drawing.Point(208, 169);
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
            this.SDRadioButton.Location = new System.Drawing.Point(16, 22);
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
            this.HDRadioButton.Location = new System.Drawing.Point(71, 22);
            this.HDRadioButton.Name = "HDRadioButton";
            this.HDRadioButton.Size = new System.Drawing.Size(49, 17);
            this.HDRadioButton.TabIndex = 4;
            this.HDRadioButton.Text = "高清";
            this.HDRadioButton.UseVisualStyleBackColor = true;
            this.HDRadioButton.CheckedChanged += new System.EventHandler(this.HDRadioButton_CheckedChanged);
            // 
            // DownloadSubtitleCheckBox
            // 
            this.DownloadSubtitleCheckBox.AutoSize = true;
            this.DownloadSubtitleCheckBox.Checked = true;
            this.DownloadSubtitleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadSubtitleCheckBox.Location = new System.Drawing.Point(100, 54);
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
            this.DownloadVideoCheckBox.Location = new System.Drawing.Point(16, 24);
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
            this.DownloadDocumentCheckBox.Location = new System.Drawing.Point(16, 54);
            this.DownloadDocumentCheckBox.Name = "DownloadDocumentCheckBox";
            this.DownloadDocumentCheckBox.Size = new System.Drawing.Size(74, 17);
            this.DownloadDocumentCheckBox.TabIndex = 5;
            this.DownloadDocumentCheckBox.Text = "下载课件";
            this.DownloadDocumentCheckBox.UseVisualStyleBackColor = true;
            this.DownloadDocumentCheckBox.CheckedChanged += new System.EventHandler(this.DownloadDocumentCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.DownloadVideoCheckBox);
            this.groupBox1.Controls.Add(this.DownloadDocumentCheckBox);
            this.groupBox1.Controls.Add(this.DownloadAttachmentCheckBox);
            this.groupBox1.Controls.Add(this.DownloadSubtitleCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(282, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 86);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下载选项";
            // 
            // DownloadAttachmentCheckBox
            // 
            this.DownloadAttachmentCheckBox.AutoSize = true;
            this.DownloadAttachmentCheckBox.Checked = true;
            this.DownloadAttachmentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadAttachmentCheckBox.Location = new System.Drawing.Point(100, 24);
            this.DownloadAttachmentCheckBox.Name = "DownloadAttachmentCheckBox";
            this.DownloadAttachmentCheckBox.Size = new System.Drawing.Size(74, 17);
            this.DownloadAttachmentCheckBox.TabIndex = 5;
            this.DownloadAttachmentCheckBox.Text = "下载附件";
            this.DownloadAttachmentCheckBox.UseVisualStyleBackColor = true;
            this.DownloadAttachmentCheckBox.CheckedChanged += new System.EventHandler(this.DownloadAttachmentCheckBox_CheckedChanged);
            // 
            // StartDownloadButton
            // 
            this.StartDownloadButton.BackColor = System.Drawing.Color.YellowGreen;
            this.StartDownloadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartDownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartDownloadButton.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartDownloadButton.ForeColor = System.Drawing.Color.White;
            this.StartDownloadButton.Location = new System.Drawing.Point(282, 182);
            this.StartDownloadButton.Name = "StartDownloadButton";
            this.StartDownloadButton.Size = new System.Drawing.Size(116, 54);
            this.StartDownloadButton.TabIndex = 7;
            this.StartDownloadButton.Text = "开始下载";
            this.StartDownloadButton.UseVisualStyleBackColor = false;
            this.StartDownloadButton.Click += new System.EventHandler(this.StartDownloadButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.AutoScrollCheckBox);
            this.groupBox2.Controls.Add(this.RunningLogListBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 242);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 150);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "运行日志";
            // 
            // RunningLogListBox
            // 
            this.RunningLogListBox.BackColor = System.Drawing.Color.White;
            this.RunningLogListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RunningLogListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RunningLogListBox.FormattingEnabled = true;
            this.RunningLogListBox.HorizontalScrollbar = true;
            this.RunningLogListBox.Location = new System.Drawing.Point(3, 16);
            this.RunningLogListBox.Name = "RunningLogListBox";
            this.RunningLogListBox.Size = new System.Drawing.Size(452, 131);
            this.RunningLogListBox.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.LoginMoocButton);
            this.groupBox3.Controls.Add(this.CourseUrlTextBox);
            this.groupBox3.Controls.Add(this.SavePathTextBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.ClearCourseUrlButton);
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
            // ClearCourseUrlButton
            // 
            this.ClearCourseUrlButton.Location = new System.Drawing.Point(208, 113);
            this.ClearCourseUrlButton.Name = "ClearCourseUrlButton";
            this.ClearCourseUrlButton.Size = new System.Drawing.Size(40, 25);
            this.ClearCourseUrlButton.TabIndex = 3;
            this.ClearCourseUrlButton.Text = "清空";
            this.ClearCourseUrlButton.UseVisualStyleBackColor = true;
            this.ClearCourseUrlButton.Click += new System.EventHandler(this.ClearCourseUrlButton_Click);
            // 
            // CancelDownloadButton
            // 
            this.CancelDownloadButton.BackColor = System.Drawing.Color.DarkOrange;
            this.CancelDownloadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelDownloadButton.Enabled = false;
            this.CancelDownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelDownloadButton.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.CancelDownloadButton.ForeColor = System.Drawing.Color.White;
            this.CancelDownloadButton.Location = new System.Drawing.Point(407, 182);
            this.CancelDownloadButton.Name = "CancelDownloadButton";
            this.CancelDownloadButton.Size = new System.Drawing.Size(63, 54);
            this.CancelDownloadButton.TabIndex = 7;
            this.CancelDownloadButton.Text = "取消";
            this.CancelDownloadButton.UseVisualStyleBackColor = false;
            this.CancelDownloadButton.Click += new System.EventHandler(this.CancelDownloadButton_Click);
            // 
            // MainFormMenuStrip
            // 
            this.MainFormMenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.MainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.SettingToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MainFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainFormMenuStrip.Name = "MainFormMenuStrip";
            this.MainFormMenuStrip.Size = new System.Drawing.Size(483, 24);
            this.MainFormMenuStrip.TabIndex = 13;
            this.MainFormMenuStrip.Text = "menuStrip1";
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
            this.StartDownloadToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.StartDownloadToolStripMenuItem.Text = "开始下载";
            this.StartDownloadToolStripMenuItem.Click += new System.EventHandler(this.StartDownloadToolStripMenuItem_Click);
            // 
            // CancelDownloadToolStripMenuItem
            // 
            this.CancelDownloadToolStripMenuItem.Enabled = false;
            this.CancelDownloadToolStripMenuItem.Name = "CancelDownloadToolStripMenuItem";
            this.CancelDownloadToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.CancelDownloadToolStripMenuItem.Text = "取消下载";
            this.CancelDownloadToolStripMenuItem.Click += new System.EventHandler(this.CancelDownloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
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
            this.LoginToolStripMenuItem.Click += new System.EventHandler(this.LoginToolStripMenuItem_Click);
            // 
            // PasteCourseLinkToolStripMenuItem
            // 
            this.PasteCourseLinkToolStripMenuItem.Name = "PasteCourseLinkToolStripMenuItem";
            this.PasteCourseLinkToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.PasteCourseLinkToolStripMenuItem.Text = "粘贴课程链接";
            this.PasteCourseLinkToolStripMenuItem.Click += new System.EventHandler(this.PasteCourseLinkToolStripMenuItem_Click);
            // 
            // SetCourseSavePathToolStripMenuItem
            // 
            this.SetCourseSavePathToolStripMenuItem.Name = "SetCourseSavePathToolStripMenuItem";
            this.SetCourseSavePathToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.SetCourseSavePathToolStripMenuItem.Text = "设置课程下载保存路径";
            this.SetCourseSavePathToolStripMenuItem.Click += new System.EventHandler(this.SetCourseSavePathToolStripMenuItem_Click);
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
            this.DownloadVideoToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.DownloadVideoToolStripMenuItem.Text = "下载视频";
            this.DownloadVideoToolStripMenuItem.Click += new System.EventHandler(this.DownloadVideoToolStripMenuItem_Click);
            // 
            // DownloadAttachmentToolStripMenuItem
            // 
            this.DownloadAttachmentToolStripMenuItem.Checked = true;
            this.DownloadAttachmentToolStripMenuItem.CheckOnClick = true;
            this.DownloadAttachmentToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadAttachmentToolStripMenuItem.Name = "DownloadAttachmentToolStripMenuItem";
            this.DownloadAttachmentToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.DownloadAttachmentToolStripMenuItem.Text = "下载附件";
            this.DownloadAttachmentToolStripMenuItem.Click += new System.EventHandler(this.DownloadAttachmentToolStripMenuItem_Click);
            // 
            // DownloadDocumentToolStripMenuItem
            // 
            this.DownloadDocumentToolStripMenuItem.Checked = true;
            this.DownloadDocumentToolStripMenuItem.CheckOnClick = true;
            this.DownloadDocumentToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadDocumentToolStripMenuItem.Name = "DownloadDocumentToolStripMenuItem";
            this.DownloadDocumentToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.DownloadDocumentToolStripMenuItem.Text = "下载课件";
            this.DownloadDocumentToolStripMenuItem.Click += new System.EventHandler(this.DownloadDocumentToolStripMenuItem_Click);
            // 
            // DownloadSubtitlesToolStripMenuItem
            // 
            this.DownloadSubtitlesToolStripMenuItem.Checked = true;
            this.DownloadSubtitlesToolStripMenuItem.CheckOnClick = true;
            this.DownloadSubtitlesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadSubtitlesToolStripMenuItem.Name = "DownloadSubtitlesToolStripMenuItem";
            this.DownloadSubtitlesToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.DownloadSubtitlesToolStripMenuItem.Text = "下载字幕";
            this.DownloadSubtitlesToolStripMenuItem.Click += new System.EventHandler(this.DownloadSubtitlesToolStripMenuItem_Click);
            // 
            // CourseQualityToolStripMenuItem
            // 
            this.CourseQualityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UHDToolStripMenuItem,
            this.HDToolStripMenuItem,
            this.SDToolStripMenuItem});
            this.CourseQualityToolStripMenuItem.Name = "CourseQualityToolStripMenuItem";
            this.CourseQualityToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.CourseQualityToolStripMenuItem.Text = "课程视频质量";
            // 
            // UHDToolStripMenuItem
            // 
            this.UHDToolStripMenuItem.Checked = true;
            this.UHDToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UHDToolStripMenuItem.Name = "UHDToolStripMenuItem";
            this.UHDToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.UHDToolStripMenuItem.Text = "超清";
            this.UHDToolStripMenuItem.Click += new System.EventHandler(this.UHDToolStripMenuItem_Click);
            // 
            // HDToolStripMenuItem
            // 
            this.HDToolStripMenuItem.CheckOnClick = true;
            this.HDToolStripMenuItem.Name = "HDToolStripMenuItem";
            this.HDToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.HDToolStripMenuItem.Text = "高清";
            this.HDToolStripMenuItem.Click += new System.EventHandler(this.HDToolStripMenuItem_Click);
            // 
            // SDToolStripMenuItem
            // 
            this.SDToolStripMenuItem.CheckOnClick = true;
            this.SDToolStripMenuItem.Name = "SDToolStripMenuItem";
            this.SDToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.SDToolStripMenuItem.Text = "标清";
            this.SDToolStripMenuItem.Click += new System.EventHandler(this.SDToolStripMenuItem_Click);
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
            this.ViewHelpToolStripMenuItem.Click += new System.EventHandler(this.ViewHelpToolStripMenuItem_Click);
            // 
            // UpdateToolStripMenuItem
            // 
            this.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem";
            this.UpdateToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.UpdateToolStripMenuItem.Text = "更新程序";
            this.UpdateToolStripMenuItem.Click += new System.EventHandler(this.UpdateToolStripMenuItem_Click);
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
            this.FeedbackToolStripMenuItem.Click += new System.EventHandler(this.FeedbackToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.AboutToolStripMenuItem.Text = "关于";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.UHDRadioButton);
            this.groupBox4.Controls.Add(this.HDRadioButton);
            this.groupBox4.Controls.Add(this.SDRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(282, 121);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(188, 55);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "视频质量";
            // 
            // UHDRadioButton
            // 
            this.UHDRadioButton.AutoSize = true;
            this.UHDRadioButton.Checked = true;
            this.UHDRadioButton.Location = new System.Drawing.Point(126, 22);
            this.UHDRadioButton.Name = "UHDRadioButton";
            this.UHDRadioButton.Size = new System.Drawing.Size(49, 17);
            this.UHDRadioButton.TabIndex = 4;
            this.UHDRadioButton.TabStop = true;
            this.UHDRadioButton.Text = "超清";
            this.UHDRadioButton.UseVisualStyleBackColor = true;
            this.UHDRadioButton.CheckedChanged += new System.EventHandler(this.UHDRadioButton_CheckedChanged);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.TotalStripProgressBar,
            this.toolStripStatusLabel4,
            this.CurrentToolStripProgressBar,
            this.toolStripStatusLabel2,
            this.StatusToolStripStatusLabel,
            this.DownloadTimeToolStripStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 401);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(483, 24);
            this.MainStatusStrip.SizingGrip = false;
            this.MainStatusStrip.TabIndex = 15;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(49, 19);
            this.toolStripStatusLabel1.Text = "总进度:";
            // 
            // TotalStripProgressBar
            // 
            this.TotalStripProgressBar.Name = "TotalStripProgressBar";
            this.TotalStripProgressBar.Size = new System.Drawing.Size(100, 18);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(62, 19);
            this.toolStripStatusLabel4.Text = "当前进度:";
            // 
            // CurrentToolStripProgressBar
            // 
            this.CurrentToolStripProgressBar.Name = "CurrentToolStripProgressBar";
            this.CurrentToolStripProgressBar.Size = new System.Drawing.Size(100, 18);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(36, 19);
            this.toolStripStatusLabel2.Text = "状态:";
            // 
            // StatusToolStripStatusLabel
            // 
            this.StatusToolStripStatusLabel.Name = "StatusToolStripStatusLabel";
            this.StatusToolStripStatusLabel.Size = new System.Drawing.Size(59, 19);
            this.StatusToolStripStatusLabel.Text = "准备就绪";
            // 
            // AutoScrollCheckBox
            // 
            this.AutoScrollCheckBox.AutoSize = true;
            this.AutoScrollCheckBox.BackColor = System.Drawing.Color.White;
            this.AutoScrollCheckBox.Checked = true;
            this.AutoScrollCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoScrollCheckBox.Location = new System.Drawing.Point(63, 0);
            this.AutoScrollCheckBox.Name = "AutoScrollCheckBox";
            this.AutoScrollCheckBox.Size = new System.Drawing.Size(74, 17);
            this.AutoScrollCheckBox.TabIndex = 1;
            this.AutoScrollCheckBox.Text = "自动滚动";
            this.AutoScrollCheckBox.UseVisualStyleBackColor = false;
            // 
            // DownloadTimeToolStripStatusLabel
            // 
            this.DownloadTimeToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.DownloadTimeToolStripStatusLabel.Name = "DownloadTimeToolStripStatusLabel";
            this.DownloadTimeToolStripStatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DownloadTimeToolStripStatusLabel.Size = new System.Drawing.Size(53, 19);
            this.DownloadTimeToolStripStatusLabel.Text = "00:00:00";
            this.DownloadTimeToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DownloadTimeToolStripStatusLabel.ToolTipText = "下载时间";
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Interval = 1000;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(483, 425);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.CancelDownloadButton);
            this.Controls.Add(this.StartDownloadButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MainFormMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中国大学 MOOC 下载器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.MainFormMenuStrip.ResumeLayout(false);
            this.MainFormMenuStrip.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
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
        private System.Windows.Forms.ListBox RunningLogListBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox DownloadAttachmentCheckBox;
        private System.Windows.Forms.Button CancelDownloadButton;
        private System.Windows.Forms.MenuStrip MainFormMenuStrip;
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
        private System.Windows.Forms.ToolStripMenuItem UHDToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton UHDRadioButton;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar TotalStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel StatusToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripProgressBar CurrentToolStripProgressBar;
        private System.Windows.Forms.Button ClearCourseUrlButton;
        private System.Windows.Forms.CheckBox AutoScrollCheckBox;
        private System.Windows.Forms.ToolStripStatusLabel DownloadTimeToolStripStatusLabel;
        private System.Windows.Forms.Timer MainTimer;
    }
}

