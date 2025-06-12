using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Common;

public class EFCoreRepository<TEntity>(
    AppDbContext appDbContext) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet = appDbContext.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public Task<TEntity?> GetByIdAsync(Guid id)
    {
        return _dbSet.FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public Task SaveChangesAsync()
    {
        return appDbContext.SaveChangesAsync();
    }
}
