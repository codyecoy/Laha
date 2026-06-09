using System.ComponentModel.DataAnnotations;

namespace SahabatAnak.Api.DTOs.Articles;

public class ArticleListItemDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Slug { get; init; }
    public required string Category { get; init; }
    public required string Excerpt { get; init; }
    public required string ThumbnailUrl { get; init; }
    public required bool Featured { get; init; }
    public required int ReadingTime { get; init; }
    public required DateTime PublishedAt { get; init; }
    public required bool IsPublished { get; init; }
    public required string CoverTone { get; init; }
}

public sealed class ArticleDetailDto : ArticleListItemDto
{
    public required string Content { get; init; }
}

public sealed class UpsertArticleRequestDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Category { get; set; } = "Edukasi";

    public string Excerpt { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string ThumbnailUrl { get; set; } = string.Empty;

    public bool Featured { get; set; }

    public bool IsPublished { get; set; } = true;

    public int ReadingTime { get; set; } = 5;

    public DateTime? PublishedAt { get; set; }

    public string CoverTone { get; set; } = "emerald";
}
