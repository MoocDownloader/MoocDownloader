using MoocDownloader.Services.Contracts;

namespace MoocDownloader.Services;

public class LanguageService : ILanguageService
{
    private readonly IResourceService _resourceService;

    public LanguageService(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    /// <inheritdoc />
    public virtual string L(string key, params object[] args)
    {
        var text = _resourceService.Get<string>(key);

        return string.IsNullOrEmpty(text)
            ? string.Empty
            : string.Format(text, args);
    }
}