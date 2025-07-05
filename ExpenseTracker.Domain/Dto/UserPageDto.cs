namespace ExpenseTracker.Domain.Dto;

public record UserPageDto(
    decimal Balance,
    decimal TotalExpenses,
    decimal TotalIncome);