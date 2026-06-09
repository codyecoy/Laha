using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Common;
using SahabatAnak.Api.DTOs.Contacts;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin,Operator")]
[Route("api/admin/contacts")]
public sealed class AdminContactsController : ControllerBase
{
    private readonly IContactsService _contacts;

    public AdminContactsController(IContactsService contacts)
    {
        _contacts = contacts;
    }

    [HttpGet]
    public Task<PagedResultDto<ContactDto>> List(
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
        => _contacts.ListAsync(status, page, pageSize, ct);

    [HttpPut("{id:guid}/status")]
    public async Task<ActionResult<ContactDto>> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateContactStatusRequestDto dto, CancellationToken ct)
    {
        var updated = await _contacts.UpdateStatusAsync(id, dto.Status, ct);
        if (updated is null) return NotFound();
        return Ok(updated);
    }
}
