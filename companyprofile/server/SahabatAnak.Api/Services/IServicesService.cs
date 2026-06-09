using SahabatAnak.Api.DTOs.Services;
using SahabatAnak.Api.DTOs.Common;

namespace SahabatAnak.Api.Services;

public interface IServicesService
{
    Task<IReadOnlyList<ServiceDto>> ListAsync(CancellationToken ct = default);
    Task<PagedResultDto<ServiceDto>> ListAdminAsync(string? q, int page, int pageSize, CancellationToken ct = default);
    Task<ServiceDto> CreateAsync(UpsertServiceRequestDto dto, CancellationToken ct = default);
    Task<ServiceDto?> UpdateAsync(Guid id, UpsertServiceRequestDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
