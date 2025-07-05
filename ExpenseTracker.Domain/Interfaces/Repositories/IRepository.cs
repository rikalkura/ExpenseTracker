using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;
using System.Linq.Expressions;

namespace ExpenseTracker.Domain.Interfaces.Repositories;
public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken);

    Task<TEntity?> FirstOrDefaultAsync(ISingleResultSpecification<TEntity> spec, CancellationToken cancellationToken);
   
    Task<TEntity> FirstAsync(ISingleResultSpecification<TEntity> spec, CancellationToken cancellationToken);
   
    Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    void Add(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
