using CefSharp;
using MoocResolver.Contracts;
using MoocResolver.Sites.ICOURSE163.Courses;
using MoocResolver.Sites.ICOURSE163.Coursewares;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MoocResolver.Sites.ICOURSE163;

/// <summary>
/// Website name: 中国大学MOOC(慕课)_国家精品课程在线学习平台
/// Website address: https://www.icourse163.org/
/// </summary>
public class Course163Resolver : ResolverBase
{
    public const string Domain = "www.icourse163.org";

    private readonly Playlist _playlist = new();
    private readonly List<CourseCategory> _courseCategories = new();
    private readonly List<CourseLector> _courseLectors = new();

    private string? _courseId;
    private CourseInfo? _courseInfo;
    private CourseSchool? _courseSchool;
    private CourseTerm? _courseTerm;
    private CoursewareTerm? _coursewareTerm;

    /// <inheritdoc />
    public Course163Resolver(string link, CookieCollection cookies) : base(link, cookies)
    {
    }

    /// <inheritdoc />
    public override bool CanResolve()
    {
        return Link.Contains(Domain, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public override async Task<Playlist> ResolveAsync()
    {
        // 1. Parse course id from the `Link`.
        ParseCourseId();

        // 2. Spider course.
        await InitialBrowserAsync();
        await SpiderCourseAsync();

        _playlist.Name = _courseInfo!.Name;

        // 3. Spider courseware.
        InitialHttpClient();
        await SpiaderCoursewareAsync();

        // 4. Spider file links.
        await SpiderMultimediaAsync();

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

        var playlistData = JsonSerializer.Serialize(_playlist, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        });

        await File.WriteAllTextAsync("playlist.json", playlistData);

        return _playlist;
    }

    private string GetCsrfToken()
    {
        const string csrfTokenKey = "NTESSTUDYSI";
        var cookie = Cookies.FirstOrDefault(cookie => cookie.Name == csrfTokenKey);

        if (cookie is null)
        {
            // Credential is wrong.
            throw new AuthenticationException();
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
        var courseId = new Uri(Link).Segments.LastOrDefault();

        if (string.IsNullOrEmpty(courseId))
        {
            throw new ArgumentException(nameof(courseId));
        }

        _courseId = courseId;
    }

    private async Task SpiderCourseAsync()
    {
        var pageUrl = $"https://www.icourse163.org/course/{_courseId}";
        var response = await Browser!.LoadUrlAsync(pageUrl);

        if (!response.Success)
        {
            throw new ApplicationException(
                $"Page load failed with ErrorCode:{response.ErrorCode}, HttpStatusCode:{response.HttpStatusCode}");
        }

        var courseInfoResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.courseDto)");
        var courseSchoolResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.schoolDto)");
        var courseTermResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.termDto)");
        var chiefLectorResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.chiefLector)");
        var staffLectorsResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.staffLectors)");
        var categoriesResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.categories)");

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
    }

    private async Task SpiaderCoursewareAsync()
    {
        const string coursewareUrl = "https://www.icourse163.org/web/j/courseBean.getLastLearnedMocTermDto.rpc";

        var csrfToke = GetCsrfToken();
        using var request = new HttpRequestMessage(HttpMethod.Post, coursewareUrl + $"?csrfKey={csrfToke}")
        {
            Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("termId", _courseTerm?.TermId ?? string.Empty)
            })
        };

        AddHttpHeaders(request, new NameValueHeaderValue[]
        {
            new("edu-script-token", csrfToke),
            new("Referer", Link)
        });

        using var response = await HttpClient!.SendAsync(request);
        await using var stream = await response.EnsureSuccessStatusCode().Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<CoursewareResult<CoursewareTermResult>>(stream);

        if (result?.Result is null) // TODO
        {
            // Credential has expired.
            throw new AuthenticationException();
        }

        _coursewareTerm = result.Result.Term;
    }

    private async Task SpiderMultimediaAsync()
    {
        if (_coursewareTerm?.Chapters is null)
        {
            throw new ArgumentNullException();
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
                    var relativePath =
                        $"{_courseSchool?.Name}/{i:00}_{chapter.ChapterName}/{i:00}.{j:00}_{lesson.LessonName}";
                    var groupName = chapter.ChapterName;

                    var list = unit.ContentType switch
                    {
                        CoursewareContentType.Video =>
                            await SpiderVideoAsync(unit, index, relativePath, groupName),
                        CoursewareContentType.Document =>
                            await SpiderDocumentAsync(unit, index, relativePath, groupName),
                        _ => new List<Multimedia>()
                    };

                    if (!list.Any()) // List is empty.
                    {
                        continue;
                    }

                    _playlist.List.AddRange(list);
                }
            }
        }
    }

    private async Task<List<Multimedia>> SpiderVideoAsync(
        CoursewareUnit coursewareUnit,
        int index,
        string relativePath,
        string groupName)
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

        var videos = videoResult?.Result?.Videos;
        var captions = videoResult?.Result?.Captions;

        if (captions?.Any() ?? false)
        {
            foreach (var coursewareCaption in captions)
            {
                var multimedia = new Multimedia
                {
                    Index = index,
                    FileName = $"{videoName}_{coursewareCaption.Code}",
                    DownloadUrl = coursewareCaption.Url,
                    MediaFormat = "srt",
                    RelativePath = relativePath,
                    GroupName = groupName,
                };
                list.Add(multimedia);
            }
        }

        if (videos is null)
        {
            return list;
        }

        // Priority:
        // 1. quality: 3 > 2 > 1.
        // 2. format: mp4 > hls > flv.
        const StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;
        videos = videos.Where(v1 => v1.Quality == videos.Max(v2 => v2.Quality)).ToList();
        var video = videos.FirstOrDefault(video => string.Equals(video.Format, "mp4", ignoreCase)) ??
                    videos.FirstOrDefault(video => string.Equals(video.Format, "hls", ignoreCase)) ??
                    videos.FirstOrDefault(video => string.Equals(video.Format, "flv", ignoreCase));

        if (video is null)
        {
            return list;
        }

        list.Add(new Multimedia
        {
            Index = index,
            FileName = videoName,
            DownloadUrl = video.VideoUrl,
            ImageUrl = videoResult?.Result?.VideoImageUrl,
            MediaFormat = video.Format,
            RelativePath = relativePath,
            GroupName = groupName,
        });

        return list;
    }

    private async Task<CoursewareVideoSign?> GetVideoSignAsync(CoursewareUnit coursewareUnit)
    {
        const string tokenUrl = "https://www.icourse163.org/web/j/resourceRpcBean.getResourceToken.rpc";

        var csrfToke = GetCsrfToken();
        using var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl + $"?csrfKey={csrfToke}")
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
            new("edu-script-token", csrfToke),
            new("Referer", Link)
        });
        using var tokenResponse = await HttpClient!.SendAsync(tokenRequest);
        await using var tokenStream = await tokenResponse.EnsureSuccessStatusCode().Content.ReadAsStreamAsync();
        var tokenResult = await JsonSerializer.DeserializeAsync<CoursewareResult<CoursewareTokenResult>>(tokenStream);

        return tokenResult?.Result?.VideoSign;
    }

    private async Task<List<Multimedia>> SpiderDocumentAsync(
        CoursewareUnit coursewareUnit,
        int index,
        string relativePath,
        string groupName)
    {
        const string documentUrl = "https://www.icourse163.org/dwr/call/plaincall/CourseBean.getLessonUnitLearnVo.dwr";
        const string serializeFun = "function serialize(){return JSON.stringify(arguments[2])}\r\n";
        const string callFunName = "dwr.engine._remoteHandleCallback";
        const string targetFunName = "serialize";

        var csrfToke = GetCsrfToken();

        using var documentRequest = new HttpRequestMessage(HttpMethod.Post, documentUrl)
        {
            Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("callCount", "1"),
                new("scriptSessionId", "${scriptSessionId}190"),
                new("httpSessionId", csrfToke),
                new("c0-scriptName", "CourseBean"),
                new("c0-methodName", "getLessonUnitLearnVo"),
                new("c0-id", "0"),
                new("c0-param0", $"number:{coursewareUnit.ContentId}"),
                new("c0-param1", "number:3"),
                new("c0-param2", "number:0"),
                new("c0-param3", $"number:{coursewareUnit.UnitId}"),
                new("batchId", $"{GetTimestamp()}"), // Timestamp of the current time.
            })
        };
        AddHttpHeaders(documentRequest, new NameValueHeaderValue[]
        {
            new("edu-script-token", csrfToke),
            new("Referer", Link)
        });
        using var response = await HttpClient!.SendAsync(documentRequest);
        var content = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
        var javascriptCode = serializeFun + content.Replace(callFunName, targetFunName);

        var loadedResponse = await Browser!.LoadUrlAsync(BlankPage);

        var list = new List<Multimedia>();

        if (!loadedResponse.Success)
        {
            return list;
        }

        var evaluatedResponse = await Browser!.EvaluateScriptAsync(javascriptCode);

        if (!evaluatedResponse.Success)
        {
            return list;
        }

        if (evaluatedResponse.Result is not string evaluatedResult)
        {
            return list;
        }

        var remoteResult = JsonSerializer.Deserialize<CoursewareRemoteResult>(evaluatedResult);
        var multimedia = new Multimedia
        {
            Index = index,
            FileName = "",
            DownloadUrl = remoteResult?.TextOriginalUrl,
            RelativePath = relativePath,
            GroupName = groupName,
        };

        list.Add(multimedia);
        return list;
    }
}