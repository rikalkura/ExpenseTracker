using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Application.Behaviors.User.Login;

public class LoginHandler(
    UserManager<UserEntity> userManager,
    IJwtTokenProvider tokenService,
    ITokenRepository tokenRepository) : IRequestHandler<LoginCommand, TokenResponseDto>
{
    public async Task<TokenResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new NotAuthorizedException("Invalid username or password!");
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
