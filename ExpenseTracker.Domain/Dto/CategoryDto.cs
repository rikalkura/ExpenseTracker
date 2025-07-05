using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Dto;

public record CategoryDto (Guid Id, CategoryType Type, string Name, Guid? UserId);