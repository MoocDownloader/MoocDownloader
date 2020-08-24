using System;
using MoocDownloader.App.Models;
using MoocDownloader.App.Mooc;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using static MoocDownloader.App.Mooc.MoocCodeCorrector;
using static MoocDownloader.App.Utilities.JavaScriptHelper;
using static Newtonsoft.Json.JsonConvert;

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
            moocTermCode = FixCourseBeanCode(moocTermCode);
            var moocTermJSON = EvaluateJavaScriptCode(moocTermCode, COURSE_BEAN_NAME) as string;

            // 5. deserialize moocTermJSON.
            var course = DeserializeObject<CourseModel>(moocTermJSON ?? string.Empty);

            foreach (var chapter in course.Chapters)
            {
                foreach (var lesson in chapter.Lessons)
                {
                    foreach (var unit in lesson.Units)
                    {
                        var unitCode = mooc.GetUnitJavaScriptCode(unit.ContentId, unit.Id, unit.ContentType);

                        if (unitCode.Contains("dwr.engine._remoteHandleException"))
                        {
                            Console.WriteLine(@"Error: system error.");
                            break;
                        }

                        unitCode = FixCourseBeanCode(unitCode);

                        var unitJSON   = EvaluateJavaScriptCode(unitCode, COURSE_BEAN_NAME) as string;
                        var unitResult = DeserializeObject<UnitResultModel>(unitJSON ?? string.Empty);

                        // Parse video / document / attachment link.
                        var unitType = (UnitType) (unit.ContentType ?? 0);

                        switch (unitType)
                        {
                            case UnitType.Other: // type is null.
                                break;
                            case UnitType.Video: // video type.
                            {
                                // 1. get access token.
                                var tokenJSON =
                                    mooc.GetResourceTokenJSON($"{unit.Id}", $@"{unit.TermId}", $"{unit.ContentType}");

                                var tokenObject = JObject.Parse(tokenJSON);
                                var signature   = tokenObject["result"]?["videoSignDto"]?["signature"]?.ToString();
                                var videoJSON   = mooc.GetVideoJSON($@"{unit.ContentId}", signature);
                            }
                                break;
                            case UnitType.Document: // document type. E.g pdf.
                            {
                                var documentUrl = unitResult.TextOrigUrl;
                                var fileName    = $@"{unit.Name}.pdf";
                            }
                                break;
                            case UnitType.Attachment: // attachment type. E.g source code.
                            {
                                const string attachmentBaseUrl = "https://www.icourse163.org/course/attachment.htm";

                                var content    = JObject.Parse(unit.JsonContent);
                                var nosKey     = content["nosKey"]?.ToString();
                                var fileName   = content["fileName"]?.ToString();
                                var attachment = $@"{attachmentBaseUrl}?fileName={fileName}&nosKey={nosKey}";
                            }
                                break;
                            default: // not recognized type
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
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