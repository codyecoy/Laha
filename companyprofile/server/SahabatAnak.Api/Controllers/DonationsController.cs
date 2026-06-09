using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Donations;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers;

[ApiController]
[Route("api/donations")]
public sealed class DonationsController : ControllerBase
{
    private readonly IDonationsService _donations;

    public DonationsController(IDonationsService donations)
    {
        _donations = donations;
    }

    [HttpPost]
    public async Task<ActionResult<SubmitDonationResponseDto>> Submit([FromBody] SubmitDonationRequestDto dto, CancellationToken ct)
    {
        try
        {
            var res = await _donations.SubmitAsync(dto, ct);
            return Ok(res);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}

