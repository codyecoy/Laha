using SahabatAnak.Api.DTOs.Auth;

namespace SahabatAnak.Api.Services;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken ct = default);
}

