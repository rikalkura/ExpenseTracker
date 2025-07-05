using MediatR;

namespace ExpenseTracker.Application.Behaviors.Transaction.Create;

public record CreateTransactionCommand(
    decimal Amount,
    DateOnly Date,
    string Comment,
    Guid CategoryId) 
    : IRequest;