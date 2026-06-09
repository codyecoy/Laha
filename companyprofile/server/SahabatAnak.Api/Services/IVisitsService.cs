namespace SahabatAnak.Api.Services;

public interface IVisitsService
{
    Task TrackAsync(CancellationToken ct = default);
    Task<(int Daily, int Monthly, int Yearly)> GetSummaryAsync(CancellationToken ct = default);
}

