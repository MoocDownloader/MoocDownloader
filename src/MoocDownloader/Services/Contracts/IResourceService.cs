namespace MoocDownloader.Services.Contracts;

public interface IResourceService
{
    T? Get<T>(string key);
}