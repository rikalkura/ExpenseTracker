using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.User.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string PhoneNumber,
    Gender Gender) 
    : IRequest<string>;
