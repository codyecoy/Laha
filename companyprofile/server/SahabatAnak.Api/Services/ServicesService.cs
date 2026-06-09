using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Services;
using SahabatAnak.Api.Models;
using SahabatAnak.Api.Repositories;

namespace SahabatAnak.Api.Services;

public sealed class ServicesService : IServicesService
{
    private readonly IGenericRepository<ServiceItem> _services;

    public ServicesService(IGenericRepository<ServiceItem> services)
    {
        _services = services;
    }

    public async Task<IReadOnlyList<ServiceDto>> ListAsync(CancellationToken ct = default)
    {
        var items = await _services.Query().OrderBy(x => x.Title).ToListAsync(ct);
        return items.Select(Map).ToList();
    }

    public async Task<PagedResultDto<ServiceDto>> ListAdminAsync(string? q, int page, int pageSize, CancellationToken ct = default)
    {
        var safePage = Math.Max(1, page);
        var safeSize = Math.Clamp(pageSize, 1, 50);

        var query = _services.Query();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var needle = $"%{q.Trim()}%";
            query = query.Where(x => EF.Functions.Like(x.Title, needle) || EF.Functions.Like(x.Description, needle));
        }

        query = query.OrderBy(x => x.Title);

        var total = await query.CountAsync(ct);
        var totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)safeSize));
        var clampedPage = Math.Min(safePage, totalPages);
        var skip = (clampedPage - 1) * safeSize;

        var data = await query.Skip(skip).Take(safeSize).ToListAsync(ct);

        return new PagedResultDto<ServiceDto>
        {
            Data = data.Select(Map).ToList(),
            Page = clampedPage,
            PageSize = safeSize,
            Total = total,
            TotalPages = totalPages,
        };
    }

    public async Task<ServiceDto> CreateAsync(UpsertServiceRequestDto dto, CancellationToken ct = default)
    {
        var entity = new ServiceItem();
        Apply(entity, dto);
        await _services.AddAsync(entity, ct);
        await _services.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<ServiceDto?> UpdateAsync(Guid id, UpsertServiceRequestDto dto, CancellationToken ct = default)
    {
        var entity = await _services.GetByIdAsync(id, ct);
        if (entity is null) return null;
        Apply(entity, dto);
        _services.Update(entity);
        await _services.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _services.GetByIdAsync(id, ct);
        if (entity is null) return false;
        _services.SoftDelete(entity);
        await _services.SaveChangesAsync(ct);
        return true;
    }

    private static void Apply(ServiceItem entity, UpsertServiceRequestDto dto)
    {
        entity.Title = (dto.Title ?? string.Empty).Trim();
        entity.Description = dto.Description ?? string.Empty;
        entity.Icon = string.IsNullOrWhiteSpace(dto.Icon) ? "spark" : dto.Icon.Trim();
        entity.ImageUrl = dto.ImageUrl ?? string.Empty;
        entity.Highlights = dto.Highlights?.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).Take(8).ToList()
            ?? new List<string>();
    }

    private static ServiceDto Map(ServiceItem x) => new()
    {
        Id = x.Id,
        Title = x.Title,
        Description = x.Description,
        Icon = x.Icon,
        ImageUrl = x.ImageUrl,
        Highlights = x.Highlights,
    };
}
