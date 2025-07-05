using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.Transaction.Specification;

public class GetTransactionsByUserPageIdSpecification : SingleResultSpecification<TransactionEntity>
{
    public GetTransactionsByUserPageIdSpecification(Guid userPageId)
    {
        Query.Where(t => t.UserPageId == userPageId);
    }
}
