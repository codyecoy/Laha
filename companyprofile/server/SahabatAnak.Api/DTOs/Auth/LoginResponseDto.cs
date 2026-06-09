namespace SahabatAnak.Api.DTOs.Auth;

public sealed class LoginResponseDto
{
    public required string Token { get; init; }
    public required AdminUserDto User { get; init; }
}

public sealed class AdminUserDto
{
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string Role { get; init; }
}

