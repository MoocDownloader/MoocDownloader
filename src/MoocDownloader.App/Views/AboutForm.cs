using System.Windows.Forms;

namespace MoocDownloader.App.Views
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// visit link of contributors.
        /// </summary>
        private void VisitLink(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel label && label.Tag is string link)
            {
                System.Diagnostics.Process.Start(link);

                Close();
            }
        }
    }
}