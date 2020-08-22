using MoocDownloader.App.Models;
using MoocDownloader.App.Mooc;
using MoocDownloader.App.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MoocDownloader.App.Views
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Cookies of after logging in to icourse163.org
        /// </summary>
        private readonly List<CookieModel> _cookies = new List<CookieModel>();

        /// <summary>
        /// configuration of main form.
        /// </summary>
        private MainFormConfig _config = new MainFormConfig();

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Login icourse163.org.
        /// </summary>
        private void LoginMoocButton_Click(object sender, System.EventArgs e)
        {
            var form   = new LoginForm(_cookies);
            var result = form.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:
                {
                    if (_cookies.Any())
                    {
                        Log($@"已收集到登录信息.");
                    }

                    break;
                }
                case DialogResult.Cancel:
                    Log(@"已取消登录.");
                    break;
            }
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
                Log($@"已设置保存路径为 {SavePathTextBox.Text}.");
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

        /// <summary>
        /// Start download.
        /// </summary>
        private void StartDownloadButton_Click(object sender, System.EventArgs e)
        {
            const string courseUrl = "https://www.icourse163.org/course/ECNU-1002842004";


            // 1. initializes a mooc request.
            var mooc = new MoocRequest(_cookies, courseUrl);

            // 2. get term id.
            var termId = mooc.GetTermId();

            // 3. get Mooc term JavaScript code.
            var moocTermCode = mooc.GetMocTermJavaScriptCode(termId);

            // 4. evaluate mooc term JavaScript code.
            moocTermCode = MoocCodeCorrector.FixMoocTermCode(moocTermCode);
            var moocTermJSON = JavaScriptHelper.EvaluateJavaScriptCode(moocTermCode, "lessonJSON") as string;

            // 5. deserialize moocTermJSON.
            var course = JsonConvert.DeserializeObject<CourseModel>(moocTermJSON ?? string.Empty);
        }

        #region UI controls properties binding.

        private void CourseUrlTextBox_TextChanged(object sender, System.EventArgs e)
        {
            _config.CourseUrl = CourseUrlTextBox.Text;
        }

        private void SavePathTextBox_TextChanged(object sender, System.EventArgs e)
        {
            _config.CourseSavePath = SavePathTextBox.Text;
        }

        private void DownloadVideoCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            _config.IsDownloadVideo = DownloadVideoCheckBox.Checked;
        }

        private void DownloadDocumentCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            _config.IsDownloadDocument = DownloadDocumentCheckBox.Checked;
        }

        private void DownloadSubtitleCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            _config.IsDownloadSubtitle = DownloadSubtitleCheckBox.Checked;
        }

        private void SDRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (SDRadioButton.Checked)
            {
                _config.VideoQuality = VideoQuality.SD;
            }
        }

        private void HDRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (HDRadioButton.Checked)
            {
                _config.VideoQuality = VideoQuality.HD;
            }
        }

        #endregion
    }
}