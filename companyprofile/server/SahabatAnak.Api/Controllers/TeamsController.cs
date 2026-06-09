using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Teams;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers;

[ApiController]
[Route("api/teams")]
public sealed class TeamsController : ControllerBase
{
    private readonly ITeamsService _teams;

    public TeamsController(ITeamsService teams)
    {
        _teams = teams;
    }

    [HttpGet]
    public Task<IReadOnlyList<TeamMemberDto>> List(CancellationToken ct)
        => _teams.ListAsync(ct);
}

