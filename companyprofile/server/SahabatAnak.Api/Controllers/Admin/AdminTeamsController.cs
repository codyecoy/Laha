using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Teams;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin,Editor")]
[Route("api/admin/teams")]
public sealed class AdminTeamsController : ControllerBase
{
    private readonly ITeamsService _teams;

    public AdminTeamsController(ITeamsService teams)
    {
        _teams = teams;
    }

    [HttpGet]
    public Task<PagedResultDto<TeamMemberDto>> List(
        [FromQuery] string? q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
        => _teams.ListAdminAsync(q, page, pageSize, ct);

    [HttpPost]
    public Task<TeamMemberDto> Create([FromBody] UpsertTeamMemberRequestDto dto, CancellationToken ct)
        => _teams.CreateAsync(dto, ct);

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TeamMemberDto>> Update([FromRoute] Guid id, [FromBody] UpsertTeamMemberRequestDto dto, CancellationToken ct)
    {
        var updated = await _teams.UpdateAsync(id, dto, ct);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var ok = await _teams.DeleteAsync(id, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}
