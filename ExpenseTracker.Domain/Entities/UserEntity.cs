using ExpenseTracker.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Domain.Entities;

public class UserEntity : IdentityUser<Guid> 
{
    public UserEntity()
    {
        Gender = Gender.None;
    }

    public Gender Gender { get; set; }
}
