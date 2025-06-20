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
        var category = await categoryRepository.FirstOrDefaultAsync(new UserCategoryByIdSpec(request.Id, currentUserService.Id));

        if (category is null) 
        {
            throw new NotFoundException($"Category with ID - {request.Id} - cannot be found.");
        }

        category.SetName(request.Name);

        await EnsureIsUniqueAsync(category);

        categoryRepository.Update(category);

        await categoryRepository.SaveChangesAsync();
    }

    private async Task EnsureIsUniqueAsync(CategoryEntity category)
    {
        var userId = currentUserService.Id;

        var isExist = await categoryRepository.AnyAsync(c =>
           (c.UserId == userId || c.UserId == null) &&
           c.Type == category.Type &&
           EF.Functions.Like(c.Name, category.Name));

        if (isExist)
        {
            throw new ApiException(
                HttpStatusCode.Conflict,
                "Category conflict",
                "Such a category already exists!");
        }
    }
}
