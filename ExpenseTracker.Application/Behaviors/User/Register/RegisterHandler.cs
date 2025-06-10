using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Application.Behaviors.User.Register;

public class RegisterHandler(
    UserManager<UserEntity> userManager,
    ITokenService tokenService) :
    IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var newUser = new UserEntity
        {
            Email = request.Email,
            UserName = request.Email.ToUsername(),
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender
        };

        var result = await userManager.CreateAsync(
            newUser,
            request.Password);

        if (!result.Succeeded)
        {
            throw new IdentityOperationException("User register error.");
        }

        return "User was registered successfully!";
    }
}
