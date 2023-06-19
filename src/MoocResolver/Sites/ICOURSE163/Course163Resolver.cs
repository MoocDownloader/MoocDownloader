using CefSharp;
using MoocResolver.Contracts;
using MoocResolver.Sites.ICOURSE163.Courses;
using MoocResolver.Sites.ICOURSE163.Coursewares;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text.Json;

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
        var courseTermResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.termDto)");
        var chiefLectorResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.chiefLector)");
        var staffLectorsResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.staffLectors)");
        var categoriesResponse = await Browser.EvaluateScriptAsync("JSON.stringify(window.categories)");

        // Get Course info.
        if (courseInfoResponse.Success && courseInfoResponse.Result is string courseInfodata)
        {
            _courseInfo = JsonSerializer.Deserialize<CourseInfo>(courseInfodata);
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

        if (result?.Result is null)
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

        foreach (var coursewareChapter in _coursewareTerm.Chapters)
        {
            if (coursewareChapter.Lessons is null)
            {
                continue;
            }

            foreach (var chapterLesson in coursewareChapter.Lessons)
            {
                if (chapterLesson.Units is null)
                {
                    continue;
                }

                foreach (var coursewareUnit in chapterLesson.Units)
                {
                    var list = coursewareUnit.ContentType switch
                    {
                        CoursewareContentType.Video => await SpiderVideoAsync(coursewareUnit),
                        CoursewareContentType.Document => await SpiderDocumentAsync(coursewareUnit),
                        _ => new List<Multimedia>()
                    };

                    if (list.Any())
                    {
                        continue;
                    }

                    _playlist.List.AddRange(list);
                }
            }
        }
    }

    private async Task<List<Multimedia>> SpiderVideoAsync(CoursewareUnit coursewareUnit)
    {
        const string tokenUrl = "https://www.icourse163.org/web/j/resourceRpcBean.getResourceToken.rpc";
        const string videoUrl = "https://vod.study.163.com/eds/api/v1/vod/video";

        var list = new List<Multimedia>();
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

        var videoId = tokenResult?.Result?.VideoSign?.VideoId;
        var signature = tokenResult?.Result?.VideoSign?.Signature;
        var videoName = tokenResult?.Result?.VideoSign?.Name;

        if (videoId is null || string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(videoName))
        {
            return list;
        }

        var videoParameter = $"?videoId={videoId}&signature={signature}&clientType=1";
        using var videoRequest = new HttpRequestMessage(HttpMethod.Get, videoUrl + videoParameter);
        AddHttpHeaders(videoRequest, Array.Empty<NameValueHeaderValue>());
        using var viderResponse = await HttpClient!.SendAsync(videoRequest);
        await using var videoStream = await viderResponse.EnsureSuccessStatusCode().Content.ReadAsStreamAsync();
        var videoResult = await JsonSerializer.DeserializeAsync<CoursewareResult<CoursewareVideoResult>>(videoStream);

        var videos = videoResult?.Result?.Videos;
        var captions = videoResult?.Result?.Captions;

        if (captions?.Any() ?? false)
        {
            foreach (var coursewareCaption in captions)
            {
                var multimedia = new Multimedia
                {
                    FileName = $"{videoName}_{coursewareCaption.Code}",
                    DownloadUrl = coursewareCaption.Url,
                    MediaFormat = "srt",
                    RelativePath = "", // TODO
                    GroupName = "", // TODO
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
        if (videos.Any(video => video.Format == "mp4"))
        {
            videos.Where(video => video.Format == "mp4").MaxBy(video => video.Quality);
        }

        return list;
    }

    private async Task<List<Multimedia>> SpiderDocumentAsync(CoursewareUnit coursewareUnit)
    {
        var list = new List<Multimedia>();
        await Task.CompletedTask;
        return list;
    }
}