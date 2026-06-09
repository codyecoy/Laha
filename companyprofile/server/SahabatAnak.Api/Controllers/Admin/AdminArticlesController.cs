using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Articles;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin,Editor")]
[Route("api/admin/articles")]
public sealed class AdminArticlesController : ControllerBase
{
    private readonly IArticlesService _articles;

    public AdminArticlesController(IArticlesService articles)
    {
        _articles = articles;
    }

    [HttpGet]
    public Task<PagedResultDto<ArticleListItemDto>> List(
        [FromQuery] string? category,
        [FromQuery] string? q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
        => _articles.ListAsync(category, q, page, pageSize, includeUnpublished: true, ct);

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ArticleDetailDto>> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var res = await _articles.GetByIdAsync(id, ct);
        if (res is null) return NotFound();
        return Ok(res);
    }

    [HttpPost]
    public Task<ArticleDetailDto> Create([FromBody] UpsertArticleRequestDto dto, CancellationToken ct)
        => _articles.CreateAsync(dto, ct);

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ArticleDetailDto>> Update([FromRoute] Guid id, [FromBody] UpsertArticleRequestDto dto, CancellationToken ct)
    {
        var updated = await _articles.UpdateAsync(id, dto, ct);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var ok = await _articles.DeleteAsync(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}
