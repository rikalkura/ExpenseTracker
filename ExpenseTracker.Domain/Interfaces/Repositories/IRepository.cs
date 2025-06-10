using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Domain.Interfaces.Repositories;
public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    void Add(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);

    Task SaveChangesAsync();
}
