using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Behaviors.Category.Specifications;

public class UserCategoryByIdSpec : Specification<CategoryEntity>
{
    public UserCategoryByIdSpec(Guid categoryId, Guid userId)
    {
        Query.Where(c => c.Id == categoryId && c.UserId == userId);
    }

}
