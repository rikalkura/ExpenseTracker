using ExpenseTracker.Application.Behaviors.Transaction.Specification;
using ExpenseTracker.Domain.Interfaces.Repositories;

namespace ExpenseTracker.Application.Behaviors.Transaction.Services;

public interface ITransactionBalanceService
{
    Task AdjustBalancesAsync(
        DateOnly date,
        Guid userPageId,
        CancellationToken cancellationToken);
}

public class TransactionBalanceService(ITransactionRepository transactionRepository) : ITransactionBalanceService
{
    public async Task AdjustBalancesAsync(
        DateOnly date,
        Guid userPageId,
        CancellationToken cancellationToken)
    {
        var nextTransactions = await transactionRepository.ListAsync(
            new GetTransactionsAfterAnchorSpecification(date, userPageId),
            cancellationToken);

        if (!nextTransactions.Any())
        {
            return;
        }

        var anchorTransaction = await transactionRepository.FirstOrDefaultAsync(
            new GetAnchorTransactionSpecification(date, userPageId),
            cancellationToken);

        decimal anchorBalance;

        if (anchorTransaction is not null)
        {
            anchorBalance = anchorTransaction.BalanceBeforeTransaction + anchorTransaction.Amount;
        }
        else
        {
            anchorBalance = nextTransactions.First().BalanceBeforeTransaction;
        }

        foreach(var transaction in nextTransactions)
        {
            transaction.SetBalanceBeforeTransaction(anchorBalance);

            transactionRepository.Update(transaction);

            anchorBalance += transaction.Amount;
        }

        await transactionRepository.SaveChangesAsync(cancellationToken);
    }
}
