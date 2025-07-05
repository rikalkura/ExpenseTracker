using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.UserPage.Specifications;

public class GetUserPageByUserId : SingleResultSpecification<UserPageEntity>
{
    public GetUserPageByUserId(Guid userId)
    {
        Query.Where(p => p.UserId == userId);
    }
}
