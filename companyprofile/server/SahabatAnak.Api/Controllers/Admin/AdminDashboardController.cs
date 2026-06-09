using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Dashboard;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin,Editor,Operator")]
[Route("api/admin/dashboard")]
public sealed class AdminDashboardController : ControllerBase
{
    private readonly IDashboardService _dashboard;

    public AdminDashboardController(IDashboardService dashboard)
    {
        _dashboard = dashboard;
    }

    [HttpGet]
    public Task<DashboardStatsDto> Get(CancellationToken ct)
        => _dashboard.GetAsync(ct);
}
