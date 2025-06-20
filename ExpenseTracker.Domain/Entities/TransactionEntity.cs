namespace ExpenseTracker.Domain.Entities;

public class TransactionEntity : BaseEntity
{
    public TransactionEntity(
        decimal amount,
        decimal balanceBeforeTransaction,
        DateTime date,
        Guid categoryId,
        Guid userPageId)
    {
        Amount = amount;
        BalanceBeforeTransaction = balanceBeforeTransaction;
        Date = date;
        CategoryId = categoryId;
        UserPageId = userPageId;
    }
    public decimal Amount { get; private set; }
    public decimal BalanceBeforeTransaction { get; private set; }
    public DateTime Date{ get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid UserPageId { get; private set; }
}
