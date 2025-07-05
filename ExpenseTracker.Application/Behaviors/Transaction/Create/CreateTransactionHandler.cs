using ExpenseTracker.Application.Behaviors.Category.Specifications;
using ExpenseTracker.Application.Behaviors.Transaction.Extensions;
using ExpenseTracker.Application.Behaviors.Transaction.Services;
using ExpenseTracker.Application.Behaviors.UserPage.Services.Abstraction;
using ExpenseTracker.Application.Behaviors.UserPage.Specifications;
using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.Transaction.Create;

public class CreateTransactionHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository,
    IUserPageRepository userPageRepository,
    ICurrentUserService currentUserService,
    ITransactionBalanceService transactionBalanceService,
    IUserPageBalanceService userPageBalanceService) :
    IRequestHandler<CreateTransactionCommand>
{
    public async Task Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var userPage = await GetUserPageAsync(cancellationToken);

        var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

        var signedAmount = request.Amount.ToSignedAmount(category.Type);

        var transaction = new TransactionEntity(
            signedAmount,
            0,
            request.Date,
            request.Comment,
            request.CategoryId,
            userPage.Id);

        transactionRepository.Add(transaction);
        await transactionRepository.SaveChangesAsync(cancellationToken);

        await userPageBalanceService.RecalculateAsync(userPage.Id, cancellationToken);

        await transactionBalanceService.AdjustBalancesAsync(
            request.Date,
            userPage.Id,
            cancellationToken);
    }

    private async Task<UserPageEntity> GetUserPageAsync(CancellationToken cancellationToken)
    {
        var userId = currentUserService.Id;

        var userPage = await userPageRepository.FirstOrDefaultAsync(
            new GetUserPageByUserId(userId),
            cancellationToken);

        if (userPage is null)
        {
            throw new NotFoundException("User page does not exist");
        }

        return userPage;
    }

    private async Task<CategoryEntity> GetCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FirstOrDefaultAsync(
            new CategoryByIdSpecification(categoryId),
            cancellationToken);

        if (category is null)
        {
            throw new NotFoundException("Category does not exist");
        }

        return category;
    }
}
