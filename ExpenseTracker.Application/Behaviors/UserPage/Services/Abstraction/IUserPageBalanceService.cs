namespace ExpenseTracker.Application.Behaviors.UserPage.Services.Abstraction;

public interface IUserPageBalanceService
{
    Task RecalculateAsync(Guid userPageId, CancellationToken cancellationToken = default);
}