using System.ComponentModel.DataAnnotations;

namespace SahabatAnak.Api.DTOs.Contacts;

public sealed class SubmitContactRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; } = string.Empty;
}

public sealed class ContactDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Subject { get; init; }
    public required string Message { get; init; }
    public required string Status { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public sealed class UpdateContactStatusRequestDto
{
    [Required]
    public string Status { get; set; } = string.Empty;
}

