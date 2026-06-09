using System.ComponentModel.DataAnnotations;

namespace SahabatAnak.Api.DTOs.Donations;

public sealed class SubmitDonationRequestDto
{
    [Required]
    public decimal Amount { get; set; }

    public string Method { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    [Required]
    public string RecipientBank { get; set; } = string.Empty;

    [Required]
    public string RecipientAccountNumber { get; set; } = string.Empty;

    [Required]
    public string RecipientAccountName { get; set; } = string.Empty;

    public string ReferenceCode { get; set; } = string.Empty;
    public string ProofUrl { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}

public sealed class SubmitDonationResponseDto
{
    public required Guid Id { get; init; }
}

public sealed class DonationDto
{
    public required Guid Id { get; init; }
    public required decimal Amount { get; init; }
    public required string Method { get; init; }
    public required string DonorName { get; init; }
    public required string DonorEmail { get; init; }
    public required string DonorPhone { get; init; }
    public required string RecipientBank { get; init; }
    public required string RecipientAccountNumber { get; init; }
    public required string RecipientAccountName { get; init; }
    public required string ReferenceCode { get; init; }
    public required string ProofUrl { get; init; }
    public required string Note { get; init; }
    public required string Status { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public sealed class UpdateDonationStatusRequestDto
{
    [Required]
    public string Status { get; set; } = string.Empty;
}
