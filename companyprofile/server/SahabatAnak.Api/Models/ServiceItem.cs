namespace SahabatAnak.Api.Models;

public sealed class ServiceItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = "spark";
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> Highlights { get; set; } = new();
}

