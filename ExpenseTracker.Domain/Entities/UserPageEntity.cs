
namespace ExpenseTracker.Domain.Entities;

public  class UserPageEntity : BaseEntity<Guid>
{
    public UserPageEntity(
        decimal balance,
        decimal totalExpenses,
        decimal totalIncome,
        Guid userId)
    {
        Id = Guid.NewGuid();
        Balance = balance;
        TotalExpenses = totalExpenses;
        TotalIncome = totalIncome;
        UserId = userId;
    }

    public decimal Balance { get; private set; }
    public decimal TotalExpenses { get; private set; }
    public decimal TotalIncome { get; private set; }
    public Guid UserId { get; private set; }

    public static UserPageEntity Empty(Guid userId)
    {
        return new UserPageEntity(0, 0, 0, userId);
    }

    public void SetBalance(
        decimal balance,
        decimal totalExpenses,
        decimal totalIncome)
    {
        Balance = balance;
        TotalExpenses = Math.Abs(totalExpenses);
        TotalIncome = totalIncome;
    }

}
