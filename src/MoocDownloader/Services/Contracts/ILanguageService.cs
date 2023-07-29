namespace MoocDownloader.Services.Contracts;

public interface ILanguageService
{
    string L(string key, params object[] args);
}