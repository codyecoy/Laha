using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Donations;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin,Operator")]
[Route("api/admin/donations")]
public sealed class AdminDonationsController : ControllerBase
{
    private readonly IDonationsService _donations;

    public AdminDonationsController(IDonationsService donations)
    {
        _donations = donations;
    }

    [HttpGet]
    public Task<PagedResultDto<DonationDto>> List(
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
        => _donations.ListAsync(status, page, pageSize, ct);

    [HttpPut("{id:guid}/status")]
    public async Task<ActionResult<DonationDto>> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateDonationStatusRequestDto dto, CancellationToken ct)
    {
        try
        {
            var updated = await _donations.UpdateStatusAsync(id, dto.Status, ct);
            if (updated is null) return NotFound();
            return Ok(updated);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}

