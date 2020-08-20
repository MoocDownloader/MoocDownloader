using System.Windows.Forms;

namespace MoocDownloader.App.Views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            MoocWebBrowser.Navigate("https://www.icourse163.org/");
        }
    }
}