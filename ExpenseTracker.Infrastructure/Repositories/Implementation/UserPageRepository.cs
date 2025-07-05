using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces.Repositories;
using ExpenseTracker.Infrastructure.Repositories.Common;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation;

public class UserPageRepository : EFCoreRepository<UserPageEntity>, IUserPageRepository
{
    public UserPageRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
