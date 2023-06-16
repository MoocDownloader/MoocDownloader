namespace MoocResolver.Contracts;

public class Author
{
    public string Name { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? PhotoUrl { get; set; }

    public string? PhotoData { get; set; }

    public string? HomePage { get; set; }
}