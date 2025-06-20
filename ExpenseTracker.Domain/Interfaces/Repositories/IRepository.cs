using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;
using System.Linq.Expressions;

namespace ExpenseTracker.Domain.Interfaces.Repositories;
public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> spec);

    Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    void Add(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);

    Task SaveChangesAsync();
}
