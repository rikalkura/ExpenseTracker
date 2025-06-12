using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Application.Behaviors.User.Get;

public class GetUserInfoHandler(
    UserManager<UserEntity> userManager) 
    : IRequestHandler<GetUserInfoQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var userInfo = await userManager.FindByIdAsync(request.GuidUserId.ToString());

        if (userInfo is null)
        {
            throw new NotFoundException($"User with ID - {request.GuidUserId} - cannot be found.");
        }

        return new UserDto
        {
            Email = userInfo.Email,
            UserName = userInfo.UserName,
            PhoneNumber = userInfo.PhoneNumber,
            Gender = userInfo.Gender
        };
    }
}
