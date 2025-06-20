using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
    public async Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> spec)
    {
        return await _dbSet.WithSpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec)
    {
        return await _dbSet.WithSpecification(spec).ToListAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.AnyAsync(predicate);
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
