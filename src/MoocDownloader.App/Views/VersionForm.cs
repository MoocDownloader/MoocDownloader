using MoocDownloader.App.Models;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace MoocDownloader.App.Views
{
    public partial class VersionForm : Form
    {
        public VersionForm(NewVersion version)
        {
            InitializeComponent();

            CurrentVersionLabel.Text  = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            NewVersionLabel.Text      = version.Version;
            UpdateMessageTextBox.Text = version.Message;
        }

        private void DownloadNewVersionButton_Click(object sender, EventArgs e)
        {
            const string RELEASES_URL = "https://github.com/xixixixixiao/mooc-downloader/releases";

            System.Diagnostics.Process.Start(RELEASES_URL);
        }

        private void CancelDownloadButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void VersionForm_Load(object sender, EventArgs e)
        {
        }
    }
}