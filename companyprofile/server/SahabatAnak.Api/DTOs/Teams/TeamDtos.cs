using System.ComponentModel.DataAnnotations;

namespace SahabatAnak.Api.DTOs.Teams;

public sealed class TeamMemberDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Role { get; init; }
    public required string Bio { get; init; }
    public required string Tone { get; init; }
    public required string PhotoUrl { get; init; }
}

public sealed class UpsertTeamMemberRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Tone { get; set; } = "emerald";
    public string PhotoUrl { get; set; } = string.Empty;
}

