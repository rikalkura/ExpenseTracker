using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Application.Behaviors.User.Register;

public class RegisterHandler(UserManager<UserEntity> userManager) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(
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
    }
}
