using ExpenseTracker.Domain.Dto;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.Category.Get;

public record GetCategoriesForUserQuery() : IRequest<List<CategoryDto>>;
