using ExpenseTracker.Domain.Core;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Behaviors.Transaction.Get;

public record GetTransactionsQuery(
    TransactionFilterEnum FilterEnum,
    TransactionsSortEnum SortEnum,
    SortDirection Direction,
    int CurrentPage,
    int PageSize,
    string? SearchValue = null,
    string? FilterValue = null) 
    : PaginationQuery<TransactionDto>(CurrentPage, PageSize);
