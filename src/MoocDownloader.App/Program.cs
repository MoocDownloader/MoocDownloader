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
        /// gecko kernel.
        /// </summary>
        public const string FIREFOX_PATH = "Firefox";

        public const string FFMPEG = @".\FFMPEG\ffmpeg.exe";

        /// <summary>
        /// gecko kernel temp path.
        /// </summary>
        public const string FIREFOX_TEMP = "temp";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var profilePath = Path.Combine(Application.StartupPath, FIREFOX_TEMP);

            if (!Directory.Exists(profilePath))
            {
                Directory.CreateDirectory(profilePath);
            }
            else
            {
                DeleteFiles(profilePath);
            }

            Xpcom.ProfileDirectory = profilePath;

            try // Check if the Firefox component exists.
            {
                Xpcom.Initialize(FIREFOX_PATH);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    @"程序组件 ""FIREFOX"" 缺失, 请重新下载.", @"中国大学 MOOC 下载器", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return;
            }

            if (!File.Exists(FFMPEG))
            {
                MessageBox.Show(
                    @"程序组件 ""FFMPEG"" 缺失, 请重新下载.", @"中国大学 MOOC 下载器", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Delete ProfilePath Files
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void DeleteFiles(string path)
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

                if (Path.Combine(Application.StartupPath, FIREFOX_TEMP) != path)
                {
                    Directory.Delete(path);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}