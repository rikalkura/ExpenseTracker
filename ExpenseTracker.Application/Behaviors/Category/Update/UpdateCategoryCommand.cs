using MediatR;

namespace ExpenseTracker.Application.Behaviors.Category.Update;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest;
