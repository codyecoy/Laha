namespace SahabatAnak.Api.Models;

public sealed class Article : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Category { get; set; } = "Edukasi";
    public string Excerpt { get; set; } = string.Empty;
    public string ContentHtml { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public int ReadingTimeMinutes { get; set; } = 5;
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public bool IsPublished { get; set; } = true;
    public string CoverTone { get; set; } = "emerald";
}
