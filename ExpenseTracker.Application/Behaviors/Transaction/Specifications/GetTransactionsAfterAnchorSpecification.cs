using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.Transaction.Specification;

public class GetTransactionsAfterAnchorSpecification : SingleResultSpecification<TransactionEntity>
{
    public GetTransactionsAfterAnchorSpecification(DateOnly anchor, Guid userPageId)
    {
        Query.Where(t => t.UserPageId == userPageId && t.Date >= anchor).OrderBy(t => t.Date).ThenBy(t => t.Id); ;
    }
}
