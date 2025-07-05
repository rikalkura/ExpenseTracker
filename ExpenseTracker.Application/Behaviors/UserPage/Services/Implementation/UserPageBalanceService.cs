using ExpenseTracker.Application.Behaviors.Transaction.Specification;
using ExpenseTracker.Application.Behaviors.UserPage.Services.Abstraction;
using ExpenseTracker.Application.Behaviors.UserPage.Specifications;
using ExpenseTracker.Domain.Interfaces.Repositories;

namespace ExpenseTracker.Application.Behaviors.UserPage.Services.Implementation;

public class UserPageBalanceService (
    ITransactionRepository transactionRepository,
    IUserPageRepository userPageRepository) 
    : IUserPageBalanceService
{
    public async Task RecalculateAsync(Guid userPageId, CancellationToken cancellationToken = default)
    {
        var transactions = await transactionRepository
            .ListAsync(
            new GetTransactionsByUserPageIdSpecification(userPageId),
            cancellationToken);

        decimal income = transactions
            .Where(t => t.Amount > 0)
            .Sum(t => t.Amount);

        decimal expenses = transactions
            .Where(t => t.Amount < 0)
            .Sum(t => t.Amount);

        decimal balance = transactions.Sum(t => t.Amount);

        var userPage = await userPageRepository.FirstOrDefaultAsync(
            new GetUserPageByIdSpecification(userPageId),
            cancellationToken);
        if (userPage == null) throw new Exception("UserPage not found");

        userPage.SetBalance(balance, expenses, income);

        userPageRepository.Update(userPage);
        await userPageRepository.SaveChangesAsync(cancellationToken);
    }
}
