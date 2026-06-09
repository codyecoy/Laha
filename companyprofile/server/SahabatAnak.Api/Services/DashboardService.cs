using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.DTOs.Dashboard;
using SahabatAnak.Api.Models;
using SahabatAnak.Api.Repositories;

namespace SahabatAnak.Api.Services;

public sealed class DashboardService : IDashboardService
{
    private readonly IGenericRepository<CaseReport> _reports;
    private readonly IGenericRepository<ContactMessage> _contacts;
    private readonly IVisitsService _visits;
    private readonly IDonationsService _donations;

    public DashboardService(
        IGenericRepository<CaseReport> reports,
        IGenericRepository<ContactMessage> contacts,
        IVisitsService visits,
        IDonationsService donations)
    {
        _reports = reports;
        _contacts = contacts;
        _visits = visits;
        _donations = donations;
    }

    public async Task<DashboardStatsDto> GetAsync(CancellationToken ct = default)
    {
        var totalReports = await _reports.Query().CountAsync(ct);
        var reportsByStatus = await _reports.Query()
            .GroupBy(x => x.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Status, x => x.Count, ct);

        var newContacts = await _contacts.Query().CountAsync(x => x.Status == "baru", ct);
        var (visitorsDaily, visitorsMonthly, visitorsYearly) = await _visits.GetSummaryAsync(ct);
        var (donationsTotal, donationsMonthly, _) = await _donations.GetSummaryAsync(ct);

        var now = DateTime.UtcNow;
        string MonthKey(DateTime d) => $"{d.Year:D4}-{d.Month:D2}";
        var buckets = Enumerable.Range(0, 6)
            .Select(i => new DateTime(now.Year, now.Month, 1).AddMonths(-i))
            .OrderBy(d => d)
            .Select(d => new MonthlyPointDto { Key = MonthKey(d), Count = 0 })
            .ToList();

        var bucketMap = buckets.ToDictionary(x => x.Key, x => x);
        var start = new DateTime(now.Year, now.Month, 1).AddMonths(-5);
        var monthlyCounts = await _reports.Query()
            .Where(x => x.CreatedAt >= start)
            .GroupBy(x => new { x.CreatedAt.Year, x.CreatedAt.Month })
            .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
            .ToListAsync(ct);

        foreach (var row in monthlyCounts)
        {
            var key = $"{row.Year:D4}-{row.Month:D2}";
            if (bucketMap.TryGetValue(key, out var b))
            {
                bucketMap[key] = new MonthlyPointDto { Key = b.Key, Count = row.Count };
            }
        }

        var chart = buckets.Select(b => bucketMap[b.Key]).ToList();

        var recentReports = await _reports.Query()
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .Select(x => new RecentReportDto { Id = x.Id, Chronology = x.Chronology, Status = x.Status, CreatedAt = x.CreatedAt })
            .ToListAsync(ct);

        var recentContacts = await _contacts.Query()
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .Select(x => new RecentContactDto { Id = x.Id, Name = x.Name, Email = x.Email, Subject = x.Subject, Status = x.Status, CreatedAt = x.CreatedAt })
            .ToListAsync(ct);

        return new DashboardStatsDto
        {
            TotalReports = totalReports,
            ReportsByStatus = reportsByStatus,
            NewContacts = newContacts,
            VisitorsDaily = visitorsDaily,
            VisitorsMonthly = visitorsMonthly,
            VisitorsYearly = visitorsYearly,
            DonationsTotal = donationsTotal,
            DonationsMonthly = donationsMonthly,
            Chart = chart,
            RecentReports = recentReports,
            RecentContacts = recentContacts,
        };
    }
}

