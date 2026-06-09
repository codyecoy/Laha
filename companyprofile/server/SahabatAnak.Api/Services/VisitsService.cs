using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.Data;

namespace SahabatAnak.Api.Services;

public sealed class VisitsService : IVisitsService
{
    private readonly ApplicationDbContext _db;

    public VisitsService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task TrackAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var day = now.Date;
        var id = Guid.NewGuid();

        await _db.Database.ExecuteSqlInterpolatedAsync($"""
MERGE [DailyVisits] WITH (HOLDLOCK) AS target
USING (SELECT {day} AS [Day]) AS source
ON target.[Day] = source.[Day]
WHEN MATCHED THEN
    UPDATE SET [Count] = [Count] + 1, [UpdatedAt] = {now}
WHEN NOT MATCHED THEN
    INSERT ([Id], [Day], [Count], [CreatedAt], [UpdatedAt], [IsDeleted])
    VALUES ({id}, {day}, 1, {now}, {now}, CAST(0 AS bit));
""", ct);
    }

    public async Task<(int Daily, int Monthly, int Yearly)> GetSummaryAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var today = now.Date;
        var monthStart = new DateTime(now.Year, now.Month, 1);
        var yearStart = new DateTime(now.Year, 1, 1);

        var daily = await _db.DailyVisits.Where(x => x.Day == today).Select(x => x.Count).FirstOrDefaultAsync(ct);
        var monthly = await _db.DailyVisits.Where(x => x.Day >= monthStart && x.Day <= today).SumAsync(x => (int?)x.Count, ct) ?? 0;
        var yearly = await _db.DailyVisits.Where(x => x.Day >= yearStart && x.Day <= today).SumAsync(x => (int?)x.Count, ct) ?? 0;

        return (daily, monthly, yearly);
    }
}

