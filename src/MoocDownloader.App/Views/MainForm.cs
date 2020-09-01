using MoocDownloader.App.M3U8;
using MoocDownloader.App.Models;
using MoocDownloader.App.Models.MoocModels;
using MoocDownloader.App.Mooc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MoocDownloader.App.Mooc.MoocCodeCorrector;
using static MoocDownloader.App.Utilities.IOHelper;
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
        private readonly MainFormConfig _config = new MainFormConfig();

        /// <summary>
        /// the maximum number of retries when the download fails.
        /// </summary>
        private const int MAX_TIMES = 5;

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
                        Log(@"已收集到登录信息.");
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
        private async void StartDownloadButton_Click(object sender, EventArgs e)
        {
            const string courseUrl = "https://www.icourse163.org/course/ECNU-1002842004?tid=1450249442";

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
            var termId = await mooc.GetTermIdAsync();

            // 3. get Mooc term JavaScript code.
            var moocTermCode = await mooc.GetMocTermJavaScriptCodeAsync(termId);

            // 4. evaluate mooc term JavaScript code.
            moocTermCode = FixCourseBeanCode(moocTermCode);
            var moocTermJSON = EvaluateJavaScriptCode(moocTermCode, COURSE_BEAN_NAME) as string;

            // 5. deserialize moocTermJSON.
            var course = DeserializeObject<CourseModel>(moocTermJSON ?? string.Empty);

            FFmpegWorker.Instance.Start();

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
                        var chapterDir = $@"{chapterIndex + 1:00}-{FixPath(chapter.Name)}";
                        var lessonDir  = $@"{lessonIndex  + 1:00}-{FixPath(lesson.Name)}";
                        var unitPath   = Path.Combine(_config.CourseSavePath, course.CourseName, chapterDir, lessonDir);

                        var unitFileName = $@"{unitIndex + 1:00}-{FixPath(unit.Name)}";

                        if (!Directory.Exists(unitPath))
                        {
                            Directory.CreateDirectory(unitPath);
                        }

                        var unitCode = await mooc.GetUnitJavaScriptCodeAsync(
                            unit.Id, unit.ContentId, unit.TermId, unit.ContentType
                        );

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
                                var tokenJSON = await mooc.GetResourceTokenJsonAsync(
                                    $"{unit.Id}", $@"{unit.TermId}", $"{unit.ContentType}"
                                );

                                var tokenObject = JObject.Parse(tokenJSON);
                                var signature   = tokenObject["result"]?["videoSignDto"]?["signature"]?.ToString();
                                var videoJSON   = await mooc.GetVideoJsonAsync($@"{unit.ContentId}", signature);
                                var video       = DeserializeObject<VideoResponseModel>(videoJSON);
                                var courseVideo = new CourseVideoInfo
                                {
                                    SavePath      = unitPath,
                                    VideoFileName = $@"{unitFileName}.mp4",
                                    MergeListFile = $@"{unitFileName}.text"
                                };

                                // subtitles
                                foreach (var caption in video.Result.SrtCaptions)
                                {
                                    // subtitle file. E.g:
                                    //  01-第一节 Java明天 视频_zh.srt
                                    //  01-第一节 Java明天 视频_en.srt
                                    var srtName    = $@"{unitFileName}_{caption.LanguageCode}.srt";
                                    var srtContent = await mooc.DownloadSubtitleAsync(caption.Url);

                                    File.WriteAllBytes(Path.Combine(unitPath, srtName), srtContent);
                                }


                                var videoInfo = video.Result.Videos.FirstOrDefault(
                                    v => v.Quality.HasValue
                                      && (VideoQuality) v.Quality == _config.VideoQuality
                                );

                                if (videoInfo != null)
                                {
                                    var videoUrl = new Uri(videoInfo.VideoUrl); // video url.

                                    Configuration.Default.BaseUri = new Uri(
                                        $@"{videoUrl.Scheme}://{videoUrl.Host}{string.Join("", videoUrl.Segments.Take(videoUrl.Segments.Length - 1))}",
                                        UriKind.Absolute
                                    );

                                    var       m3u8List = await mooc.DownloadM3U8ListAsync(videoUrl);
                                    using var reader   = new M3UFileReader(m3u8List);
                                    var       m3u8Info = reader.Read();
                                    var       merger   = new StringBuilder();

                                    for (var i = 0; i < m3u8Info.MediaFiles.Count; i++)
                                    {
                                        var tsSavedName = $@"{unitFileName}-{i:00}.ts";
                                        merger.AppendLine($@"file '{tsSavedName}'");

                                        for (var j = 0; j < MAX_TIMES; j++)
                                        {
                                            var tsBytes = await mooc.DownloadM3U8TSAsync(m3u8Info.MediaFiles[i].Uri);

                                            if (tsBytes is null)
                                            {
                                                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, j + 1)));
                                            }
                                            else
                                            {
                                                File.WriteAllBytes(Path.Combine(unitPath, tsSavedName), tsBytes);
                                                break;
                                            }
                                        }
                                    }

                                    File.WriteAllText(
                                        Path.Combine(unitPath, $@"{courseVideo.MergeListFile}"), merger.ToString()
                                    );

                                    FFmpegWorker.Instance.Enqueue(courseVideo);
                                }
                            }
                                break;
                            case UnitType.Document: // document type. E.g pdf.
                            {
                                var documentUrl = unitResult.TextOrigUrl;
                                var fileName    = $@"{unitFileName}.pdf";

                                for (var i = 0; i < MAX_TIMES; i++)
                                {
                                    var document = await mooc.DownloadDocumentAsync(documentUrl);

                                    if (document is null)
                                    {
                                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i + 1)));
                                    }
                                    else
                                    {
                                        File.WriteAllBytes(Path.Combine(unitPath, fileName), document);
                                        break;
                                    }
                                }
                            }
                                break;
                            case UnitType.Attachment: // attachment type. E.g source code.
                            {
                                const string attachmentBaseUrl = "https://www.icourse163.org/course/attachment.htm";

                                var content       = JObject.Parse(unit.JsonContent);
                                var nosKey        = content["nosKey"]?.ToString();
                                var fileName      = content["fileName"]?.ToString();
                                var attachmentUrl = $@"{attachmentBaseUrl}?fileName={fileName}&nosKey={nosKey}";

                                for (var i = 0; i < MAX_TIMES; i++)
                                {
                                    var attachment = await mooc.DownloadAttachmentAsync(attachmentUrl);

                                    if (attachment is null)
                                    {
                                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i + 1)));
                                    }
                                    else
                                    {
                                        File.WriteAllBytes(
                                            Path.Combine(unitPath, $@"{unitFileName}-{FixPath(fileName)}"), attachment);

                                        break;
                                    }
                                }
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