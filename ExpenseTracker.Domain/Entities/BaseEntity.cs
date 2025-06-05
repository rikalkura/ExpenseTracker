namespace ExpenseTracker.Domain.Entities;

public class BaseEntity
{
    public BaseEntity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}
