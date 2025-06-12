using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Abstraction;

public interface IJwtTokenProvider
{
    TokenResponseDto Generate(
        UserEntity user);
}
