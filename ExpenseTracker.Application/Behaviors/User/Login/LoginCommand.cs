using ExpenseTracker.Domain.Dto;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.User.Login;

public record LoginCommand(string Email, string Password) : IRequest<TokenResponseDto>;
