using MoocDownloader.Services.Contracts;
using System.Windows;

namespace MoocDownloader.Services;

public class ResourceService : IResourceService
{
    private readonly ResourceDictionary _resources;

    public ResourceService(ResourceDictionary resources)
    {
        _resources = resources;
    }

    /// <inheritdoc />
    public T? Get<T>(string key)
    {
        if (_resources[key] is T value)
        {
            return value;
        }

        return default;
    }
}