using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.Category.Specifications;

public class UserCategoryByIdSpecification : SingleResultSpecification<CategoryEntity>
{
    public UserCategoryByIdSpecification(Guid categoryId, Guid userId)
    {
        Query.Where(c => c.Id == categoryId && c.UserId == userId);
    }
}
