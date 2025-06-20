using ExpenseTracker.Domain.Enums;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.Category.Create;

public record CreateCategoryCommand(CategoryType Type, string Name) : IRequest;
