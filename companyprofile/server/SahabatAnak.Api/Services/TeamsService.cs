using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Teams;
using SahabatAnak.Api.Models;
using SahabatAnak.Api.Repositories;

namespace SahabatAnak.Api.Services;

public sealed class TeamsService : ITeamsService
{
    private readonly IGenericRepository<TeamMember> _teams;

    public TeamsService(IGenericRepository<TeamMember> teams)
    {
        _teams = teams;
    }

    public async Task<IReadOnlyList<TeamMemberDto>> ListAsync(CancellationToken ct = default)
    {
        var items = await _teams.Query().OrderBy(x => x.Name).ToListAsync(ct);
        return items.Select(Map).ToList();
    }

    public async Task<PagedResultDto<TeamMemberDto>> ListAdminAsync(string? q, int page, int pageSize, CancellationToken ct = default)
    {
        var safePage = Math.Max(1, page);
        var safeSize = Math.Clamp(pageSize, 1, 50);

        var query = _teams.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var needle = $"%{q.Trim()}%";
            query = query.Where(x =>
                EF.Functions.Like(x.Name, needle) ||
                EF.Functions.Like(x.Role, needle) ||
                EF.Functions.Like(x.Bio, needle));
        }

        query = query.OrderBy(x => x.Name);

        var total = await query.CountAsync(ct);
        var totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)safeSize));
        var clampedPage = Math.Min(safePage, totalPages);
        var skip = (clampedPage - 1) * safeSize;

        var items = await query.Skip(skip).Take(safeSize).ToListAsync(ct);

        return new PagedResultDto<TeamMemberDto>
        {
            Data = items.Select(Map).ToList(),
            Page = clampedPage,
            PageSize = safeSize,
            Total = total,
            TotalPages = totalPages,
        };
    }

    public async Task<TeamMemberDto> CreateAsync(UpsertTeamMemberRequestDto dto, CancellationToken ct = default)
    {
        var entity = new TeamMember();
        Apply(entity, dto);
        await _teams.AddAsync(entity, ct);
        await _teams.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<TeamMemberDto?> UpdateAsync(Guid id, UpsertTeamMemberRequestDto dto, CancellationToken ct = default)
    {
        var entity = await _teams.GetByIdAsync(id, ct);
        if (entity is null) return null;
        Apply(entity, dto);
        _teams.Update(entity);
        await _teams.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _teams.GetByIdAsync(id, ct);
        if (entity is null) return false;
        _teams.SoftDelete(entity);
        await _teams.SaveChangesAsync(ct);
        return true;
    }

    private static void Apply(TeamMember entity, UpsertTeamMemberRequestDto dto)
    {
        entity.Name = (dto.Name ?? string.Empty).Trim();
        entity.Role = dto.Role ?? string.Empty;
        entity.Bio = dto.Bio ?? string.Empty;
        entity.Tone = string.IsNullOrWhiteSpace(dto.Tone) ? "emerald" : dto.Tone.Trim();
        entity.PhotoUrl = dto.PhotoUrl ?? string.Empty;
    }

    private static TeamMemberDto Map(TeamMember x) => new()
    {
        Id = x.Id,
        Name = x.Name,
        Role = x.Role,
        Bio = x.Bio,
        Tone = x.Tone,
        PhotoUrl = x.PhotoUrl,
    };
}
