using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.Transaction.Specifications;

public class GetTransactionsByCategoryIdSpecification : Specification<TransactionEntity>
{
    public GetTransactionsByCategoryIdSpecification(Guid categoryId)
    {
        Query.Where(t => t.CategoryId == categoryId);
    }
}
