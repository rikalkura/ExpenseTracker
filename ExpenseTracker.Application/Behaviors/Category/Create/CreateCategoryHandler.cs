using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.Category.Create;

public class CreateCategoryHandler(
    ICategoryRepository categoryRepository,
    ICurrentUserService currentUserService) 
    : IRequestHandler<CreateCategoryCommand>
{
    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CategoryEntity(
            request.Type,
            request.Name,
            currentUserService.Id);

        categoryRepository.Add(category);
        await categoryRepository.SaveChangesAsync();
    }
}
