using ExpenseTracker.Application.Behaviors.UserPage.Specifications;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.UserPage.Get;

public class GetUserPageForUserHandler(IUserPageRepository userPageRepository) : IRequestHandler<GetUserPageForUserQuery, UserPageDto>
{
    public async Task<UserPageDto> Handle(GetUserPageForUserQuery request, CancellationToken cancellationToken)
    {
        var userPage = await userPageRepository.FirstOrDefaultAsync(
            new GetUserPageByUserId(request.UserId),
            cancellationToken);

        if (userPage == null) 
        {
            throw new NotFoundException($"No UserPage for this UserID - {request.UserId}");
        }

        return new UserPageDto(
            userPage.Balance,
            userPage.TotalExpenses,
            userPage.TotalIncome);
    }
}
