namespace ExpenseTracker.Domain.Entities;

public abstract class BaseEntity<TId> : IEntity
{
   public TId Id { get; set; }
}

public interface IEntity { }