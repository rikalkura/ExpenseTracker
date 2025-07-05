using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.Transaction.Specification;

public class GetAnchorTransactionSpecification : SingleResultSpecification<TransactionEntity>
{
    public GetAnchorTransactionSpecification(DateOnly date, Guid userPageId)
    {
        Query.Where(t => t.UserPageId == userPageId && t.Date < date).OrderByDescending(t => t.Date).ThenByDescending(t => t.Id);
    }
}
