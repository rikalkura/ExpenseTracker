using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.Transaction.Specification;

public class GetTransactionByIdSpecification : SingleResultSpecification<TransactionEntity>
{
    public GetTransactionByIdSpecification(int id)
    {
        Query.Where(t => t.Id == id);
    }
}
