using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Gallery;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin,Editor")]
[Route("api/admin/gallery")]
public sealed class AdminGalleryController : ControllerBase
{
    private readonly IGalleryService _gallery;

    public AdminGalleryController(IGalleryService gallery)
    {
        _gallery = gallery;
    }

    [HttpGet]
    public Task<PagedResultDto<GalleryPhotoDto>> List(
        [FromQuery] string? category,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12,
        CancellationToken ct = default)
        => _gallery.ListAdminAsync(category, page, pageSize, ct);

    [HttpPost]
    public Task<GalleryPhotoDto> Create([FromBody] UpsertGalleryPhotoRequestDto dto, CancellationToken ct)
        => _gallery.CreateAsync(dto, ct);

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GalleryPhotoDto>> Update([FromRoute] Guid id, [FromBody] UpsertGalleryPhotoRequestDto dto, CancellationToken ct)
    {
        var updated = await _gallery.UpdateAsync(id, dto, ct);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var ok = await _gallery.DeleteAsync(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}
