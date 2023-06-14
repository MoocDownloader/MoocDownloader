namespace MoocResolver.Contracts;

public class Author
{
    public string Name { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public string? HomePage { get; set; }
}