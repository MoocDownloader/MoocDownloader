using CefSharp;
using MoocResolver.Contracts;
using MoocResolver.Exceptions;
using MoocResolver.Helpers;
using MoocResolver.Sites.ICOURSE163.Courses;
using MoocResolver.Sites.ICOURSE163.Coursewares;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;

namespace MoocResolver.Sites.ICOURSE163;

/// <summary>
/// Website name: 中国大学MOOC(慕课)_国家精品课程在线学习平台
/// Website address: https://www.icourse163.org/
/// </summary>
public class Course163Resolver : ResolverBase
{
    public const string Pattern = @"^(https:\/\/)?www.icourse163.org\/(course|learn)";
    private const string SessionKey = "NTESSTUDYSI";

    private readonly Playlist _playlist = new();
    private readonly List<CourseCategory> _courseCategories = new();
    private readonly List<CourseLector> _courseLectors = new();

    private string? _courseId;
    private string? _courseIntro;
    private CourseInfo? _courseInfo;
    private CourseSchool? _courseSchool;
    private CourseTerm? _courseTerm;
    private CoursewareTerm? _coursewareTerm;

    /// <inheritdoc />
    public Course163Resolver(ResolverOption option) : base(option)
    {
    }

    /// <inheritdoc />
    public override async Task<Playlist> ResolveAsync()
    {
        // 1. Parse course id from the `Link`.
        ParseCourseId();

        // 2. Spider course.
        await InitializeBrowserAsync();
        await SpiderCourseAsync();

        if (string.IsNullOrEmpty(_courseInfo?.Id) || string.IsNullOrEmpty(_courseTerm?.TermId))
        {
            throw new ResolveFailedException(ErrorCodes.ICOURSE163.CannotParseCourseId);
        }

        _playlist.Name = _courseInfo?.Name;
        _playlist.Introduction = _courseIntro;
        _playlist.CoverImageUrl = _courseTerm?.PhotoUrl;
        _playlist.OriginalLink = Option.Link;

        // Initialize HTTP Client.
        InitializeHttpClient();

        // 3. Authentication.
        await LoginAsync();

        // 4. Spider courseware.
        await SpiderCoursewareAsync();

        // 5. Spider file links.
        await SpiderMultimediaAsync();

        // 6. Construct playlist.
        foreach (var courseCategory in _courseCategories)
        {
            _playlist.Categories.Add(new Category
            {
                Index = Convert.ToInt32(courseCategory.Index),
                Name = courseCategory.Name,
            });
        }

        foreach (var courseLector in _courseLectors)
        {
            _playlist.Authors.Add(new Author
            {
                Name = courseLector.Name,
                Title = courseLector.Title,
                PhotoUrl = courseLector.PhotoUrl,
            });
        }

        return _playlist;
    }

    private async Task LoginAsync()
    {
        // 1. Check whether the user has already logged in.
        if (Cookies.GetAllCookies().Any(cookie => cookie.Name == SessionKey))
        {
            return;
        }

        // 2. Authenticate the user if the user has not logged in.
        const string homeUrl = "https://www.icourse163.org/";
        const string loginUrl = homeUrl + "passport/reg/icourseLogin.do";
        const string returnUrl = homeUrl + "/member/login.htm#/webLoginIndex";
        const string failUrl = homeUrl + "/member/login.htm?emailEncoded=";

        using var homeRequest = new HttpRequestMessage(HttpMethod.Get, homeUrl);
        using var homeResponse = await HttpClient!.SendAsync(homeRequest);

        if (!homeResponse.IsSuccessStatusCode)
        {
            throw new ResolveFailedException(ErrorCodes.Resolver.UnableToAccessPage);
        }

        using var loginRequest = new HttpRequestMessage(HttpMethod.Post, loginUrl)
        {
            Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("returnUrl", Encode(returnUrl)),
                new("failUrl", Encode(failUrl + Encode(Credential.Username))),
                new("saveLogin", "true"),
                new("oauthType", ""),
                new("username", Credential.Username),
                new("passwd", Credential.Password),
            })
        };
        AddHttpHeaders(loginRequest, Array.Empty<NameValueHeaderValue>());
        using var loginResponse = await HttpClient!.SendAsync(loginRequest);

        // 3. Checking result logged.
        switch (loginResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                // Login successful.
                const string study163Domain = "study.163.com";

                foreach (var cookie in Cookies.GetAllCookies().Cast<System.Net.Cookie>())
                {
                    Cookies.Add(new System.Net.Cookie(
                        name: cookie.Name,
                        value: cookie.Value,
                        path: cookie.Path,
                        domain: study163Domain));
                }

                break;
            case HttpStatusCode.Redirect:
                // Wrong username or password.
                throw new ResolveFailedException(ErrorCodes.Resolver.WrongCredentials);
            default:
                // Login failed.
                throw new ResolveFailedException(ErrorCodes.Resolver.LoginFailed);
        }
    }

    private string GetHttpSessionId()
    {
        var cookie = Cookies.GetAllCookies().FirstOrDefault(cookie => cookie.Name == SessionKey);

        if (cookie is null)
        {
            // Credentials have wrong.
            throw new ResolveFailedException(ErrorCodes.Resolver.WrongCredentials);
        }

        return cookie.Value;
    }

    private void ParseCourseId()
    {
        // There are two types of `Link`:
        // 1. https://www.icourse163.org/course/xxxxxx
        // 2. https://www.icourse163.org/learn/xxxx?tid=xxxx
        //
        // Example:
        // 1. https://www.icourse163.org/course/XMU-1001771003
        // 2. https://www.icourse163.org/learn/XMU-1001771003?tid=1470076448#/learn/announce
        //
        // Course ID: XMU-1001771003
        var courseId = new Uri(Option.Link).Segments.LastOrDefault();

        if (string.IsNullOrEmpty(courseId))
        {
            throw new ResolveFailedException(ErrorCodes.ICOURSE163.CannotParseCourseId);
        }

        _courseId = courseId;
    }

    private async Task SpiderCourseAsync()
    {
        const string getIntroJavaScriptCode = "document.getElementsByClassName('course-heading-intro')[0].innerText";

        var pageUrl = $"https://www.icourse163.org/course/{_courseId}";
        var response = await Browser!.LoadUrlAsync(pageUrl);

        if (!response.Success)
        {
            throw new ResolveFailedException(ErrorCodes.Resolver.UnableToAccessPage);
        }

        var courseInfoResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.courseDto)");
        var courseSchoolResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.schoolDto)");
        var courseTermResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.termDto)");
        var chiefLectorResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.chiefLector)");
        var staffLectorsResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.staffLectors)");
        var categoriesResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.categories)");
        var introductionResponse = await Browser.EvaluateScriptAsync(getIntroJavaScriptCode);

        // Get Course info.
        if (courseInfoResponse.Success && courseInfoResponse.Result is string courseInfoData)
        {
            _courseInfo = JsonSerializer.Deserialize<CourseInfo>(courseInfoData);
        }

        // Get school.
        if (courseSchoolResponse.Success && courseSchoolResponse.Result is string courseSchoolData)
        {
            _courseSchool = JsonSerializer.Deserialize<CourseSchool>(courseSchoolData);
        }

        // Get Term.
        if (courseTermResponse.Success && courseTermResponse.Result is string courseTermData)
        {
            _courseTerm = JsonSerializer.Deserialize<CourseTerm>(courseTermData);
        }

        // Get categories.
        if (categoriesResponse.Success && categoriesResponse.Result is string categoriesData)
        {
            var categories = JsonSerializer.Deserialize<List<CourseCategory>>(categoriesData);

            if (categories is not null)
            {
                _courseCategories.AddRange(categories);
            }
        }

        // Get Chief Lector.
        if (chiefLectorResponse.Success && chiefLectorResponse.Result is string lectorData)
        {
            var chiefLector = JsonSerializer.Deserialize<CourseLector>(lectorData);

            if (chiefLector is not null)
            {
                _courseLectors.Add(chiefLector);
            }
        }

        // Get List of Staff Lector.
        if (staffLectorsResponse.Success && staffLectorsResponse.Result is string staffLectorsData)
        {
            var staffLectors = JsonSerializer.Deserialize<List<CourseLector>>(staffLectorsData);

            if (staffLectors is not null)
            {
                _courseLectors.AddRange(staffLectors);
            }
        }

        // Get introduction.
        if (introductionResponse.Success && introductionResponse.Result is string introData)
        {
            _courseIntro = introData;
        }
    }

    private async Task SpiderCoursewareAsync()
    {
        const string coursewareUrl = "https://www.icourse163.org/web/j/courseBean.getLastLearnedMocTermDto.rpc";

        var httpSessionId = GetHttpSessionId();
        var requestUrl = coursewareUrl + $"?csrfKey={httpSessionId}";
        using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
        {
            Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("termId", _courseTerm?.TermId ?? string.Empty)
            })
        };

        AddHttpHeaders(request, new NameValueHeaderValue[]
        {
            new("edu-script-token", httpSessionId),
            new("Referer", Option.Link)
        });

        using var response = await HttpClient!.SendAsync(request);
        await using var stream = await response.EnsureSuccessStatusCode().Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<CoursewareResult<CoursewareTermResult>>(stream);

        if (result?.Result is null) // TODO: https://www.icourse163.org/course/ZJU-93001?tid=1003997005
        {
            // Credentials have expired.
            throw new ResolveFailedException(ErrorCodes.Resolver.WrongCredentials);
        }

        _coursewareTerm = result.Result.Term;
    }

    private async Task SpiderMultimediaAsync()
    {
        if (_coursewareTerm?.Chapters is null)
        {
            throw new ResolveFailedException(ErrorCodes.Resolver.CannotResolve);
        }

        for (var i = 0; i < _coursewareTerm.Chapters.Count; i++)
        {
            var chapter = _coursewareTerm.Chapters[i];

            if (chapter.Lessons is null)
            {
                continue;
            }

            for (var j = 0; j < chapter.Lessons.Count; j++)
            {
                var lesson = chapter.Lessons[j];

                if (lesson.Units is null)
                {
                    continue;
                }

                for (var k = 0; k < lesson.Units.Count; k++)
                {
                    var unit = lesson.Units[k];

                    var index = i * 100 + j * 10 + k;
                    var relativePath = GetRelativePath(i, chapter, j, lesson);
                    var groupName = chapter.ChapterName;

                    if (unit.ContentType == CoursewareContentType.Video)
                    {
                        var multimediaList = await SpiderVideoAsync(unit);

                        foreach (var multimedia in multimediaList)
                        {
                            multimedia.Index = index;
                            multimedia.RelativePath = relativePath;
                            multimedia.GroupName = groupName;
                        }

                        _playlist.List.AddRange(multimediaList);
                    }

                    if (unit.ContentType == CoursewareContentType.Document)
                    {
                        var document = await SpiderDocumentAsync(unit);

                        if (document is not null)
                        {
                            document.Index = index;
                            document.RelativePath = relativePath;
                            document.GroupName = groupName;

                            _playlist.List.Add(document);
                        }
                    }
                }
            }
        }
    }

    private async Task<List<Multimedia>> SpiderVideoAsync(CoursewareUnit coursewareUnit)
    {
        var list = new List<Multimedia>();

        var videoSign = await GetVideoSignAsync(coursewareUnit);
        var videoId = videoSign?.VideoId;
        var signature = videoSign?.Signature;
        var videoName = videoSign?.Name;

        if (videoId is null || string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(videoName))
        {
            return list;
        }

        const string videoUrl = "https://vod.study.163.com/eds/api/v1/vod/video";

        var videoParameter = $"?videoId={videoId}&signature={signature}&clientType=1";
        using var videoRequest = new HttpRequestMessage(HttpMethod.Get, videoUrl + videoParameter);
        AddHttpHeaders(videoRequest, Array.Empty<NameValueHeaderValue>());
        using var videoResponse = await HttpClient!.SendAsync(videoRequest);

        await using var videoStream = await videoResponse.EnsureSuccessStatusCode().Content.ReadAsStreamAsync();
        var videoResult = await JsonSerializer.DeserializeAsync<CoursewareResult<CoursewareVideoResult>>(videoStream);

        // Priority of videos:
        // 1. quality: 3 > 2 > 1.
        // 2. format: mp4 > hls > flv.
        var videos = videoResult?.Result?.Videos
            ?.Where(v1 => v1.Quality == videoResult.Result?.Videos.Max(v2 => v2.Quality))
            .ToList();
        var captions = videoResult?.Result?.Captions;

        // Spider the subtitle file corresponding to the video.
        if (captions?.Any() ?? false)
        {
            const string captionFormat = "srt";
            var videoFileName = Path.GetFileNameWithoutExtension(videoName);
            foreach (var coursewareCaption in captions)
            {
                var multimedia = new Multimedia
                {
                    FileName = $"{videoFileName}.{coursewareCaption.Code}.{captionFormat}",
                    FileUrl = coursewareCaption.Url,
                    MediaFormat = captionFormat,
                };
                list.Add(multimedia);
            }
        }

        if (videos is null)
        {
            return list;
        }

        // Spider the video.
        const StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;
        var video = videos.FirstOrDefault(video => string.Equals(video.Format, "mp4", ignoreCase)) ??
                    videos.FirstOrDefault(video => string.Equals(video.Format, "hls", ignoreCase)) ??
                    videos.FirstOrDefault(video => string.Equals(video.Format, "flv", ignoreCase));

        if (video is null)
        {
            return list;
        }

        list.Add(new Multimedia
        {
            FileName = videoName,
            FileUrl = video.VideoUrl,
            FileSize = video.Size,
            ImageUrl = videoResult?.Result?.VideoImageUrl,
            MediaFormat = video.Format,
        });

        return list;
    }

    private async Task<CoursewareVideoSign?> GetVideoSignAsync(CoursewareUnit coursewareUnit)
    {
        const string tokenUrl = "https://www.icourse163.org/web/j/resourceRpcBean.getResourceToken.rpc";

        var httpSessionId = GetHttpSessionId();
        using var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl + $"?csrfKey={httpSessionId}")
        {
            Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("bizId", coursewareUnit.UnitId.ToString()),
                new("bizType", "1"),
                new("contentType", "1")
            })
        };
        AddHttpHeaders(tokenRequest, new NameValueHeaderValue[]
        {
            new("edu-script-token", httpSessionId),
            new("Referer", Option.Link)
        });
        using var tokenResponse = await HttpClient!.SendAsync(tokenRequest);
        await using var tokenStream = await tokenResponse.EnsureSuccessStatusCode().Content.ReadAsStreamAsync();
        var tokenResult = await JsonSerializer.DeserializeAsync<CoursewareResult<CoursewareTokenResult>>(tokenStream);

        return tokenResult?.Result?.VideoSign;
    }

    private async Task<Multimedia?> SpiderDocumentAsync(CoursewareUnit coursewareUnit)
    {
        const string documentUrl = "https://www.icourse163.org/dwr/call/plaincall/CourseBean.getLessonUnitLearnVo.dwr";
        const string serializeFun = "function serialize(){return JSON.stringify(arguments[2])}\r\n";
        const string callFunName = "dwr.engine._remoteHandleCallback";
        const string targetFunName = "serialize";

        var httpSessionId = GetHttpSessionId();

        using var documentRequest = new HttpRequestMessage(HttpMethod.Post, documentUrl)
        {
            Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("callCount", "1"),
                new("scriptSessionId", "${scriptSessionId}190"),
                new("httpSessionId", httpSessionId),
                new("c0-scriptName", "CourseBean"),
                new("c0-methodName", "getLessonUnitLearnVo"),
                new("c0-id", "0"),
                new("c0-param0", $"number:{coursewareUnit.ContentId}"),
                new("c0-param1", "number:3"),
                new("c0-param2", "number:0"),
                new("c0-param3", $"number:{coursewareUnit.UnitId}"),
                new("batchId", $"{CurrentTimestamp()}"), // Timestamp of the current time.
            })
        };
        AddHttpHeaders(documentRequest, new NameValueHeaderValue[]
        {
            new("edu-script-token", httpSessionId),
            new("Referer", Option.Link)
        });
        using var response = await HttpClient!.SendAsync(documentRequest);
        var content = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
        var javascriptCode = serializeFun + content.Replace(callFunName, targetFunName);

        var loadedResponse = await Browser!.LoadUrlAsync(StartPage);

        if (!loadedResponse.Success)
        {
            return null;
        }

        var evaluatedResponse = await Browser!.EvaluateScriptAsync(javascriptCode);

        if (!evaluatedResponse.Success)
        {
            return null;
        }

        if (evaluatedResponse.Result is not string evaluatedResult)
        {
            return null;
        }

        var remoteResult = JsonSerializer.Deserialize<CoursewareRemoteResult>(evaluatedResult);
        var originalUrl = remoteResult?.TextOriginalUrl;

        if (string.IsNullOrEmpty(originalUrl))
        {
            return null;
        }

        var multimedia = new Multimedia
        {
            FileName = coursewareUnit.UnitName,
            FileUrl = originalUrl,
        };
        var decodeUrl = new Uri(HttpUtility.UrlDecode(originalUrl));
        var queries = HttpUtility.ParseQueryString(decodeUrl.Query);

        if (queries.HasKeys() && queries.AllKeys.Contains("download"))
        {
            var fileName = queries["download"];

            if (string.IsNullOrEmpty(fileName))
            {
                return multimedia;
            }

            multimedia.FileName = queries["download"];
            multimedia.MediaFormat = new FileInfo(fileName).Extension.TrimStart('.');
        }

        return multimedia;
    }

    private string GetRelativePath(
        int chapterIndex,
        CoursewareChapter chapter,
        int lessonIndex,
        CoursewareLesson lesson)
    {
        var pathBuilder = new StringBuilder();

        // Example:
        // [School]CourseName/00_ChapterName/00.00_LessonName
        pathBuilder
            .Append($"[{_courseSchool?.Name}]{_courseInfo?.Name}")
            .Append("/")
            .Append($"{chapterIndex + 1:00}_{chapter.ChapterName}")
            .Append("/")
            .Append($"{chapterIndex + 1:00}.{lessonIndex + 1:00}_{lesson.LessonName}");

        return pathBuilder.ToString();
    }

    private static string Encode(string plainText) => Base64Helper.Encode(plainText);
}