using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Donations;
using SahabatAnak.Api.Models;
using SahabatAnak.Api.Repositories;

namespace SahabatAnak.Api.Services;

public sealed class DonationsService : IDonationsService
{
    private readonly IGenericRepository<Donation> _donations;

    public DonationsService(IGenericRepository<Donation> donations)
    {
        _donations = donations;
    }

    public async Task<SubmitDonationResponseDto> SubmitAsync(SubmitDonationRequestDto dto, CancellationToken ct = default)
    {
        var amount = dto.Amount;
        if (amount <= 0) throw new ArgumentException("Nominal donasi tidak valid.");

        var bank = (dto.RecipientBank ?? string.Empty).Trim();
        var accNo = (dto.RecipientAccountNumber ?? string.Empty).Trim();
        var accName = (dto.RecipientAccountName ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(bank) || string.IsNullOrWhiteSpace(accNo) || string.IsNullOrWhiteSpace(accName))
        {
            throw new ArgumentException("Rekening penerima wajib diisi.");
        }

        var entity = new Donation
        {
            Amount = amount,
            Method = (dto.Method ?? string.Empty).Trim(),
            DonorName = (dto.Name ?? string.Empty).Trim(),
            DonorEmail = (dto.Email ?? string.Empty).Trim(),
            DonorPhone = (dto.Phone ?? string.Empty).Trim(),
            RecipientBank = bank,
            RecipientAccountNumber = accNo,
            RecipientAccountName = accName,
            ReferenceCode = (dto.ReferenceCode ?? string.Empty).Trim(),
            ProofUrl = (dto.ProofUrl ?? string.Empty).Trim(),
            Note = (dto.Note ?? string.Empty).Trim(),
            Status = "baru",
        };

        await _donations.AddAsync(entity, ct);
        await _donations.SaveChangesAsync(ct);
        return new SubmitDonationResponseDto { Id = entity.Id };
    }

    public async Task<PagedResultDto<DonationDto>> ListAsync(string? status, int page, int pageSize, CancellationToken ct = default)
    {
        var safePage = Math.Max(1, page);
        var safeSize = Math.Clamp(pageSize, 1, 50);

        var query = _donations.Query();
        if (!string.IsNullOrWhiteSpace(status))
        {
            var s = status.Trim();
            query = query.Where(x => x.Status == s);
        }

        var total = await query.CountAsync(ct);
        var totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)safeSize));
        var clampedPage = Math.Min(safePage, totalPages);
        var skip = (clampedPage - 1) * safeSize;

        var data = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip(skip)
            .Take(safeSize)
            .Select(x => new DonationDto
            {
                Id = x.Id,
                Amount = x.Amount,
                Method = x.Method,
                DonorName = x.DonorName,
                DonorEmail = x.DonorEmail,
                DonorPhone = x.DonorPhone,
                RecipientBank = x.RecipientBank,
                RecipientAccountNumber = x.RecipientAccountNumber,
                RecipientAccountName = x.RecipientAccountName,
                ReferenceCode = x.ReferenceCode,
                ProofUrl = x.ProofUrl,
                Note = x.Note,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync(ct);

        return new PagedResultDto<DonationDto>
        {
            Data = data,
            Page = clampedPage,
            PageSize = safeSize,
            Total = total,
            TotalPages = totalPages,
        };
    }

    public async Task<DonationDto?> UpdateStatusAsync(Guid id, string status, CancellationToken ct = default)
    {
        var entity = await _donations.GetByIdAsync(id, ct);
        if (entity is null) return null;

        var next = (status ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(next)) throw new ArgumentException("Status tidak valid.");

        entity.Status = next;
        _donations.Update(entity);
        await _donations.SaveChangesAsync(ct);

        return new DonationDto
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Method = entity.Method,
            DonorName = entity.DonorName,
            DonorEmail = entity.DonorEmail,
            DonorPhone = entity.DonorPhone,
            RecipientBank = entity.RecipientBank,
            RecipientAccountNumber = entity.RecipientAccountNumber,
            RecipientAccountName = entity.RecipientAccountName,
            ReferenceCode = entity.ReferenceCode,
            ProofUrl = entity.ProofUrl,
            Note = entity.Note,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
        };
    }

    public async Task<(decimal Total, decimal Monthly, int Count)> GetSummaryAsync(CancellationToken ct = default)
    {
        var query = _donations.Query();
        var total = await query.SumAsync(x => (decimal?)x.Amount, ct) ?? 0m;
        var count = await query.CountAsync(ct);

        var now = DateTime.UtcNow;
        var monthStart = new DateTime(now.Year, now.Month, 1);
        var monthly = await query.Where(x => x.CreatedAt >= monthStart).SumAsync(x => (decimal?)x.Amount, ct) ?? 0m;

        return (total, monthly, count);
    }
}
