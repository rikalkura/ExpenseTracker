using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces.Repositories;
using ExpenseTracker.Infrastructure.Repositories.Common;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation;

public class TokenRepository : EFCoreRepository<RefreshTokenEntity>, ITokenRepository
{
    public TokenRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
