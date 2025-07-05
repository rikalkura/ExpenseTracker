using ExpenseTracker.Application.Behaviors.Category.Specifications;
using ExpenseTracker.Application.Behaviors.Transaction.Specifications;
using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.Category.Delete;

public class DeleteCategoryHandler(
    ICategoryRepository categoryRepository,
    ITransactionRepository transactionRepository,
    ICurrentUserService currentUserService) 
    : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FirstAsync(
            new UserCategoryByIdSpecification(request.Id, currentUserService.Id),
            cancellationToken);

        var transactionsWithCategory = await transactionRepository.ListAsync(
            new GetTransactionsByCategoryIdSpecification(category.Id),
            cancellationToken);

        foreach (var transaction in transactionsWithCategory)
        {
            transaction.SetCategoryId(null);
            transactionRepository.Update(transaction);
        }

        categoryRepository.Delete(category);
        await categoryRepository.SaveChangesAsync(cancellationToken);
    }
}
