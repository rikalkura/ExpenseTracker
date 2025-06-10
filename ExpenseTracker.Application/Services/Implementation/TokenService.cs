using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Configurations;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseTracker.Application.Services.Implementation;

public class TokenService(
    IOptions<TokenConfiguration> jwtOptions) : ITokenService
{
    private readonly TokenConfiguration tokenConfig = jwtOptions.Value;

    public TokenResponseDto Generate(UserEntity user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        return new TokenResponseDto(accessToken, refreshToken);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    private string GenerateAccessToken(UserEntity user)
    {
        var claims = new[]
        {
            new Claim   (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: tokenConfig.Issuer,
            audience: tokenConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(tokenConfig.AccessTokenExpirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
