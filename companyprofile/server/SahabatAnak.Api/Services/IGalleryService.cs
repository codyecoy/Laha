using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Gallery;

namespace SahabatAnak.Api.Services;

public interface IGalleryService
{
    Task<IReadOnlyList<GalleryPhotoDto>> ListPublicAsync(CancellationToken ct = default);
    Task<PagedResultDto<GalleryPhotoDto>> ListAdminAsync(string? category, int page, int pageSize, CancellationToken ct = default);
    Task<GalleryPhotoDto> CreateAsync(UpsertGalleryPhotoRequestDto dto, CancellationToken ct = default);
    Task<GalleryPhotoDto?> UpdateAsync(Guid id, UpsertGalleryPhotoRequestDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}

