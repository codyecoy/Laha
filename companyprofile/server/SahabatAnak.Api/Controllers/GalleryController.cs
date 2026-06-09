using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Gallery;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers;

[ApiController]
[Route("api/gallery")]
public sealed class GalleryController : ControllerBase
{
    private readonly IGalleryService _gallery;

    public GalleryController(IGalleryService gallery)
    {
        _gallery = gallery;
    }

    [HttpGet]
    public Task<IReadOnlyList<GalleryPhotoDto>> List(CancellationToken ct)
        => _gallery.ListPublicAsync(ct);
}

