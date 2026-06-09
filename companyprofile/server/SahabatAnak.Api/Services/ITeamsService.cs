using SahabatAnak.Api.DTOs.Teams;
using SahabatAnak.Api.DTOs.Common;

namespace SahabatAnak.Api.Services;

public interface ITeamsService
{
    Task<IReadOnlyList<TeamMemberDto>> ListAsync(CancellationToken ct = default);
    Task<PagedResultDto<TeamMemberDto>> ListAdminAsync(string? q, int page, int pageSize, CancellationToken ct = default);
    Task<TeamMemberDto> CreateAsync(UpsertTeamMemberRequestDto dto, CancellationToken ct = default);
    Task<TeamMemberDto?> UpdateAsync(Guid id, UpsertTeamMemberRequestDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
