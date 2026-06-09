using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Reports;

namespace SahabatAnak.Api.Services;

public interface IReportsService
{
    Task<SubmitReportResponseDto> SubmitAsync(SubmitReportRequestDto dto, CancellationToken ct = default);
    Task<PagedResultDto<ReportDto>> ListAsync(string? status, int page, int pageSize, CancellationToken ct = default);
    Task<ReportDto?> UpdateStatusAsync(Guid id, string status, CancellationToken ct = default);
}

