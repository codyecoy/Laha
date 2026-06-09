using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Articles;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers;

[ApiController]
[Route("api/articles")]
public sealed class ArticlesController : ControllerBase
{
    private readonly IArticlesService _articles;

    public ArticlesController(IArticlesService articles)
    {
        _articles = articles;
    }

    [HttpGet]
    public Task<PagedResultDto<ArticleListItemDto>> List(
        [FromQuery] string? category,
        [FromQuery] string? q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 9,
        CancellationToken ct = default)
        => _articles.ListAsync(category, q, page, pageSize, includeUnpublished: false, ct);

    [HttpGet("{slug}")]
    public async Task<ActionResult<ArticleDetailDto>> GetBySlug([FromRoute] string slug, CancellationToken ct)
    {
        var res = await _articles.GetBySlugAsync(slug, includeUnpublished: false, ct);
        if (res is null) return NotFound();
        return Ok(res);
    }
}
