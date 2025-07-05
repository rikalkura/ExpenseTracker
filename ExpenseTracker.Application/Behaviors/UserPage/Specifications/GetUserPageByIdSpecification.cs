using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.UserPage.Specifications;

public class GetUserPageByIdSpecification : SingleResultSpecification<UserPageEntity>
{
    public GetUserPageByIdSpecification(Guid userPageId)
    {
        Query.Where(p => p.Id == userPageId);
    }
}
