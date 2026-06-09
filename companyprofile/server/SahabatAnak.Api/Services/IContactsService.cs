using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Contacts;

namespace SahabatAnak.Api.Services;

public interface IContactsService
{
    Task<ContactDto> SubmitAsync(SubmitContactRequestDto dto, CancellationToken ct = default);
    Task<PagedResultDto<ContactDto>> ListAsync(string? status, int page, int pageSize, CancellationToken ct = default);
    Task<ContactDto?> UpdateStatusAsync(Guid id, string status, CancellationToken ct = default);
}

