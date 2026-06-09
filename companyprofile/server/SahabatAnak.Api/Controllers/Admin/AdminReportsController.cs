using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Reports;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin,Operator")]
[Route("api/admin/reports")]
public sealed class AdminReportsController : ControllerBase
{
    private readonly IReportsService _reports;

    public AdminReportsController(IReportsService reports)
    {
        _reports = reports;
    }

    [HttpGet]
    public Task<PagedResultDto<ReportDto>> List(
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
        => _reports.ListAsync(status, page, pageSize, ct);

    [HttpPut("{id:guid}/status")]
    public async Task<ActionResult<ReportDto>> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateReportStatusRequestDto dto, CancellationToken ct)
    {
        var updated = await _reports.UpdateStatusAsync(id, dto.Status, ct);
        if (updated is null) return NotFound();
        return Ok(updated);
    }
}
