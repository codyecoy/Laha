namespace SahabatAnak.Api.Models;

public sealed class CaseReport : BaseEntity
{
    public bool IsAnonymous { get; set; }
    public string ReporterName { get; set; } = string.Empty;
    public string ReporterContact { get; set; } = string.Empty;
    public string Chronology { get; set; } = string.Empty;
    public List<string> EvidenceUrls { get; set; } = new();
    public string Status { get; set; } = "baru";
}

