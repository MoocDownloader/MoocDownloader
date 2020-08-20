using System.Windows.Forms;

namespace MoocDownloader.App.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Login icourse163.org.
        /// </summary>
        private void LoginMoocButton_Click(object sender, System.EventArgs e)
        {
            var form = new LoginForm();

            form.ShowDialog();
        }

        /// <summary>
        /// Find save file path.
        /// </summary>
        private void FindPathButton_Click(object sender, System.EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                SavePathTextBox.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Write log.
        /// </summary>
        /// <param name="message">log message.</param>
        private void Log(string message)
        {
            RunningLogListBox.Items.Add(message);
        }
    }
}