using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Reports;
using SahabatAnak.Api.Models;
using SahabatAnak.Api.Repositories;

namespace SahabatAnak.Api.Services;

public sealed class ReportsService : IReportsService
{
    private readonly IGenericRepository<CaseReport> _reports;

    public ReportsService(IGenericRepository<CaseReport> reports)
    {
        _reports = reports;
    }

    public async Task<SubmitReportResponseDto> SubmitAsync(SubmitReportRequestDto dto, CancellationToken ct = default)
    {
        var isAnonymous = dto.IsAnonymous;
        var chronology = (dto.Chronology ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(chronology)) throw new ArgumentException("Kronologi wajib diisi.");

        var entity = new CaseReport
        {
            IsAnonymous = isAnonymous,
            ReporterName = isAnonymous ? string.Empty : (dto.Name ?? string.Empty).Trim(),
            ReporterContact = isAnonymous ? string.Empty : (dto.Contact ?? string.Empty).Trim(),
            Chronology = chronology,
            EvidenceUrls = dto.EvidenceUrls?.Take(3).ToList() ?? new List<string>(),
            Status = "baru",
        };

        await _reports.AddAsync(entity, ct);
        await _reports.SaveChangesAsync(ct);

        return new SubmitReportResponseDto { Id = entity.Id };
    }

    public async Task<PagedResultDto<ReportDto>> ListAsync(string? status, int page, int pageSize, CancellationToken ct = default)
    {
        var safePage = Math.Max(1, page);
        var safeSize = Math.Clamp(pageSize, 1, 50);

        var query = _reports.Query();
        if (!string.IsNullOrWhiteSpace(status))
        {
            var s = status.Trim();
            query = query.Where(x => x.Status == s);
        }

        query = query.OrderByDescending(x => x.CreatedAt);

        var total = await query.CountAsync(ct);
        var totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)safeSize));
        var clampedPage = Math.Min(safePage, totalPages);
        var skip = (clampedPage - 1) * safeSize;

        var data = await query.Skip(skip).Take(safeSize).Select(x => new ReportDto
        {
            Id = x.Id,
            IsAnonymous = x.IsAnonymous,
            Name = x.ReporterName,
            Contact = x.ReporterContact,
            Chronology = x.Chronology,
            EvidenceUrls = x.EvidenceUrls,
            Status = x.Status,
            CreatedAt = x.CreatedAt,
        }).ToListAsync(ct);

        return new PagedResultDto<ReportDto>
        {
            Data = data,
            Page = clampedPage,
            PageSize = safeSize,
            Total = total,
            TotalPages = totalPages,
        };
    }

    public async Task<ReportDto?> UpdateStatusAsync(Guid id, string status, CancellationToken ct = default)
    {
        var entity = await _reports.GetByIdAsync(id, ct);
        if (entity is null) return null;
        entity.Status = (status ?? string.Empty).Trim();
        _reports.Update(entity);
        await _reports.SaveChangesAsync(ct);
        return new ReportDto
        {
            Id = entity.Id,
            IsAnonymous = entity.IsAnonymous,
            Name = entity.ReporterName,
            Contact = entity.ReporterContact,
            Chronology = entity.Chronology,
            EvidenceUrls = entity.EvidenceUrls,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
        };
    }
}

