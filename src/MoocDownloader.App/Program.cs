using Gecko;
using MoocDownloader.App.Views;
using System;
using System.IO;
using System.Windows.Forms;

namespace MoocDownloader.App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var profilePath = Path.Combine(Application.StartupPath, "Temp");

            if (!Directory.Exists(profilePath))
            {
                Directory.CreateDirectory(profilePath);
            }

            Xpcom.ProfileDirectory = profilePath;
            Xpcom.Initialize("Firefox64");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}