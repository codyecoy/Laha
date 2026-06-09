using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Donations;

namespace SahabatAnak.Api.Services;

public interface IDonationsService
{
    Task<SubmitDonationResponseDto> SubmitAsync(SubmitDonationRequestDto dto, CancellationToken ct = default);
    Task<PagedResultDto<DonationDto>> ListAsync(string? status, int page, int pageSize, CancellationToken ct = default);
    Task<DonationDto?> UpdateStatusAsync(Guid id, string status, CancellationToken ct = default);
    Task<(decimal Total, decimal Monthly, int Count)> GetSummaryAsync(CancellationToken ct = default);
}

