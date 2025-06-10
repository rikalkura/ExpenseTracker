using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Abstraction;

public interface ITokenService
{
    TokenResponseDto Generate(
        UserEntity user);
}
