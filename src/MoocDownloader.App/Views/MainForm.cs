using MoocDownloader.App.Views;
using System.Windows.Forms;

namespace MoocDownloader.App
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
    }
}
