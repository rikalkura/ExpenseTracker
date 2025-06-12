using ExpenseTracker.Domain.Dto;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.User.Get;

public record GetUserInfoQuery(string id) : IRequest<UserDto>
{
    public Guid GuidUserId => Guid.Parse(id);
}
