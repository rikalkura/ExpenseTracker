using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTracker.Infrastructure.Repositories.Common;

public class EFCoreRepository<TEntity>(
    AppDbContext appDbContext) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly DbSet<TEntity> _dbSet = appDbContext.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(ISingleResultSpecification<TEntity> spec, CancellationToken cancellationToken)
    {
        return await _dbSet.WithSpecification(spec).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> FirstAsync(ISingleResultSpecification<TEntity> spec, CancellationToken cancellationToken)
    {
        var entity = await FirstOrDefaultAsync(spec, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} not found.");
        }

        return entity;
    }

    public async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken)
    {
        return await _dbSet.WithSpecification(spec).ToListAsync(cancellationToken);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return _dbSet.CountAsync(predicate, cancellationToken);
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

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return appDbContext.SaveChangesAsync(cancellationToken);
    }

}
