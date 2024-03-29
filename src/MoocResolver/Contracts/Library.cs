﻿namespace MoocResolver.Contracts;

public class Library
{
    public string? Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string? Introduction { get; set; } = string.Empty;

    public string? CoverImageUrl { get; set; }

    public string? CoverImageData { get; set; }

    public DateTime CreationTime { get; set; } = DateTime.Now;

    public List<Category> Categories { get; set; } = new();

    public List<Author> Authors { get; set; } = new();

    public List<Media> Medias { get; set; } = new();
}