using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SahabatAnak.Api.DTOs.Reports;

public sealed class SubmitReportRequestDto
{
    public bool IsAnonymous { get; set; } = true;
    public string Name { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;

    [Required]
    public string Chronology { get; set; } = string.Empty;

    public List<string> EvidenceUrls { get; set; } = new();
}

public sealed class SubmitReportResponseDto
{
    public required Guid Id { get; init; }
}

public sealed class ReportDto
{
    public required Guid Id { get; init; }
    public required bool IsAnonymous { get; init; }
    public required string Name { get; init; }
    public required string Contact { get; init; }
    public required string Chronology { get; init; }
    public required IReadOnlyList<string> EvidenceUrls { get; init; }
    public required string Status { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public sealed class UpdateReportStatusRequestDto
{
    [Required]
    public string Status { get; set; } = string.Empty;
}

public sealed class SubmitReportFormRequestDto
{
    public bool IsAnonymous { get; set; } = true;
    public string Name { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;

    [Required]
    public string Chronology { get; set; } = string.Empty;

    public IFormFile[]? EvidenceFiles { get; set; }
}
