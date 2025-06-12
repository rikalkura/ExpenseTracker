using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Dto;

public class UserDto
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
}
