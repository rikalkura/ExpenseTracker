using ExpenseTracker.Domain.Dto;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.UserPage.Get;

public record GetUserPageForUserQuery(Guid UserId) : IRequest<UserPageDto>;
