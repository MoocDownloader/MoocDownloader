using Gecko;
using MoocDownloader.App.Views;
using Serilog;
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

        public const string FFMPEG_EXE = @".\ffmpeg\ffmpeg.exe";

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
#if DEBUG
                // do not delete temp files in DEBUG mode.
#else
                EmptyFolder(profilePath);
#endif
            }

#if DEBUG
            Log.Logger = new LoggerConfiguration() // Logger.
                        .MinimumLevel.Verbose()
                        .WriteTo.File(@".\log\mooc.log", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
#else
            Log.Logger = new LoggerConfiguration() // Logger.
                        .MinimumLevel.Information()
                        .WriteTo.File(@".\log\mooc.log", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
#endif

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

            if (!File.Exists(FFMPEG_EXE))
            {
                MessageBox.Show(
                    @"程序组件 ""FFMPEG"" 缺失, 请重新下载.", @"中国大学 MOOC 下载器", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return;
            }

            Log.Information("=========程序启动=========");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception exception)
            {
                Log.Error(exception, $@"程序运行发生错误: {exception.Message}");
                MessageBox.Show($@"程序运行发生错误: {exception.Message}");
            }
        }

        /// <summary>
        /// Attempt to empty the folder. Return false if it fails (locked files...).
        /// </summary>
        /// <param name="pathName">target path.</param>
        /// <returns>true on success</returns>
        public static bool EmptyFolder(string pathName)
        {
            var errors = false;
            var dir    = new DirectoryInfo(pathName);

            foreach (var fi in dir.EnumerateFiles())
            {
                try
                {
                    fi.IsReadOnly = false;
                    fi.Delete();

                    //Wait for the item to disapear (avoid 'dir not empty' error).
                    while (fi.Exists)
                    {
                        System.Threading.Thread.Sleep(10);
                        fi.Refresh();
                    }
                }
                catch (IOException)
                {
                    errors = true;
                }
            }

            foreach (var di in dir.EnumerateDirectories())
            {
                try
                {
                    EmptyFolder(di.FullName);
                    di.Delete();

                    //Wait for the item to disapear (avoid 'dir not empty' error).
                    while (di.Exists)
                    {
                        System.Threading.Thread.Sleep(10);
                        di.Refresh();
                    }
                }
                catch (IOException)
                {
                    errors = true;
                }
            }

            return !errors;
        }
    }
}
