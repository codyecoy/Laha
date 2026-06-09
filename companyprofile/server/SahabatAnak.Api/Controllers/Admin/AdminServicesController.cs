using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Services;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin,Editor")]
[Route("api/admin/services")]
public sealed class AdminServicesController : ControllerBase
{
    private readonly IServicesService _services;

    public AdminServicesController(IServicesService services)
    {
        _services = services;
    }

    [HttpGet]
    public Task<PagedResultDto<ServiceDto>> List(
        [FromQuery] string? q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
        => _services.ListAdminAsync(q, page, pageSize, ct);

    [HttpPost]
    public Task<ServiceDto> Create([FromBody] UpsertServiceRequestDto dto, CancellationToken ct)
        => _services.CreateAsync(dto, ct);

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ServiceDto>> Update([FromRoute] Guid id, [FromBody] UpsertServiceRequestDto dto, CancellationToken ct)
    {
        var updated = await _services.UpdateAsync(id, dto, ct);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var ok = await _services.DeleteAsync(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}
