using MoocDownloader.App.Models;
using MoocDownloader.App.Mooc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
        private void LoginMoocButton_Click(object sender, EventArgs e)
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
        private void FindPathButton_Click(object sender, EventArgs e)
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
            RunningLogListBox.Items.Add($"{DateTime.Now:hh:mm:ss} {message}");
        }

        /// <summary>
        /// Start download.
        /// </summary>
        private void StartDownloadButton_Click(object sender, EventArgs e)
        {
            const string courseUrl = "https://www.icourse163.org/course/ECNU-1002842004";

            if (!_config.IsDownloadDocument
             && !_config.IsDownloadVideo
             && !_config.IsDownloadSubtitle
             && !_config.IsDownloadAttachment) // checked at least one of them
            {
                MessageBox.Show(@"至少勾选下载视频, 文档, 字幕, 附件其中一种类型.", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Log($@"课程将会下载到文件夹: {_config.CourseSavePath}");

            if (!Directory.Exists(_config.CourseSavePath))
            {
                Log($@"路径: {_config.CourseSavePath} 不存在, 准备创建.");

                try
                {
                    Directory.CreateDirectory(_config.CourseSavePath);
                    Log($@"路径: {_config.CourseSavePath} 创建成功.");
                }
                catch (Exception exception)
                {
                    Log($@"路径: {_config.CourseSavePath} 创建失败, 原因: {exception.Message}.");
                    return;
                }
            }

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

            for (var chapterIndex = 0; chapterIndex < course.Chapters.Count; chapterIndex++)
            {
                var chapter = course.Chapters[chapterIndex];

                for (var lessonIndex = 0; lessonIndex < chapter.Lessons.Count; lessonIndex++)
                {
                    var lesson = chapter.Lessons[lessonIndex];

                    for (var unitIndex = 0; unitIndex < lesson.Units.Count; unitIndex++)
                    {
                        var unit = lesson.Units[unitIndex];

                        // create unit save path.
                        var chapterDir = $@"{chapterIndex + 1:00}-{chapter.Name}";
                        var lessonDir  = $@"{lessonIndex  + 1:00}-{lesson.Name}";
                        var unitPath   = Path.Combine(_config.CourseSavePath, chapterDir, lessonDir);

                        var unitFileName = $@"{unitIndex + 1:00}-{unit.Name}";

                        if (!Directory.Exists(unitPath))
                        {
                            Directory.CreateDirectory(unitPath);
                        }

                        var unitCode =
                            mooc.GetUnitJavaScriptCode(unit.Id, unit.ContentId, unit.TermId, unit.ContentType);

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
                                // get access token.
                                var tokenJSON =
                                    mooc.GetResourceTokenJSON($"{unit.Id}", $@"{unit.TermId}", $"{unit.ContentType}");

                                var tokenObject = JObject.Parse(tokenJSON);
                                var signature   = tokenObject["result"]?["videoSignDto"]?["signature"]?.ToString();
                                var videoJSON   = mooc.GetVideoJSON($@"{unit.ContentId}", signature);
                                var video       = DeserializeObject<VideoResponseModel>(videoJSON);

                                // subtitles
                                foreach (var caption in video.Result.SrtCaptions)
                                {
                                    // subtitle file. E.g:
                                    //  01-第一节 Java明天 视频_zh.srt
                                    //  01-第一节 Java明天 视频_en.srt
                                    var srt = $@"{unitFileName}_{caption.Name}";
                                }

                                var videoUrl  = ""; // video url.
                                var videoSize = 0L; // video size.

                                foreach (var videoInfo in video.Result.Videos)
                                {
                                    if (videoInfo.Quality.HasValue &&
                                        (VideoQuality) videoInfo.Quality == _config.VideoQuality)
                                    {
                                        videoUrl  = videoInfo.VideoUrl;
                                        videoSize = videoInfo.Size ?? 0;

                                        break;
                                    }
                                }

                                // TODO download video.
                            }
                                break;
                            case UnitType.Document: // document type. E.g pdf.
                            {
                                var documentUrl = unitResult.TextOrigUrl;
                                var fileName    = $@"{unit.Name}.pdf";

                                // TODO download document.
                            }
                                break;
                            case UnitType.Attachment: // attachment type. E.g source code.
                            {
                                const string attachmentBaseUrl = "https://www.icourse163.org/course/attachment.htm";

                                var content    = JObject.Parse(unit.JsonContent);
                                var nosKey     = content["nosKey"]?.ToString();
                                var fileName   = content["fileName"]?.ToString();
                                var attachment = $@"{attachmentBaseUrl}?fileName={fileName}&nosKey={nosKey}";

                                // TODO download attachment.
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

        private void CourseUrlTextBox_TextChanged(object sender, EventArgs e)
        {
            _config.CourseUrl = CourseUrlTextBox.Text;
        }

        private void SavePathTextBox_TextChanged(object sender, EventArgs e)
        {
            _config.CourseSavePath = SavePathTextBox.Text;
        }

        private void DownloadVideoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _config.IsDownloadVideo = DownloadVideoCheckBox.Checked;
        }

        private void DownloadDocumentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _config.IsDownloadDocument = DownloadDocumentCheckBox.Checked;
        }

        private void DownloadSubtitleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _config.IsDownloadSubtitle = DownloadSubtitleCheckBox.Checked;
        }

        private void DownloadAttachmentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _config.IsDownloadAttachment = DownloadAttachmentCheckBox.Checked;
        }

        private void SDRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SDRadioButton.Checked)
            {
                _config.VideoQuality = VideoQuality.SD;
            }
        }

        private void HDRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (HDRadioButton.Checked)
            {
                _config.VideoQuality = VideoQuality.HD;
            }
        }

        #endregion

        /// <summary>
        /// When the program is started
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // set the course save path.
            SavePathTextBox.Text = Path.Combine(Application.StartupPath, "课程下载");
        }
    }
}