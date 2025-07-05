namespace ExpenseTracker.Domain.Dto;

public record TransactionDto(
    int Id,
    decimal Amount,
    decimal BalanceBeforeTransaction,
    DateOnly Date,
    string Comment,
    string CategoryName,
    Guid? CategoryId,
    Guid UserPageId);
