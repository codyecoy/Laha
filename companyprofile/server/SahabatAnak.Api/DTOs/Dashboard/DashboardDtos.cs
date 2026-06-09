namespace SahabatAnak.Api.DTOs.Dashboard;

public sealed class DashboardStatsDto
{
    public required int TotalReports { get; init; }
    public required Dictionary<string, int> ReportsByStatus { get; init; }
    public required int NewContacts { get; init; }
    public required int VisitorsDaily { get; init; }
    public required int VisitorsMonthly { get; init; }
    public required int VisitorsYearly { get; init; }
    public required decimal DonationsTotal { get; init; }
    public required decimal DonationsMonthly { get; init; }
    public required IReadOnlyList<MonthlyPointDto> Chart { get; init; }
    public required IReadOnlyList<RecentReportDto> RecentReports { get; init; }
    public required IReadOnlyList<RecentContactDto> RecentContacts { get; init; }
}

public sealed class MonthlyPointDto
{
    public required string Key { get; init; }
    public required int Count { get; init; }
}

public sealed class RecentReportDto
{
    public required Guid Id { get; init; }
    public required string Chronology { get; init; }
    public required string Status { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public sealed class RecentContactDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Subject { get; init; }
    public required string Status { get; init; }
    public required DateTime CreatedAt { get; init; }
}
