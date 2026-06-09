using System.ComponentModel.DataAnnotations;

namespace SahabatAnak.Api.DTOs.Gallery;

public sealed class GalleryPhotoDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Alt { get; init; }
    public required string ImageUrl { get; init; }
    public required string Category { get; init; }
    public required int SortOrder { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public sealed class UpsertGalleryPhotoRequestDto
{
    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

