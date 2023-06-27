namespace MoocResolver.Contracts;

public class Media
{
    public int Index { get; set; }

    public string? FileName { get; set; }

    public string? FileUrl { get; set; }

    public long? FileSize { get; set; }

    public string? ImageUrl { get; set; }

    public string? MediaFormat { get; set; }

    public string? RelativePath { get; set; }

    public string? GroupName { get; set; }
}