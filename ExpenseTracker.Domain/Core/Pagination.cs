using MediatR;

namespace ExpenseTracker.Domain.Core;

public record PaginationQuery<TItem>(int CurrentPage = 1, int PageSize = 10) 
    : IRequest<PaginationResult<TItem>>
{
    public int Skip => (CurrentPage - 1) * PageSize;
    public int Take => PageSize;
}

public record PaginationResult<TItem>(
    List<TItem> Items,
    int TotalItems,
    int CurrentPage,
    int PageSize)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool HasNextPage => CurrentPage < TotalItems;
    public bool HasPreviousPage => CurrentPage > 1;
}
