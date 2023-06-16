using CefSharp;
using CefSharp.Core;
using CefSharp.OffScreen;
using MoocResolver.Contracts;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace MoocResolver.Sites.ICOURSE163;

/// <summary>
/// Website name: 中国大学MOOC(慕课)_国家精品课程在线学习平台
/// Website address: https://www.icourse163.org/
/// </summary>
public class Course163Resolver : ResolverBase
{
    public const string Domain = "www.icourse163.org";

    private string? _courseId;

    private CourseInfo? _courseInfo;
    private CourseTerm? _courseTerm;
    private List<CourseCategory> _courseCategories = new();
    private List<CourseLector> _courseLectors = new();

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
    public override async Task<ResolvedResult> ResolveAsync()
    {
        // 1. Parse course id from the `Link`.
        ParseCourseId();

        // 2. Spider course info.
        await SpiderInfoAsync();

        //InitialHttpClient();
        return new ResolvedResult();
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

    private async Task SpiderInfoAsync()
    {
        var localData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var cachePath = Path.Combine(localData, @"MoocDownloader\Cache");
        var userDataPath = Path.Combine(localData, @"MoocDownloader\UserData");
        var settings = new CefSettings
        {
            CachePath = cachePath,
            UserDataPath = userDataPath,
            UserAgent = UserAgent,
        };
        var success = await CefSharp.Cef.InitializeAsync(
            settings: settings,
            performDependencyCheck: true,
            browserProcessHandler: null);

        if (!success)
        {
            return;
        }

        var browserSettings = ObjectFactory.CreateBrowserSettings(autoDispose: true);

        // Disable loading images.
        browserSettings.ImageLoading = CefState.Disabled;

        var pageUrl = $"https://www.icourse163.org/course/{_courseId}";
        using var browser = new ChromiumWebBrowser(pageUrl, browserSettings);
        var initialLoadResponse = await browser.WaitForInitialLoadAsync();

        if (!initialLoadResponse.Success)
        {
            return;
        }

        try
        {
            var courseInfoResponse = await browser.EvaluateScriptAsync("window.courseDto");
            var courseTermResponse = await browser.EvaluateScriptAsync("window.termDto");
            var chiefLectorResponse = await browser.EvaluateScriptAsync("window.chiefLector");
            var staffLectorsResponse = await browser.EvaluateScriptAsync("window.staffLectors");
            var categoriesResponse = await browser.EvaluateScriptAsync("window.categories");

            if (courseInfoResponse.Success) // Get Course info.
            {
                var courseInfoData = JsonConvert.SerializeObject(courseInfoResponse.Result);
                _courseInfo = JsonConvert.DeserializeObject<CourseInfo>(courseInfoData);
            }

            if (courseTermResponse.Success) // Get Term.
            {
                var courseTermData = JsonConvert.SerializeObject(courseTermResponse.Result);
                _courseTerm = JsonConvert.DeserializeObject<CourseTerm>(courseTermData);
            }

            if (categoriesResponse.Success) // Get categories.
            {
                var categoriesData = JsonConvert.SerializeObject(categoriesResponse.Result);
                var categories = JsonConvert.DeserializeObject<List<CourseCategory>>(categoriesData);

                if (categories is not null)
                {
                    _courseCategories.AddRange(categories);
                }
            }

            if (chiefLectorResponse.Success) // Get Chief Lector.
            {
                var lectorData = JsonConvert.SerializeObject(chiefLectorResponse.Result);
                var chiefLector = JsonConvert.DeserializeObject<CourseLector>(lectorData);

                if (chiefLector is not null)
                {
                    _courseLectors.Add(chiefLector);
                }
            }

            if (staffLectorsResponse.Success) // Get List of Staff Lector.
            {
                var staffLectorsData = JsonConvert.SerializeObject(staffLectorsResponse.Result);
                var staffLectors = JsonConvert.DeserializeObject<List<CourseLector>>(staffLectorsData);

                if (staffLectors is not null)
                {
                    _courseLectors.AddRange(staffLectors);
                }
            }
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
        }
    }
}