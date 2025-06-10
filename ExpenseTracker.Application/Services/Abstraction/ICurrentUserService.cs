using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Abstraction;

public interface ICurrentUserService
{
    Guid Id { get; }
    string Email { get; }

    void SetData(Guid id, string email);

    Task<UserEntity> Get();
}
