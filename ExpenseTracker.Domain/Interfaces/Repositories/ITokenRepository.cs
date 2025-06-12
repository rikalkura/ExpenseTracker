using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Domain.Interfaces.Repositories;

public interface ITokenRepository : IRepository<RefreshTokenEntity> { };
