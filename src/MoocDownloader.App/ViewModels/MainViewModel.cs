using MoocDownloader.App.M3U8;
using MoocDownloader.App.Models;
using MoocDownloader.App.Models.MoocModels;
using MoocDownloader.App.Mooc;
using MoocDownloader.App.Views;
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

namespace MoocDownloader.App.ViewModels
{
    /// <summary>
    /// view model of main form.
    /// </summary>
    public class MainViewModel
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

        private bool _isCancel;

        /// <summary>
        /// Write log.
        /// </summary>
        public Action<string> Log;

        public Action<string> SetStatus;
        public Action<bool>   SetUIStatus;
        public Action<int>    UpdateCurrentBar;
        public Action<int>    UpdateTotalBar;
        public Action         ResetCurrentBar;
        public Action         ResetTotalBar;

        /// <summary>
        /// Login mooc.
        /// </summary>
        public void LoginMooc()
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

        private int CalculatePercentage(int current, int max)
        {
            var rate  = Convert.ToDouble(current) / Convert.ToDouble(max) * 100D;
            var value = Convert.ToInt32(Math.Ceiling(rate));

            if (value > 100)
            {
                return 100;
            }

            return value;
        }

        /// <summary>
        /// start download.
        /// </summary>
        public async void StartDownload()
        {
            if (!_cookies.Any())
            {
                MessageBox.Show(@"未登录中国大学 MOOC.", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(_config.CourseUrl))
            {
                MessageBox.Show(@"课程链接未输入.", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Uri.TryCreate(_config.CourseUrl, UriKind.RelativeOrAbsolute, out _))
            {
                var builder = new StringBuilder();

                builder.AppendLine("课程链接无法解析:");
                builder.AppendLine(_config.CourseUrl);
                builder.AppendLine("请检查链接.");

                MessageBox.Show(
                    builder.ToString(), @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }

            var courseUrl = _config.CourseUrl;

            if (!_config.IsDownloadDocument
             && !_config.IsDownloadVideo
             && !_config.IsDownloadSubtitle
             && !_config.IsDownloadAttachment) // checked at least one of them
            {
                MessageBox.Show(@"至少勾选下载视频, 文档, 字幕, 附件其中一种类型.", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (MessageBox.Show(@"开始下载?", @"提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
             == DialogResult.Cancel)
            {
                Log("取消下载.");
                return;
            }

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

            SetStatus("准备下载");
            Log($@"课程将会下载到文件夹: {_config.CourseSavePath}");
            SetUIStatus(false);
            ResetCurrentBar();
            ResetTotalBar();

            // 1. initializes a mooc request.
            var mooc = new MoocRequest(_cookies, courseUrl);

            // 2. get term id.
            string termId;

            try
            {
                termId = await mooc.GetTermIdAsync();

                if (string.IsNullOrEmpty(termId))
                {
                    Log("获取课程 ID 失败, 请检查课程是否开课.");
                    SetUIStatus(true);
                    return;
                }
            }
            catch (Exception exception)
            {
                Log($"获取课程 ID 失败, 原因: {exception.Message}");
                SetUIStatus(true);
                return;
            }

            SetStatus("正在下载");
            Log($@"提取到课程 ID 是 {termId}");

            // 3. get Mooc term JavaScript code.
            string moocTermCode;
            try
            {
                moocTermCode = await mooc.GetMocTermJavaScriptCodeAsync(termId);

                if (string.IsNullOrEmpty(moocTermCode))
                {
                    Log("获取课程信息失败, 请检查课程是否开课.");
                    SetUIStatus(true);
                    return;
                }
            }
            catch (Exception exception)
            {
                Log($"获取课程信息失败, 原因: {exception.Message}");
                SetUIStatus(true);

                return;
            }

            // 4. evaluate mooc term JavaScript code.
            moocTermCode = FixCourseBeanCode(moocTermCode);

            string moocTermJson;
            try
            {
                moocTermJson = EvaluateJavaScriptCode(moocTermCode, COURSE_BEAN_NAME) as string;

                if (string.IsNullOrEmpty(moocTermJson))
                {
                    Log("未提取到课程数据, 请检查课程是否开课.");
                    SetUIStatus(true);

                    return;
                }
            }
            catch (Exception exception)
            {
                Log($"提取课程数据失败, 原因: {exception.Message}");
                SetUIStatus(true);

                return;
            }

            // 5. deserialize moocTermJSON.
            CourseModel course;

            try
            {
                course = DeserializeObject<CourseModel>(moocTermJson);

                if (course is null || course.Chapters?.Count == 0)
                {
                    Log("未提取到课程数据, 请检查课程是否开课.");
                    SetUIStatus(true);

                    return;
                }

                Log($@"准备开始下载课程: {course.CourseName}");
            }
            catch (Exception exception)
            {
                Log($"计算课程数据时发生错误, 原因: {exception.Message}");
                SetUIStatus(true);
                return;
            }

            try
            {
                FFmpegWorker.Instance.Start();
            }
            catch (Exception exception)
            {
                Log($"视频合并功能启动失败, 原因: {exception.Message}");
                SetUIStatus(true);
                return;
            }

            for (var chapterIndex = 0; chapterIndex < course.Chapters.Count && !_isCancel; chapterIndex++)
            {
                if (chapterIndex >= course.Chapters.Count)
                {
                    break;
                }

                var chapter = course.Chapters[chapterIndex];

                for (var lessonIndex = 0; lessonIndex < chapter.Lessons.Count && !_isCancel; lessonIndex++)
                {
                    if (lessonIndex >= chapter.Lessons.Count)
                    {
                        break;
                    }

                    var lesson = chapter.Lessons[lessonIndex];

                    for (var unitIndex = 0; unitIndex < lesson.Units.Count && !_isCancel; unitIndex++)
                    {
                        if (unitIndex >= lesson.Units.Count)
                        {
                            break;
                        }

                        // update total progress bar.
                        var totalMax     = course.Chapters.Count + chapter.Lessons.Count + lesson.Units.Count;
                        var totalCurrent = (chapterIndex + 1) + (lessonIndex + 1)        + (unitIndex + 1);

                        UpdateTotalBar(CalculatePercentage(totalCurrent, totalMax));

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

                        var unitJson   = EvaluateJavaScriptCode(unitCode, COURSE_BEAN_NAME) as string;
                        var unitResult = DeserializeObject<UnitResultModel>(unitJson ?? string.Empty);

                        // Parse video / document / attachment link.
                        var unitType = (UnitType) (unit.ContentType ?? 0);

                        switch (unitType)
                        {
                            case UnitType.Other: // type is null.
                                break;
                            case UnitType.Video: // video type.
                            {
                                if (!_config.IsDownloadVideo)
                                {
                                    break;
                                }

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

                                Log($@"下载视频: {unitFileName}");

                                // subtitles
                                if (_config.IsDownloadSubtitle)
                                {
                                    foreach (var caption in video.Result.SrtCaptions)
                                    {
                                        // subtitle file. E.g:
                                        //  01-第一节 Java明天 视频.zh.srt
                                        //  01-第一节 Java明天 视频.en.srt
                                        var srtName = $@"{unitFileName}.{caption.LanguageCode}.srt";

                                        try
                                        {
                                            var srtContent = await mooc.DownloadSubtitleAsync(caption.Url);

                                            if (srtContent is null)
                                            {
                                                Log($"字幕 {srtName} 下载失败.");

                                                break;
                                            }

                                            File.WriteAllBytes(Path.Combine(unitPath, srtName), srtContent);
                                        }
                                        catch (Exception exception)
                                        {
                                            Log($"字幕 {srtName} 下载失败, 原因: {exception.Message}");

                                            break;
                                        }
                                    }
                                }

                                var videoInfo = video.Result.Videos.FirstOrDefault(
                                    v => v.Quality.HasValue
                                      && (VideoQuality) v.Quality == _config.VideoQuality
                                );

                                if (videoInfo != null && !string.IsNullOrEmpty(videoInfo.Format))
                                {
                                    if (videoInfo.Format.ToLower() == "hls") // m3u8 format.
                                    {
                                        var videoUrl = new Uri(videoInfo.VideoUrl); // video url.

                                        var baseUrl = $@"{videoUrl.Scheme}://{videoUrl.Host}" +
                                                      string.Join(
                                                          "", videoUrl.Segments.Take(videoUrl.Segments.Length - 1));

                                        Configuration.Default.BaseUri = new Uri(baseUrl, UriKind.Absolute);

                                        string m3u8List;

                                        try
                                        {
                                            m3u8List = await mooc.DownloadM3U8ListAsync(videoUrl);

                                            if (string.IsNullOrEmpty(m3u8List))
                                            {
                                                Log($"下载课程 {unitFileName} 的视频列表失败.");
                                                break;
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                            Log($"下载课程 {unitFileName} 的视频列表发生错误, 原因: {exception.Message}");
                                            break;
                                        }

                                        try
                                        {
                                            using var reader   = new M3UFileReader(m3u8List);
                                            var       m3u8Info = reader.Read();
                                            var       merger   = new StringBuilder();

                                            for (var i = 0; i < m3u8Info.MediaFiles.Count && !_isCancel; i++)
                                            {
                                                UpdateCurrentBar(CalculatePercentage(i + 1, m3u8Info.MediaFiles.Count));

                                                var tsSavedName          = $@"{unitFileName}-{i:00}.ts";
                                                var downloadVideoSuccess = false;

                                                for (var j = 0; j < MAX_TIMES; j++)
                                                {
                                                    try
                                                    {
                                                        var tsBytes =
                                                            await mooc.DownloadM3U8TSAsync(m3u8Info.MediaFiles[i].Uri);

                                                        if (tsBytes is null)
                                                        {
                                                            Log($"下载视频片段 {tsSavedName} 失败, 准备重试, 当前重试第 {i + 1} 次.");
                                                            await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, j)));
                                                        }
                                                        else
                                                        {
                                                            File.WriteAllBytes(
                                                                Path.Combine(unitPath, tsSavedName), tsBytes);
                                                            merger.AppendLine(
                                                                $@"file '{Path.Combine(unitPath, tsSavedName)}'"
                                                            ); // combine ts file path and add to list.

                                                            courseVideo.TSFiles.Add(tsSavedName);
                                                            downloadVideoSuccess = true;
                                                            break;
                                                        }
                                                    }
                                                    catch (Exception exception)
                                                    {
                                                        Log(
                                                            $"下载视频片段 {tsSavedName} 发生异常, 原因: {exception.Message}, 准备重试, 当前重试第 {i + 1} 次."
                                                        );
                                                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, j)));
                                                    }
                                                }

                                                if (!downloadVideoSuccess)
                                                {
                                                    Log($"下载视频片段 {tsSavedName} 失败, 已跳过.");
                                                }
                                            }

                                            Log($@"课程 {unitFileName} 已下载完成.");

                                            File.WriteAllText(
                                                Path.Combine(unitPath, $@"{courseVideo.MergeListFile}"),
                                                merger.ToString()
                                            );

                                            FFmpegWorker.Instance.Enqueue(courseVideo);
                                        }
                                        catch (Exception exception)
                                        {
                                            Log($"下载课程 {unitFileName} 的视频发生错误, 原因: {exception.Message}");
                                        }
                                    }
                                    else if (videoInfo.Format.ToLower() == "mp4")
                                    {
                                        var mp4Url = string.Empty;

                                        switch (_config.VideoQuality)
                                        {
                                            case VideoQuality.SD:
                                                mp4Url = unitResult.VideoVo.Mp4SdUrl;
                                                break;
                                            case VideoQuality.HD:
                                                mp4Url = unitResult.VideoVo.Mp4HdUrl;
                                                break;
                                            case VideoQuality.UHD:
                                                mp4Url = unitResult.VideoVo.Mp4ShdUrl;
                                                break;
                                        }

                                        if (string.IsNullOrEmpty(mp4Url))
                                        {
                                            mp4Url = videoInfo.VideoUrl;
                                        }

                                        for (var i = 0; i < MAX_TIMES; i++)
                                        {
                                            try
                                            {
                                                var videoBytes = await mooc.DownloadVideoAsync(mp4Url);

                                                if (videoBytes is null)
                                                {
                                                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                                    Log($"下载课程视频 {unitFileName} 失败, 准备重试, 当前重试第 {i + 1} 次.");
                                                }
                                                else
                                                {
                                                    File.WriteAllBytes(
                                                        Path.Combine(unitPath, $"{unitFileName}.mp4"), videoBytes
                                                    );

                                                    Log($@"课程 {unitFileName} 已下载完成.");
                                                    break;
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                                Log($"下载课程 {unitFileName} 的视频发生错误, 原因: {exception.Message}");
                                            }
                                        }
                                    }
                                    else if (videoInfo.Format.ToLower() == "flv")
                                    {
                                        var flvUrl = string.Empty;

                                        switch (_config.VideoQuality)
                                        {
                                            case VideoQuality.SD:
                                                flvUrl = unitResult.VideoVo.FlvSdUrl;
                                                break;
                                            case VideoQuality.HD:
                                                flvUrl = unitResult.VideoVo.FlvHdUrl;
                                                break;
                                            case VideoQuality.UHD:
                                                flvUrl = unitResult.VideoVo.FlvShdUrl;
                                                break;
                                        }

                                        if (string.IsNullOrEmpty(flvUrl))
                                        {
                                            flvUrl = videoInfo.VideoUrl;
                                        }

                                        for (var i = 0; i < MAX_TIMES; i++)
                                        {
                                            try
                                            {
                                                var videoBytes = await mooc.DownloadVideoAsync(flvUrl);

                                                if (videoBytes is null)
                                                {
                                                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                                    Log($"下载课程视频 {unitFileName} 失败, 准备重试, 当前重试第 {i + 1} 次.");
                                                }
                                                else
                                                {
                                                    File.WriteAllBytes(
                                                        Path.Combine(unitPath, $"{unitFileName}.flv"), videoBytes
                                                    );

                                                    Log($@"课程 {unitFileName} 已下载完成.");
                                                    break;
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                                Log($"下载课程 {unitFileName} 的视频发生错误, 原因: {exception.Message}");
                                            }
                                        }
                                    }
                                }
                            }
                                break;
                            case UnitType.Document: // document type. E.g pdf.
                            {
                                if (!_config.IsDownloadDocument)
                                {
                                    break;
                                }

                                if (string.IsNullOrEmpty(unitResult.TextOrigUrl))
                                {
                                    Log("文档: unitFileName 下载链接为空, 跳过下载.");
                                    break;
                                }

                                var documentUrl        = unitResult.TextOrigUrl;
                                var fileName           = $@"{unitFileName}.pdf";
                                var downloadDocSuccess = false;

                                Log($@"准备下载文档: {fileName}");

                                for (var i = 0; i < MAX_TIMES; i++)
                                {
                                    try
                                    {
                                        var document = await mooc.DownloadDocumentAsync(documentUrl);

                                        if (document is null)
                                        {
                                            Log($"下载文档 {fileName} 失败, 准备重试, 当前重试第 {i + 1} 次.");
                                            await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                        }
                                        else
                                        {
                                            File.WriteAllBytes(Path.Combine(unitPath, fileName), document);
                                            downloadDocSuccess = true;
                                            Log($@"文档 {fileName} 已下载完成.");
                                            break;
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        Log($@"下载文档 {fileName} 发生错误, 原因: {exception.Message}, 准备重试, 当前重试第 {i + 1} 次.");
                                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                    }
                                }

                                if (!downloadDocSuccess)
                                {
                                    Log($"下载文档 {fileName} 失败, 已跳过.");
                                }
                            }
                                break;
                            case UnitType.Attachment: // attachment type. E.g source code.
                            {
                                if (!_config.IsDownloadAttachment)
                                {
                                    break;
                                }

                                const string attachmentBaseUrl = "https://www.icourse163.org/course/attachment.htm";

                                if (string.IsNullOrEmpty(unit.JsonContent))
                                {
                                    Log($"附件 {unit?.Name} 下载链接为空, 跳过下载.");
                                    break;
                                }

                                var content            = JObject.Parse(unit.JsonContent);
                                var nosKey             = content["nosKey"]?.ToString();
                                var fileName           = content["fileName"]?.ToString();
                                var attachmentUrl      = $@"{attachmentBaseUrl}?fileName={fileName}&nosKey={nosKey}";
                                var downloadAttSuccess = false;

                                Log($@"准备下载附件: {fileName}");

                                for (var i = 0; i < MAX_TIMES; i++)
                                {
                                    try
                                    {
                                        var attachment = await mooc.DownloadAttachmentAsync(attachmentUrl);

                                        if (attachment is null)
                                        {
                                            Log($"下载附件 {fileName} 失败, 准备重试, 当前重试第 {i + 1} 次.");
                                            await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                        }
                                        else
                                        {
                                            File.WriteAllBytes(
                                                Path.Combine(unitPath, $@"{unitFileName}-{FixPath(fileName)}"),
                                                attachment
                                            );
                                            downloadAttSuccess = true;

                                            Log($@"附件 {fileName} 已下载完成.");
                                            break;
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                        Log($@"下载附件 {fileName} 发生错误, 原因: {exception.Message}, 准备重试, 当前重试第 {i + 1} 次.");
                                    }
                                }

                                if (!downloadAttSuccess)
                                {
                                    Log($"下载附件 {fileName} 失败, 已跳过.");
                                }
                            }
                                break;
                            default: // not recognized type
                                Log($"当前课程单元: {unitFileName} 类型不支持下载, 已忽略.");
                                break;
                        }
                    }
                }
            }

            SetUIStatus(true);

            if (_isCancel)
            {
                Log("已取消下载.");

                ResetCurrentBar();
                ResetTotalBar();
            }
            else
            {
                UpdateTotalBar(100);
                UpdateCurrentBar(100);
                SetStatus("下载完成");
                Log($"课程 {course.CourseName} 已下载完成!");

                MessageBox.Show(
                    $"课程 {course.CourseName} 已下载完成!", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }
        }

        /// <summary>
        /// cancel download.
        /// </summary>
        public void CancelDownload()
        {
            var result = MessageBox.Show(@"正在下载, 是否取消?", @"提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Log("准备取消下载.");
                _isCancel = true;
            }
        }

        public void SetSavePath(string path)
        {
            _config.CourseSavePath = path;
        }

        public void SetCourseUrl(string url)
        {
            _config.CourseUrl = url;
        }

        public void SetDownloadVideo(bool video)
        {
            _config.IsDownloadVideo = video;
        }

        public void SetDownloadDocument(bool doc)
        {
            _config.IsDownloadDocument = doc;
        }

        public void SetDownloadSubtitle(bool subtitle)
        {
            _config.IsDownloadSubtitle = subtitle;
        }

        public void SetDownloadAttachment(bool attachment)
        {
            _config.IsDownloadAttachment = attachment;
        }

        public void SetVideoQuality(VideoQuality quality)
        {
            _config.VideoQuality = quality;
        }
    }
}