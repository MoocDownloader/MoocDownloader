namespace MoocResolver.Contracts;

public class ResolvedResult
{
    public string OriginalLink { get; set; } = string.Empty;

    public string Introduction { get; set; } = string.Empty;

    public string? CoverImageUrl { get; set; }

    public DateTime CreationTime { get; set; } = DateTime.Now;

    public List<Author> Authors { get; set; } = new();

    public List<ResolvedFileLink> Links { get; set; } = new();
}