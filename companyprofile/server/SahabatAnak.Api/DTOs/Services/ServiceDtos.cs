using System.ComponentModel.DataAnnotations;

namespace SahabatAnak.Api.DTOs.Services;

public sealed class ServiceDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Icon { get; init; }
    public required string ImageUrl { get; init; }
    public required IReadOnlyList<string> Highlights { get; init; }
}

public sealed class UpsertServiceRequestDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = "spark";
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> Highlights { get; set; } = new();
}

