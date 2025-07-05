using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.Category.Specifications;

public class CategoryByIdSpecification : SingleResultSpecification<CategoryEntity>
{
    public CategoryByIdSpecification(Guid categoryId)
    {
        Query.Where(c => c.Id == categoryId);
    }
}
