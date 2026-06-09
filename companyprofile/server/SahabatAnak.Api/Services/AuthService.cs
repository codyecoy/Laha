using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SahabatAnak.Api.DTOs.Auth;
using SahabatAnak.Api.Models;
using SahabatAnak.Api.Repositories;

namespace SahabatAnak.Api.Services;

public sealed class AuthService : IAuthService
{
    private readonly IGenericRepository<AppUser> _users;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<AppUser> _hasher = new();

    public AuthService(IGenericRepository<AppUser> users, IConfiguration config)
    {
        _users = users;
        _config = config;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken ct = default)
    {
        var email = (request.Email ?? string.Empty).Trim().ToLowerInvariant();
        var user = await _users.Query().FirstOrDefaultAsync(x => x.Email.ToLower() == email, ct);
        if (user is null) return null;

        var verification = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password ?? string.Empty);
        if (verification == PasswordVerificationResult.Failed) return null;

        var issuer = _config["Jwt:Issuer"] ?? "SahabatAnak";
        var audience = _config["Jwt:Audience"] ?? "SahabatAnak";
        var key = _config["Jwt:Key"] ?? "DEV_ONLY_CHANGE_ME_TO_A_LONG_RANDOM_STRING";
        var expiresMinutes = int.TryParse(_config["Jwt:ExpiresMinutes"], out var minutes) ? minutes : 480;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("name", user.Name),
            new(ClaimTypes.Role, user.Role),
        };

        var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddMinutes(expiresMinutes), signingCredentials: creds);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseDto
        {
            Token = tokenString,
            User = new AdminUserDto { Email = user.Email, Name = user.Name, Role = user.Role },
        };
    }
}

