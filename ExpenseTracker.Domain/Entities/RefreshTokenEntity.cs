namespace ExpenseTracker.Domain.Entities;

public class RefreshTokenEntity : BaseEntity
{
    public RefreshTokenEntity(
        string token,
        Guid userId)
    {
        Token = token;
        ExpiryDate = DateTime.UtcNow.AddHours(24);
        UserId = userId;
        IsUsed = false;
    }

    public string Token { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsUsed { get; private set; }

    public void UseToken()
    {
        IsUsed = true;
    }
}
