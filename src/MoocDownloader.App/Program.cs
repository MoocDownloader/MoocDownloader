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
            else
            {
                // DeleteFiles(profilePath);
            }

            Xpcom.ProfileDirectory = profilePath;
            Xpcom.Initialize("Firefox64");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Delete ProfilePath Files
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteFiles(string path)
        {
            var dir = new DirectoryInfo(path);
            try
            {
                foreach (var item in dir.GetFiles())
                {
                    File.Delete(item.FullName);
                }

                if (dir.GetDirectories().Length != 0)
                {
                    foreach (var item in dir.GetDirectories())
                    {
                        DeleteFiles(item.FullName);
                    }
                }

                if (Path.Combine(Application.StartupPath, "Temp") != path)
                {
                    Directory.Delete(path);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}