using ExpenseTracker.Domain.Core;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Domain.Exceptions;

namespace ExpenseTracker.Domain.Entities;

public class CategoryEntity : BaseEntity
{
    public CategoryEntity(
        CategoryType type,
        string name,
        Guid userId)
    {
        EnsureNameIsValid(name);

        Guard.IsFalse(type.IsInEnum(), new ValidationException("Invalid category type."));
        Guard.NotEqual(type, CategoryType.None, new ValidationException("Category type cannot be 'None'."));

        Type = type;
        Name = name;
        UserId = userId;
    }

    private CategoryEntity()
    {
    }

    public CategoryType Type { get; private set; }
    public string Name { get; private set; }
    public Guid? UserId { get; private set; }

    public void SetName(string name)
    {
        EnsureNameIsValid(name);

        Name = name;
    }

    private void EnsureNameIsValid(string name)
    {
        Guard.IsFalse(name.IsNullOrEmpty(), new ValidationException("Category name is required."));
        Guard.IsFalse(name.Length > 35, new ValidationException("Category name must be at most 35 characters long."));
    }
}
