namespace ExpenseTracker.Domain.Entities;

public class TransactionEntity : BaseEntity<int>
{
    public TransactionEntity(
        decimal amount,
        decimal balanceBeforeTransaction,
        DateOnly date,
        string comment,
        Guid? categoryId,
        Guid userPageId)
    {
        Amount = amount;
        BalanceBeforeTransaction = balanceBeforeTransaction;
        Date = date;
        Comment = comment;
        CategoryId = categoryId;
        UserPageId = userPageId;
    }
    public decimal Amount { get; private set; }
    public decimal BalanceBeforeTransaction { get; private set; }
    public DateOnly Date{ get; private set; }
    public string Comment { get; private set; }
    public Guid? CategoryId { get; private set; }
    public CategoryEntity Category { get; private set; }
    public Guid UserPageId { get; private set; }


    public void Update(
        decimal amount,
        DateOnly date,
        string comment)
    {
        Amount = amount;
        Date = date;
        Comment = comment;
    }

    public void SetBalanceBeforeTransaction(decimal balanceBeforeTransaction)
    {
        BalanceBeforeTransaction = balanceBeforeTransaction;
    }

    public void SetCategoryId(Guid? categoryId)
    {
        CategoryId = categoryId;
    }
}
