using ExpenseTracker.Domain.Dto;

namespace ExpenseTracker.Application.Services.Abstraction;

public interface IAuthService
{
    Task<TokenResponseDto> LoginAsync(LoginRequestDto request, CancellationToken ct);
}
