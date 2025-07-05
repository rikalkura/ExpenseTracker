using ExpenseTracker.Application.Behaviors.Transaction.Specification;
using ExpenseTracker.Application.Behaviors.UserPage.Specifications;
using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Application.Services.Implementation;
using ExpenseTracker.Domain.Core;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Behaviors.Transaction.Get;

public class GetTransactionsHandler(
    ITransactionRepository transactionRepository,
    IUserPageRepository userPageRepository,
    ICurrentUserService currentUserService) 
    : IRequestHandler<GetTransactionsQuery, PaginationResult<TransactionDto>>
{
    public async Task<PaginationResult<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var userPage = await GetUserPageAsync(cancellationToken);

        var transactions = await transactionRepository.ListAsync(
            new GetFilteredAndSortedTransactionsSpecification(
                request.Skip,
                request.Take,
                userPage.Id,
                request.SortEnum,
                request.Direction,
                request.FilterEnum,
                request.SearchValue,
                request.FilterValue),
            cancellationToken);

        var totalItems = await transactionRepository.CountAsync(
            t => t.UserPageId == userPage.Id,
            cancellationToken);

        var transactionDtos = transactions.Select(t => new TransactionDto(
            t.Id,
            t.Amount,
            t.BalanceBeforeTransaction,
            t.Date,
            t.Comment,
            t.Category?.Name ?? "Undefined",
            t.CategoryId,
            userPage.Id)).ToList();

        return new PaginationResult<TransactionDto>(
            transactionDtos,
            totalItems,
            request.CurrentPage,
            request.PageSize);
    }

    private async Task<UserPageEntity> GetUserPageAsync(CancellationToken cancellationToken)
    {
        var userId = currentUserService.Id;

        var userPage = await userPageRepository.FirstOrDefaultAsync(
            new GetUserPageByUserId(userId),
            cancellationToken);

        if (userPage is null)
        {
            throw new NotFoundException("User page does not exist");
        }   

        return userPage;
    }
}
