using MediatR;

namespace ExpenseTracker.Application.Behaviors.Transaction.Update;

public record UpdateTransactionCommand(
    int Id,
    decimal Amount,
    DateOnly Date,
    string Comment) : IRequest;
