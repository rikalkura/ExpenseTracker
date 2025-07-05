using ExpenseTracker.Application.Behaviors.Transaction.Services;
using ExpenseTracker.Application.Behaviors.Transaction.Specification;
using ExpenseTracker.Application.Behaviors.UserPage.Services.Abstraction;
using ExpenseTracker.Application.Behaviors.UserPage.Specifications;
using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.Transaction.Delete;

public class DeleteTransactionHandler(
    ITransactionRepository transactionRepository,
    IUserPageRepository userPageRepository,
    ICurrentUserService currentUserService,
    ITransactionBalanceService transactionBalanceService,
    IUserPageBalanceService userPageBalanceService) 
    : IRequestHandler<DeleteTransactionCommand>
{
    public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var userPage = await GetUserPageAsync(cancellationToken);

        var transactions = await transactionRepository.ListAsync(
            new GetTransactionsByIdsSpecification(request.Ids),
            cancellationToken);

        if (!transactions.Any())
        {
            throw new NotFoundException($"Some of transactions cannot be found.");
        }

        foreach (var transaction in transactions)
        {
            transactionRepository.Delete(transaction);
        }
        
        await transactionRepository.SaveChangesAsync(cancellationToken);

        await userPageBalanceService.RecalculateAsync(userPage.Id, cancellationToken);

        var earliestDate = transactions
            .Select(t => t.Date)
            .Min();

        await transactionBalanceService.AdjustBalancesAsync(
            earliestDate,
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
}
