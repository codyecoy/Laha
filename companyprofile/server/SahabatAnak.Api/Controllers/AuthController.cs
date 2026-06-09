using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Auth;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request, CancellationToken ct)
    {
        var res = await _auth.LoginAsync(request, ct);
        if (res is null) return Unauthorized(new { message = "Email atau password salah." });
        return Ok(res);
    }
}

