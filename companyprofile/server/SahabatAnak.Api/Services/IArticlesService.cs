using SahabatAnak.Api.DTOs.Articles;
using SahabatAnak.Api.DTOs.Common;

namespace SahabatAnak.Api.Services;

public interface IArticlesService
{
    Task<PagedResultDto<ArticleListItemDto>> ListAsync(string? category, string? q, int page, int pageSize, bool includeUnpublished = false, CancellationToken ct = default);
    Task<ArticleDetailDto?> GetBySlugAsync(string slug, bool includeUnpublished = false, CancellationToken ct = default);
    Task<ArticleDetailDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ArticleDetailDto> CreateAsync(UpsertArticleRequestDto dto, CancellationToken ct = default);
    Task<ArticleDetailDto?> UpdateAsync(Guid id, UpsertArticleRequestDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
