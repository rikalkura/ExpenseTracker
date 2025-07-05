using ExpenseTracker.Application.Behaviors.Category.Specifications;
using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ExpenseTracker.Application.Behaviors.Category.Update;

public class UpdateCategoryHandler(
    ICategoryRepository categoryRepository,
    ICurrentUserService currentUserService) 
    : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FirstOrDefaultAsync(
            new UserCategoryByIdSpecification(request.Id, currentUserService.Id),
            cancellationToken);

        if (category is null) 
        {
            throw new NotFoundException($"Category with ID - {request.Id} - cannot be found.");
        }

        category.SetName(request.Name);

        await EnsureIsUniqueAsync(category, cancellationToken);

        categoryRepository.Update(category);

        await categoryRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureIsUniqueAsync(CategoryEntity category, CancellationToken cancellationToken)
    {
        var userId = currentUserService.Id;

        var isExist = await categoryRepository.AnyAsync(c =>
           (c.UserId == userId || c.UserId == null) &&
           c.Type == category.Type &&
           EF.Functions.Like(c.Name, category.Name),
           cancellationToken);

        if (isExist)
        {
            throw new ApiException(
                HttpStatusCode.Conflict,
                "Category conflict",
                "Such a category already exists!");
        }
    }
}
