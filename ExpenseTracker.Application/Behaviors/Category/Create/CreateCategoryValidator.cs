using ExpenseTracker.Application.Behaviors.Category.Create;
using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ExpenseTracker.Application.Validators.Categories;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator(
        ICategoryRepository categoryRepository,
        ICurrentUserService currentUserService)
    {
        RuleFor(x => x)
            .MustAsync(async (command, cancellationToken) =>
            {
                var userId = currentUserService.Id;

                var existingCategories = await categoryRepository.GetAllAsync();

                return !existingCategories.Any(c =>
                    c.UserId == userId &&
                    c.Type == command.Type &&
                    c.Name.Equals(command.Name, StringComparison.OrdinalIgnoreCase));
            })
            .WithMessage("Category with this name and type already exists for this user.");
    }
}
