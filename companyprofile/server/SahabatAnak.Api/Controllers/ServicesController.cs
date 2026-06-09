using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Services;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers;

[ApiController]
[Route("api/services")]
public sealed class ServicesController : ControllerBase
{
    private readonly IServicesService _services;

    public ServicesController(IServicesService services)
    {
        _services = services;
    }

    [HttpGet]
    public Task<IReadOnlyList<ServiceDto>> List(CancellationToken ct)
        => _services.ListAsync(ct);
}

