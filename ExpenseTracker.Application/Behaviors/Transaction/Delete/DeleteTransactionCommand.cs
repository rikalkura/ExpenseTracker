using MediatR;

namespace ExpenseTracker.Application.Behaviors.Transaction.Delete;

public record DeleteTransactionCommand(List <int> Ids) : IRequest;
