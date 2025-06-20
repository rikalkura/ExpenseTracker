using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Dto;

public class CategoryDto
{
    public CategoryType Type { get; set; }
    public string Name { get; set; }
}
