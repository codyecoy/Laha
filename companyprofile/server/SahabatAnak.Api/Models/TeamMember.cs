namespace SahabatAnak.Api.Models;

public sealed class TeamMember : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Tone { get; set; } = "emerald";
    public string PhotoUrl { get; set; } = string.Empty;
}

