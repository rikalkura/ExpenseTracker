using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Application.Behaviors.User.Update;

public class UpdateUserInfoHandler(
    UserManager<UserEntity> userManager) 
    : IRequestHandler<UpdateUserInfoCommand>
{
    public async Task Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var userInfo = await userManager.FindByIdAsync(request.Id.ToString());

        if (userInfo is null)
        {
            throw new NotFoundException($"User with ID - {request.Id} - cannot be found.");
        }

        userInfo.PhoneNumber = request.PhoneNumber;
        userInfo.Gender = request.Gender;


        var result = await userManager.UpdateAsync(userInfo);
    }
}
