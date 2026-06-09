using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Gallery;
using SahabatAnak.Api.Models;
using SahabatAnak.Api.Repositories;

namespace SahabatAnak.Api.Services;

public sealed class GalleryService : IGalleryService
{
    private readonly IGenericRepository<GalleryPhoto> _gallery;

    public GalleryService(IGenericRepository<GalleryPhoto> gallery)
    {
        _gallery = gallery;
    }

    public async Task<IReadOnlyList<GalleryPhotoDto>> ListPublicAsync(CancellationToken ct = default)
    {
        var items = await _gallery.Query()
            .OrderBy(x => x.SortOrder)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(ct);

        return items.Select(Map).ToList();
    }

    public async Task<PagedResultDto<GalleryPhotoDto>> ListAdminAsync(string? category, int page, int pageSize, CancellationToken ct = default)
    {
        var query = _gallery.Query();

        if (!string.IsNullOrWhiteSpace(category))
        {
            var normalized = category.Trim();
            query = query.Where(x => x.Category == normalized);
        }

        var total = await query.CountAsync(ct);
        var safePage = Math.Max(1, page);
        var safePageSize = Math.Max(1, Math.Min(50, pageSize));
        var totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)safePageSize));
        var clampedPage = Math.Min(safePage, totalPages);
        var skip = (clampedPage - 1) * safePageSize;

        var items = await query
            .OrderBy(x => x.SortOrder)
            .ThenByDescending(x => x.CreatedAt)
            .Skip(skip)
            .Take(safePageSize)
            .ToListAsync(ct);

        return new PagedResultDto<GalleryPhotoDto>
        {
            Data = items.Select(Map).ToList(),
            Page = clampedPage,
            PageSize = safePageSize,
            Total = total,
            TotalPages = totalPages,
        };
    }

    public async Task<GalleryPhotoDto> CreateAsync(UpsertGalleryPhotoRequestDto dto, CancellationToken ct = default)
    {
        var entity = new GalleryPhoto();
        Apply(entity, dto);
        await _gallery.AddAsync(entity, ct);
        await _gallery.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<GalleryPhotoDto?> UpdateAsync(Guid id, UpsertGalleryPhotoRequestDto dto, CancellationToken ct = default)
    {
        var entity = await _gallery.GetByIdAsync(id, ct);
        if (entity is null) return null;
        Apply(entity, dto);
        _gallery.Update(entity);
        await _gallery.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _gallery.GetByIdAsync(id, ct);
        if (entity is null) return false;
        _gallery.SoftDelete(entity);
        await _gallery.SaveChangesAsync(ct);
        return true;
    }

    private static void Apply(GalleryPhoto entity, UpsertGalleryPhotoRequestDto dto)
    {
        entity.ImageUrl = (dto.ImageUrl ?? string.Empty).Trim();
        entity.Title = (dto.Title ?? string.Empty).Trim();
        entity.Alt = (dto.Alt ?? string.Empty).Trim();
        entity.Category = (dto.Category ?? string.Empty).Trim();
        entity.SortOrder = dto.SortOrder;
    }

    private static GalleryPhotoDto Map(GalleryPhoto x) => new()
    {
        Id = x.Id,
        Title = x.Title,
        Alt = x.Alt,
        ImageUrl = x.ImageUrl,
        Category = x.Category,
        SortOrder = x.SortOrder,
        CreatedAt = x.CreatedAt,
    };
}

