using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Behaviors.Transaction.Extensions;

public static class TransactionExtensions
{
    public static decimal ToSignedAmount(this decimal amount, CategoryType type)
    {
        return type == CategoryType.Income ? amount : -amount;
    }
}
