namespace ExpenseTracker.Domain.Entities;

public  class UserPageEntity : BaseEntity
{
    public UserPageEntity(
        decimal balance,
        decimal totalExpenses,
        decimal totalIncome,
        Guid userId)
    {
        Balance = balance;
        TotalExpenses = totalExpenses;
        TotalIncome = totalIncome;
        UserId = userId;
    }

    public decimal Balance { get; private set; }
    public decimal TotalExpenses { get; private set; }
    public decimal TotalIncome { get; private set; }
    public Guid UserId { get; private set; }
}
