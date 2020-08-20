using Gecko;
using System;
using System.Windows.Forms;

namespace MoocDownloader.App.Views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            MoocWebBrowser.Navigate("https://www.icourse163.org/");

            MoocWebBrowser.DocumentCompleted                        += MoocWebBrowser_DocumentCompleted;
            Gecko.CertOverrideService.GetService().ValidityOverride += LoginForm_ValidityOverride;
        }

        private void LoginForm_ValidityOverride(object sender, Gecko.Events.CertOverrideEventArgs e)
        {
            e.OverrideResult = Gecko.CertOverride.Mismatch | Gecko.CertOverride.Time | Gecko.CertOverride.Untrusted;
            e.Temporary      = true;
            e.Handled        = true;
        }

        private void MoocWebBrowser_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            var cookies = CookieManager.GetCookiesFromHost(MoocWebBrowser.Url.Host);

            while (cookies.MoveNext())
            {
                Console.WriteLine($@"{cookies.Current?.Name}={cookies.Current?.Value}");
            }
        }
    }
}