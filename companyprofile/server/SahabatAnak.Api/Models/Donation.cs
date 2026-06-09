namespace SahabatAnak.Api.Models;

public sealed class Donation : BaseEntity
{
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty;
    public string DonorName { get; set; } = string.Empty;
    public string DonorEmail { get; set; } = string.Empty;
    public string DonorPhone { get; set; } = string.Empty;
    public string RecipientBank { get; set; } = string.Empty;
    public string RecipientAccountNumber { get; set; } = string.Empty;
    public string RecipientAccountName { get; set; } = string.Empty;
    public string ReferenceCode { get; set; } = string.Empty;
    public string ProofUrl { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public string Status { get; set; } = "baru";
}
