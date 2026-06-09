using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Contacts;
using SahabatAnak.Api.Models;
using SahabatAnak.Api.Repositories;

namespace SahabatAnak.Api.Services;

public sealed class ContactsService : IContactsService
{
    private readonly IGenericRepository<ContactMessage> _contacts;

    public ContactsService(IGenericRepository<ContactMessage> contacts)
    {
        _contacts = contacts;
    }

    public async Task<ContactDto> SubmitAsync(SubmitContactRequestDto dto, CancellationToken ct = default)
    {
        var entity = new ContactMessage
        {
            Name = (dto.Name ?? string.Empty).Trim(),
            Email = (dto.Email ?? string.Empty).Trim(),
            Subject = dto.Subject ?? string.Empty,
            Message = (dto.Message ?? string.Empty).Trim(),
            Status = "baru",
        };

        await _contacts.AddAsync(entity, ct);
        await _contacts.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<PagedResultDto<ContactDto>> ListAsync(string? status, int page, int pageSize, CancellationToken ct = default)
    {
        var safePage = Math.Max(1, page);
        var safeSize = Math.Clamp(pageSize, 1, 50);

        var query = _contacts.Query();
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

        var data = await query.Skip(skip).Take(safeSize).Select(x => new ContactDto
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            Subject = x.Subject,
            Message = x.Message,
            Status = x.Status,
            CreatedAt = x.CreatedAt,
        }).ToListAsync(ct);

        return new PagedResultDto<ContactDto>
        {
            Data = data,
            Page = clampedPage,
            PageSize = safeSize,
            Total = total,
            TotalPages = totalPages,
        };
    }

    public async Task<ContactDto?> UpdateStatusAsync(Guid id, string status, CancellationToken ct = default)
    {
        var entity = await _contacts.GetByIdAsync(id, ct);
        if (entity is null) return null;
        entity.Status = (status ?? string.Empty).Trim();
        _contacts.Update(entity);
        await _contacts.SaveChangesAsync(ct);
        return Map(entity);
    }

    private static ContactDto Map(ContactMessage x) => new()
    {
        Id = x.Id,
        Name = x.Name,
        Email = x.Email,
        Subject = x.Subject,
        Message = x.Message,
        Status = x.Status,
        CreatedAt = x.CreatedAt,
    };
}

