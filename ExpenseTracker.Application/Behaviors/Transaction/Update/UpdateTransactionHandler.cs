using ExpenseTracker.Application.Behaviors.Category.Specifications;
using ExpenseTracker.Application.Behaviors.Transaction.Extensions;
using ExpenseTracker.Application.Behaviors.Transaction.Services;
using ExpenseTracker.Application.Behaviors.Transaction.Specification;
using ExpenseTracker.Application.Behaviors.UserPage.Services.Abstraction;
using ExpenseTracker.Application.Behaviors.UserPage.Specifications;
using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.Transaction.Update;

public class UpdateTransactionHandler(
    ITransactionRepository transactionRepository,
    ICurrentUserService currentUserService,
    IUserPageRepository userPageRepository,
    ICategoryRepository categoryRepository,
    ITransactionBalanceService transactionBalanceService,
    IUserPageBalanceService userPageBalanceService) : IRequestHandler<UpdateTransactionCommand>
{
    public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var userPage = await GetUserPageAsync(cancellationToken);

        var transaction = await transactionRepository.FirstOrDefaultAsync(
            new GetTransactionByIdSpecification(request.Id),
            cancellationToken);

        if (transaction is null)
        {
            throw new NotFoundException($"Transaction with ID - {request.Id} - does not exist!");
        }

        var category = await GetCategoryAsync(transaction.CategoryId, cancellationToken);

        var signedAmount = request.Amount.ToSignedAmount(category.Type);

        transaction.Update(
            signedAmount,
            request.Date,
            request.Comment);

        transactionRepository.Update(transaction);
        await transactionRepository.SaveChangesAsync(cancellationToken);

        await userPageBalanceService.RecalculateAsync(userPage.Id, cancellationToken);

        await transactionBalanceService.AdjustBalancesAsync(
            request.Date,
            transaction.UserPageId,
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
    private async Task<CategoryEntity> GetCategoryAsync(Guid? categoryId, CancellationToken cancellationToken)
    {
        if (categoryId is null)
        {
            throw new NotFoundException("Transaction does not have a category assigned.");
        }

        var category = await categoryRepository.FirstAsync(
            new CategoryByIdSpecification(categoryId.Value),
            cancellationToken);

        return category;
    }
}
