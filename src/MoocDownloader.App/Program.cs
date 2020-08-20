using Gecko;
using System;
using System.Windows.Forms;
using MoocDownloader.App.Views;

namespace MoocDownloader.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Xpcom.Initialize("Firefox64");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}