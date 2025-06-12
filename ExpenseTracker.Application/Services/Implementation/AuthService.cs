using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Application.Services.Implementation;

public class AuthService(
    UserManager<UserEntity> userManager,
    IJwtTokenProvider tokenService,
    ITokenRepository tokenRepository) 
    : IAuthService
{
    public async Task<TokenResponseDto> LoginAsync(LoginRequestDto request, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new NotAuthorizedException("Ivalid username or password!");
        }

        var tokenDto = tokenService.Generate(user);

        var refreshTokenEntity = new RefreshTokenEntity(
            tokenDto.RefreshToken,
            user.Id);

        tokenRepository.Add(refreshTokenEntity);

        await tokenRepository.SaveChangesAsync();

        return tokenDto;
    }
}
