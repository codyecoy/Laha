using SahabatAnak.Api.DTOs.Dashboard;

namespace SahabatAnak.Api.Services;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetAsync(CancellationToken ct = default);
}

