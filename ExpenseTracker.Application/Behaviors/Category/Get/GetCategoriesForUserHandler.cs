using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;
using System.Linq;

namespace ExpenseTracker.Application.Behaviors.Category.Get;

public class GetCategoriesForUserHandler(
    ICategoryRepository categoryRepository,
    ICurrentUserService currentUserService) 
    : IRequestHandler<GetCategoriesForUserQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetCategoriesForUserQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.ListAsync(cancellationToken);

        var userCategories = categories
            .Where(c => c.UserId == currentUserService.Id || c.UserId is null)
            .Select(c => new CategoryDto(c.Id, c.Type, c.Name, c.UserId)).ToList();

        return userCategories;
    }
}
