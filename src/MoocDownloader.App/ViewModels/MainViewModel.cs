using MoocDownloader.App.M3U8;
using MoocDownloader.App.Models;
using MoocDownloader.App.Models.MoocModels;
using MoocDownloader.App.Mooc;
using MoocDownloader.App.Views;
using Newtonsoft.Json.Linq;
using Serilog;
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
        public Action<string> WriteLog;

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
                        WriteLog(@"登录成功! 已收集到登录信息.");
                    }

                    break;
                }
                case DialogResult.Cancel:
                    WriteLog(@"已取消登录.");
                    break;
            }
        }

        private int CalculatePercentage(int current, int max)
        {
            var rate  = Convert.ToDouble(current) / Convert.ToDouble(max) * 100D;
            var value = Convert.ToInt32(Math.Ceiling(rate));

            return value > 100 ? 100 : value;
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
                WriteLog("取消下载.");
                return;
            }

            if (!Directory.Exists(_config.CourseSavePath))
            {
                WriteLog($@"路径: {_config.CourseSavePath} 不存在, 准备创建.");

                try
                {
                    Directory.CreateDirectory(_config.CourseSavePath);
                    WriteLog($@"路径: {_config.CourseSavePath} 创建成功.");
                }
                catch (Exception exception)
                {
                    WriteLog($@"路径: {_config.CourseSavePath} 创建失败, 原因: {exception.Message}.");
                    return;
                }
            }

            // 1. initializes a mooc request.
            MoocRequest mooc;
            try
            {
                mooc = new MoocRequest(_cookies, courseUrl);
            }
            catch (Exception exception)
            {
                WriteLog($@"登录信息有误, 需要重启软件. 原因: {exception.Message}.");
                return;
            }

            SetStatus("准备下载");

            Log.Information($@"课程将会下载到文件夹: {_config.CourseSavePath}");
            WriteLog($@"课程将会下载到文件夹: {_config.CourseSavePath}");
            SetUIStatus(false);
            ResetCurrentBar();
            ResetTotalBar();

            // 2. get term id.
            string termId;

            try
            {
                termId = await mooc.GetTermIdAsync();

                if (string.IsNullOrEmpty(termId))
                {
                    WriteLog("获取课程 ID 失败, 请检查课程是否开课.");
                    SetUIStatus(true);
                    return;
                }
            }
            catch (Exception exception)
            {
                WriteLog($"获取课程 ID 失败, 原因: {exception.Message}");
                SetUIStatus(true);
                return;
            }

            SetStatus("正在下载");
            WriteLog($@"提取到课程 ID 是 {termId}");

            // 3. get Mooc term JavaScript code.
            string moocTermCode;
            try
            {
                moocTermCode = await mooc.GetMocTermJavaScriptCodeAsync(termId);

                if (string.IsNullOrEmpty(moocTermCode))
                {
                    WriteLog("获取课程信息失败, 请检查课程是否开课.");
                    SetUIStatus(true);
                    return;
                }

                if (moocTermCode.Contains("com.netease.edu.commons.exceptions.FrontNotifiableRuntimeException"))
                {
                    // Old version of the course.
                    WriteLog("检测到课程是旧版课程, 暂时无法下载.");
                    return;
                }
            }
            catch (Exception exception)
            {
                WriteLog($"获取课程信息失败, 原因: {exception.Message}");
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
                    WriteLog("未提取到课程数据, 请检查课程是否开课.");
                    SetUIStatus(true);

                    return;
                }
            }
            catch (Exception exception)
            {
                WriteLog($"提取课程数据失败, 原因: {exception.Message}");
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
                    WriteLog("未提取到课程数据, 请检查课程是否开课.");
                    SetUIStatus(true);

                    return;
                }

                WriteLog($@"准备开始下载课程: {course.CourseName}");
            }
            catch (Exception exception)
            {
                WriteLog($"计算课程数据时发生错误, 原因: {exception.Message}");
                SetUIStatus(true);
                return;
            }

            try
            {
                FFmpegWorker.Instance.Start();
            }
            catch (Exception exception)
            {
                WriteLog($"视频合并功能启动失败, 原因: {exception.Message}");
                SetUIStatus(true);
                return;
            }

            try
            {
                await DownloadCourseListAsync(course, mooc);
            }
            catch (Exception exception)
            {
                Log.Error(exception, $"下载课程发生错误, 原因: {exception}");
                SetUIStatus(true);
                WriteLog($"下载课程发生错误, 原因: {exception}");

                return;
            }

            SetUIStatus(true);

            if (_isCancel)
            {
                WriteLog("已取消下载.");

                ResetCurrentBar();
                ResetTotalBar();
            }
            else
            {
                UpdateTotalBar(100);
                UpdateCurrentBar(100);
                SetStatus("下载完成");
                WriteLog($"课程 {course.CourseName} 已下载完成!");

                MessageBox.Show(
                    $@"课程 {course.CourseName} 已下载完成!", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }
        }

        private async Task DownloadCourseListAsync(CourseModel course, MoocRequest mooc)
        {
            var total = course?.Chapters?.Sum(c => c.Lessons?.Sum(l => l?.Units?.Count ?? 0)) ?? 0;

            if (total == 0)
            {
                return;
            }

            Log.Information($@"当前一共有 {total} 个课程单元.");

            var current = 0;

            int chapterIndex;
            for (chapterIndex = 0; chapterIndex < course?.Chapters?.Count && !_isCancel; chapterIndex++)
            {
                var chapter = course?.Chapters?[chapterIndex];

                for (var lessonIndex = 0; lessonIndex < chapter?.Lessons?.Count && !_isCancel; lessonIndex++)
                {
                    var lesson = chapter?.Lessons?[lessonIndex];

                    for (var unitIndex = 0; unitIndex < lesson?.Units?.Count && !_isCancel; unitIndex++)
                    {
                        // update total progress bar.
                        UpdateTotalBar(CalculatePercentage(++current, total));

                        var unit = lesson?.Units?[unitIndex];

                        if (unit is null)
                        {
                            break;
                        }

                        // create unit save path.
                        var chapterDir = $@"{chapterIndex + 1:00}-{FixPath(chapter.Name)}";
                        var lessonDir  = $@"{lessonIndex + 1:00}-{FixPath(lesson.Name)}";
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
                                Log.Information($@"下载视频: {unitFileName}.mp4");

                                WriteLog($@"下载视频: {unitFileName}");

                                // subtitles
                                if (_config.IsDownloadSubtitle)
                                {
                                    foreach (var caption in video.Result.SrtCaptions)
                                    {
                                        // subtitle file. E.g:
                                        //  01-第一节 Java明天 视频.zh.srt
                                        //  01-第一节 Java明天 视频.en.srt
                                        var srtName = $@"{unitFileName}.{caption.LanguageCode}.srt";
                                        Log.Information($@"下载字幕: {srtName}");

                                        try
                                        {
                                            var srtContent = await mooc.DownloadSubtitleAsync(caption.Url);

                                            if (srtContent is null)
                                            {
                                                WriteLog($"字幕 {srtName} 下载失败.");

                                                break;
                                            }

                                            File.WriteAllBytes(Path.Combine(unitPath, srtName), srtContent);
                                        }
                                        catch (Exception exception)
                                        {
                                            WriteLog($"字幕 {srtName} 下载失败, 原因: {exception.Message}");

                                            break;
                                        }
                                    }
                                }

                                var quality = video.Result.Videos.Select(v => v.Quality ?? 1).ToList().Max();

                                VideoModel videoInfo;

                                if (quality < (int) _config.VideoQuality)
                                {
                                    videoInfo = video.Result.Videos.FirstOrDefault(
                                        v => v.Quality.HasValue && v.Quality == quality
                                    );
                                }
                                else
                                {
                                    videoInfo = video.Result.Videos.FirstOrDefault(
                                        v => v.Quality.HasValue
                                          && (VideoQuality) v.Quality == _config.VideoQuality
                                    );
                                }

                                var videoFormat = videoInfo.Format.ToLower();

                                switch (videoFormat)
                                {
                                    case "hls": // m3u8 format.
                                        var ts2Mp4File = Path.Combine(courseVideo.SavePath, courseVideo.VideoFileName);

                                        if (File.Exists(ts2Mp4File))
                                        {
                                            Log.Warning($@"课程 {courseVideo.VideoFileName} 已存在, 跳过下载.");
                                            WriteLog($@"课程 {courseVideo.VideoFileName} 已存在, 跳过下载.");
                                            break;
                                        }

                                        var videoUrl = new Uri(videoInfo.VideoUrl); // video url.
                                        var baseUrl =
                                            $@"{videoUrl.Scheme}://{videoUrl.Host}" +
                                            string.Join("", videoUrl.Segments.Take(videoUrl.Segments.Length - 1));

                                        Configuration.Default.BaseUri = new Uri(baseUrl, UriKind.Absolute);

                                        string m3u8List;

                                        try
                                        {
                                            m3u8List = await mooc.DownloadM3U8ListAsync(videoUrl);

                                            if (string.IsNullOrEmpty(m3u8List))
                                            {
                                                WriteLog($"下载课程 {unitFileName} 的视频列表失败.");
                                                break;
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                            WriteLog($"下载课程 {unitFileName} 的视频列表发生错误, 原因: {exception.Message}");
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
                                                            WriteLog(
                                                                $"下载视频片段 {tsSavedName} 失败, 准备重试, 当前重试第 {j + 1} 次."
                                                            );
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
                                                        WriteLog(
                                                            $"下载视频片段 {tsSavedName} 发生异常, 原因: {exception.Message}, 准备重试, 当前重试第 {j + 1} 次."
                                                        );
                                                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, j)));
                                                    }
                                                }

                                                if (!downloadVideoSuccess)
                                                {
                                                    WriteLog($"下载视频片段 {tsSavedName} 失败, 已跳过.");
                                                }
                                            }

                                            WriteLog($@"课程 {unitFileName} 已下载完成.");

                                            File.WriteAllText(
                                                Path.Combine(unitPath, $@"{courseVideo.MergeListFile}"),
                                                merger.ToString()
                                            );

                                            FFmpegWorker.Instance.Enqueue(courseVideo);
                                        }
                                        catch (Exception exception)
                                        {
                                            WriteLog($"下载课程 {unitFileName} 的视频发生错误, 原因: {exception.Message}");
                                        }

                                        break;
                                    case "mp4":
                                        var mp4File = Path.Combine(unitPath, $"{unitFileName}.mp4");

                                        if (File.Exists(mp4File)) // exist mp4, skip.
                                        {
                                            Log.Warning($@"课程: {$"{unitFileName}.mp4"} 已存在, 跳过下载.");
                                            WriteLog($@"课程: {$"{unitFileName}.mp4"} 已存在, 跳过下载.");
                                        }

                                        var mp4Url = videoInfo.VideoUrl;

                                        if (string.IsNullOrEmpty(mp4Url))
                                        {
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
                                        }

                                        Log.Information($@"{unitFileName} 的下载链接是: {mp4Url}");

                                        for (var i = 0; i < MAX_TIMES; i++)
                                        {
                                            try
                                            {
                                                var videoBytes = await mooc.DownloadVideoAsync(mp4Url);

                                                if (videoBytes is null)
                                                {
                                                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                                    WriteLog($"下载课程视频 {unitFileName} 失败, 准备重试, 当前重试第 {i + 1} 次.");
                                                }
                                                else
                                                {
                                                    File.WriteAllBytes(mp4File, videoBytes);

                                                    WriteLog($@"课程 {unitFileName} 已下载完成.");
                                                    break;
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                                WriteLog($"下载课程 {unitFileName} 的视频发生错误, 原因: {exception.Message}");
                                            }
                                        }

                                        break;
                                    case "flv":
                                        var flvFile = Path.Combine(unitPath, $"{unitFileName}.flv");

                                        if (File.Exists(flvFile)) // exist mp4, skip.
                                        {
                                            Log.Warning($@"课程: {$"{unitFileName}.flv"} 已存在, 跳过下载.");
                                            WriteLog($@"课程: {$"{unitFileName}.flv"} 已存在, 跳过下载.");
                                        }

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

                                        Log.Information($@"{unitFileName} 的下载链接是: {flvUrl}");

                                        for (var i = 0; i < MAX_TIMES; i++)
                                        {
                                            try
                                            {
                                                var videoBytes = await mooc.DownloadVideoAsync(flvUrl);

                                                if (videoBytes is null)
                                                {
                                                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                                    WriteLog($"下载课程视频 {unitFileName} 失败, 准备重试, 当前重试第 {i + 1} 次.");
                                                }
                                                else
                                                {
                                                    File.WriteAllBytes(flvFile, videoBytes);

                                                    WriteLog($@"课程 {unitFileName} 已下载完成.");
                                                    break;
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                                                WriteLog($"下载课程 {unitFileName} 的视频发生错误, 原因: {exception.Message}");
                                            }
                                        }

                                        break;
                                }
                            }
                                break;
                            case UnitType.Document: // document type. E.g pdf.
                                await DownloadDocumentAsync(unitResult, unitFileName, mooc, unitPath);

                                break;
                            case UnitType.Attachment: // attachment type. E.g source code.
                                await DownloadAttachmentAsync(unit, mooc, unitPath, unitFileName);

                                break;
                            default: // not recognized type
                                WriteLog($"当前课程单元: {unitFileName} 类型不支持下载, 已忽略.");
                                break;
                        }
                    }
                }
            }

            SetUIStatus(true);

            if (_isCancel)
            {
                WriteLog("已取消下载.");

                ResetCurrentBar();
                ResetTotalBar();
            }
            else
            {
                UpdateTotalBar(100);
                UpdateCurrentBar(100);
                SetStatus("下载完成");
                WriteLog($"课程 {course.CourseName} 已下载完成!");

                MessageBox.Show(
                    $@"课程 {course.CourseName} 已下载完成!", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }
        }

        private async Task DownloadDocumentAsync(UnitResultModel unitResult, string unitFileName, MoocRequest mooc,
                                                 string          unitPath)
        {
            if (!_config.IsDownloadDocument)
            {
                return;
            }

            if (string.IsNullOrEmpty(unitResult.TextOrigUrl))
            {
                WriteLog("文档: unitFileName 下载链接为空, 跳过下载.");
                return;
            }

            var documentUrl        = unitResult.TextOrigUrl;
            var fileName           = $@"{unitFileName}.pdf";
            var downloadDocSuccess = false;

            WriteLog($@"准备下载文档: {fileName}");

            for (var i = 0; i < MAX_TIMES; i++)
            {
                try
                {
                    var document = await mooc.DownloadDocumentAsync(documentUrl);

                    if (document is null)
                    {
                        WriteLog($"下载文档 {fileName} 失败, 准备重试, 当前重试第 {i + 1} 次.");
                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                    }
                    else
                    {
                        File.WriteAllBytes(Path.Combine(unitPath, fileName), document);
                        downloadDocSuccess = true;
                        WriteLog($@"文档 {fileName} 已下载完成.");
                        break;
                    }
                }
                catch (Exception exception)
                {
                    WriteLog($@"下载文档 {fileName} 发生错误, 原因: {exception.Message}, 准备重试, 当前重试第 {i + 1} 次.");
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                }
            }

            if (!downloadDocSuccess)
            {
                WriteLog($"下载文档 {fileName} 失败, 已跳过.");
            }
        }

        private async Task DownloadAttachmentAsync(UnitModel unit, MoocRequest mooc, string unitPath,
                                                   string    unitFileName)
        {
            if (!_config.IsDownloadAttachment)
            {
                return;
            }

            const string attachmentBaseUrl = "https://www.icourse163.org/course/attachment.htm";

            if (string.IsNullOrEmpty(unit.JsonContent))
            {
                WriteLog($"附件 {unit.Name} 下载链接为空, 跳过下载.");
                return;
            }

            var content            = JObject.Parse(unit.JsonContent);
            var nosKey             = content["nosKey"]?.ToString();
            var fileName           = content["fileName"]?.ToString();
            var attachmentUrl      = $@"{attachmentBaseUrl}?fileName={fileName}&nosKey={nosKey}";
            var downloadAttSuccess = false;
            var attachSavePath     = Path.Combine(unitPath, $@"{unitFileName}-{FixPath(fileName)}");

            if (File.Exists(attachSavePath)) // exist attachment, skip.
            {
                WriteLog($@"附件 {fileName} 已下载, 跳过.");
                return;
            }

            WriteLog($@"准备下载附件: {fileName}");

            for (var i = 0; i < MAX_TIMES; i++)
            {
                try
                {
                    var attachment = await mooc.DownloadAttachmentAsync(attachmentUrl);

                    if (attachment is null)
                    {
                        WriteLog($"下载附件 {fileName} 失败, 准备重试, 当前重试第 {i + 1} 次.");
                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                    }
                    else
                    {
                        File.WriteAllBytes(attachSavePath, attachment);
                        downloadAttSuccess = true;

                        WriteLog($@"附件 {fileName} 已下载完成.");
                        break;
                    }
                }
                catch (Exception exception)
                {
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
                    WriteLog($@"下载附件 {fileName} 发生错误, 原因: {exception.Message}, 准备重试, 当前重试第 {i + 1} 次.");
                }
            }

            if (!downloadAttSuccess)
            {
                WriteLog($"下载附件 {fileName} 失败, 已跳过.");
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
                WriteLog("准备取消下载.");
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
