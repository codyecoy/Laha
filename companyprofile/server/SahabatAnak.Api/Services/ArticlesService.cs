using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.DTOs.Articles;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.Models;
using SahabatAnak.Api.Repositories;

namespace SahabatAnak.Api.Services;

public sealed class ArticlesService : IArticlesService
{
    private readonly IGenericRepository<Article> _articles;

    public ArticlesService(IGenericRepository<Article> articles)
    {
        _articles = articles;
    }

    public async Task<PagedResultDto<ArticleListItemDto>> ListAsync(
        string? category,
        string? q,
        int page,
        int pageSize,
        bool includeUnpublished = false,
        CancellationToken ct = default)
    {
        var safePage = Math.Max(1, page);
        var safeSize = Math.Clamp(pageSize, 1, 50);

        var query = _articles.Query();
        if (!includeUnpublished)
        {
            query = query.Where(x => x.IsPublished);
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            var c = category.Trim();
            query = query.Where(x => x.Category == c);
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            var needle = q.Trim().ToLowerInvariant();
            query = query.Where(x => (x.Title + " " + x.Excerpt).ToLower().Contains(needle));
        }

        query = includeUnpublished ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderByDescending(x => x.PublishedAt);

        var total = await query.CountAsync(ct);
        var totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)safeSize));
        var clampedPage = Math.Min(safePage, totalPages);
        var skip = (clampedPage - 1) * safeSize;

        var data = await query.Skip(skip).Take(safeSize).Select(x => new ArticleListItemDto
        {
            Id = x.Id,
            Title = x.Title,
            Slug = x.Slug,
            Category = x.Category,
            Excerpt = x.Excerpt,
            ThumbnailUrl = x.ThumbnailUrl,
            Featured = x.IsFeatured,
            ReadingTime = x.ReadingTimeMinutes,
            PublishedAt = x.PublishedAt,
            IsPublished = x.IsPublished,
            CoverTone = x.CoverTone,
        }).ToListAsync(ct);

        return new PagedResultDto<ArticleListItemDto>
        {
            Data = data,
            Page = clampedPage,
            PageSize = safeSize,
            Total = total,
            TotalPages = totalPages,
        };
    }

    public async Task<ArticleDetailDto?> GetBySlugAsync(string slug, bool includeUnpublished = false, CancellationToken ct = default)
    {
        var safe = (slug ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(safe)) return null;
        var query = _articles.Query().Where(x => x.Slug == safe);
        if (!includeUnpublished) query = query.Where(x => x.IsPublished);
        var entity = await query.FirstOrDefaultAsync(ct);
        if (entity is null) return null;
        return MapDetail(entity);
    }

    public async Task<ArticleDetailDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _articles.GetByIdAsync(id, ct);
        if (entity is null) return null;
        return MapDetail(entity);
    }

    public async Task<ArticleDetailDto> CreateAsync(UpsertArticleRequestDto dto, CancellationToken ct = default)
    {
        var entity = new Article();
        ApplyUpsert(entity, dto);
        await _articles.AddAsync(entity, ct);
        await _articles.SaveChangesAsync(ct);
        return MapDetail(entity);
    }

    public async Task<ArticleDetailDto?> UpdateAsync(Guid id, UpsertArticleRequestDto dto, CancellationToken ct = default)
    {
        var entity = await _articles.GetByIdAsync(id, ct);
        if (entity is null) return null;
        ApplyUpsert(entity, dto);
        _articles.Update(entity);
        await _articles.SaveChangesAsync(ct);
        return MapDetail(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _articles.GetByIdAsync(id, ct);
        if (entity is null) return false;
        _articles.SoftDelete(entity);
        await _articles.SaveChangesAsync(ct);
        return true;
    }

    private static void ApplyUpsert(Article entity, UpsertArticleRequestDto dto)
    {
        entity.Title = (dto.Title ?? string.Empty).Trim();
        entity.Slug = !string.IsNullOrWhiteSpace(dto.Slug) ? dto.Slug.Trim() : Slugify(entity.Title);
        entity.Category = string.IsNullOrWhiteSpace(dto.Category) ? "Edukasi" : dto.Category.Trim();
        entity.Excerpt = dto.Excerpt ?? string.Empty;
        entity.ContentHtml = dto.Content ?? string.Empty;
        entity.ThumbnailUrl = dto.ThumbnailUrl ?? string.Empty;
        entity.IsFeatured = dto.Featured;
        entity.IsPublished = dto.IsPublished;
        entity.ReadingTimeMinutes = dto.ReadingTime <= 0 ? 5 : dto.ReadingTime;
        entity.PublishedAt = dto.PublishedAt ?? entity.PublishedAt;
        if (entity.IsPublished && entity.PublishedAt == default)
        {
            entity.PublishedAt = DateTime.UtcNow;
        }
        entity.CoverTone = string.IsNullOrWhiteSpace(dto.CoverTone) ? "emerald" : dto.CoverTone.Trim();
    }

    private static ArticleDetailDto MapDetail(Article x) => new()
    {
        Id = x.Id,
        Title = x.Title,
        Slug = x.Slug,
        Category = x.Category,
        Excerpt = x.Excerpt,
        ThumbnailUrl = x.ThumbnailUrl,
        Featured = x.IsFeatured,
        ReadingTime = x.ReadingTimeMinutes,
        PublishedAt = x.PublishedAt,
        IsPublished = x.IsPublished,
        CoverTone = x.CoverTone,
        Content = x.ContentHtml,
    };

    private static string Slugify(string input)
    {
        var s = (input ?? string.Empty).Trim().ToLowerInvariant();
        s = Regex.Replace(s, @"[^a-z0-9]+", "-");
        s = Regex.Replace(s, @"(^-|-$)+", "");
        return string.IsNullOrWhiteSpace(s) ? Guid.NewGuid().ToString("N") : s;
    }
}
