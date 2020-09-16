using MoocDownloader.App.Models;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace MoocDownloader.App.Views
{
    public partial class VersionForm : Form
    {
        private readonly NewVersion _version;

        public VersionForm(NewVersion version)
        {
            InitializeComponent();

            _version = version;
        }

        private void DownloadNewVersionButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(_version.Url);
        }

        private void CancelDownloadButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void VersionForm_Load(object sender, EventArgs e)
        {
            CurrentVersionLabel.Text  = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            NewVersionLabel.Text      = _version.Version;
            UpdateMessageTextBox.Text = _version.Message;
        }
    }
}