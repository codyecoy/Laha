using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers;

[ApiController]
[Route("api/visits")]
public sealed class VisitsController : ControllerBase
{
    private readonly IVisitsService _visits;

    public VisitsController(IVisitsService visits)
    {
        _visits = visits;
    }

    [HttpPost("ping")]
    public async Task<IActionResult> Ping(CancellationToken ct)
    {
        await _visits.TrackAsync(ct);
        return Ok(new { ok = true });
    }
}

