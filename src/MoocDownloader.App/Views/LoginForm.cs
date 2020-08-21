using Gecko;
using MoocDownloader.App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MoocDownloader.App.Views
{
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Login icourse163 cookies.
        /// </summary>
        public List<CookieModel> Cookies { get; set; }

        private readonly string[] CookieKeys = {"S_INFO", "P_INFO", "STUDY_INFO", "STUDY_SESS", "STUDY_PERSIST"};


        public LoginForm(List<CookieModel> cookieModels)
        {
            Cookies = cookieModels;

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
                var cookie = new CookieModel
                {
                    CreationTime = cookies.Current?.CreationTime,
                    Expiry       = cookies.Current?.Expiry,
                    Host         = cookies.Current?.Host,
                    IsDomain     = cookies.Current?.IsDomain,
                    IsHttpOnly   = cookies.Current?.IsHttpOnly,
                    IsSecure     = cookies.Current?.IsSecure,
                    IsSession    = cookies.Current?.IsSession,
                    LastAccessed = cookies.Current?.LastAccessed,
                    Name         = cookies.Current?.Name,
                    Path         = cookies.Current?.Path,
                    RawHost      = cookies.Current?.RawHost,
                    Value        = cookies.Current?.Value,
                };

                if (Cookies.All(c => c.Name != cookie.Name))
                {
                    Cookies.Add(cookie);
                }
            }

            // Close the current window when the cookies is obtained.
            if (CookieKeys.All(key => Cookies.Exists(c => c.Name == key)))
            {
                Close();
                DialogResult = DialogResult.OK;
            }
        }
    }
}