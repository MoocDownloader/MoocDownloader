namespace MoocResolver.Contracts;

public class FileLink
{
    public int Index { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string DownloadUrl { get; set; } = string.Empty;

    public string FileType { get; set; } = string.Empty;

    public string RelativePath { get; set; } = string.Empty;

    public string GroupName { get; set; } = string.Empty;
}