using MediatR;

namespace ExpenseTracker.Application.Behaviors.Category.Delete;

public record DeleteCategoryCommand(Guid Id) : IRequest;
