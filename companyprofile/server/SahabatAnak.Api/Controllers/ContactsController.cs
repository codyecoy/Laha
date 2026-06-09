using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Contacts;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers;

[ApiController]
[Route("api/contacts")]
public sealed class ContactsController : ControllerBase
{
    private readonly IContactsService _contacts;

    public ContactsController(IContactsService contacts)
    {
        _contacts = contacts;
    }

    [HttpPost]
    public Task<ContactDto> Submit([FromBody] SubmitContactRequestDto dto, CancellationToken ct)
        => _contacts.SubmitAsync(dto, ct);
}

