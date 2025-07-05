using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ExpenseTracker.Application.Behaviors.Transaction.Specification;

public class GetTransactionsByIdsSpecification : Specification<TransactionEntity>
{
    public GetTransactionsByIdsSpecification(IEnumerable<int> ids)
    {
        Query.Where(t => ids.Contains(t.Id));
    }
}
