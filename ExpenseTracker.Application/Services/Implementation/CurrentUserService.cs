using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Application.Services.Implementation;

public class CurrentUserService(
    UserManager<UserEntity> userManager) 
    : ICurrentUserService
{
    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public async Task<UserEntity> Get()
    {
        var result = await userManager.FindByIdAsync(Id.ToString());

        if (result is null)
        {
            throw new NotFoundException(
                $"User with the ID - {Id} - cannot be found.");
        }

        return result;
    }

    public void SetData(
        Guid id,
        string email)
    {
        Id = id;
        Email = email;
    }
}
