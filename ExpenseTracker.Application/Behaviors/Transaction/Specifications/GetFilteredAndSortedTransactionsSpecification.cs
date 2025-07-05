using Ardalis.Specification;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Behaviors.Transaction.Specification;

public class GetFilteredAndSortedTransactionsSpecification : SingleResultSpecification<TransactionEntity>
{
    public GetFilteredAndSortedTransactionsSpecification(
        int skip, 
        int take,
        Guid userPageId,
        TransactionsSortEnum sortEnum,
        SortDirection sortDirection,
        TransactionFilterEnum filterEnum,
        string? search = null,
        string? filterValue = null)
    {
        Query.Include(t => t.Category)
             .Where(t => t.UserPageId == userPageId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            Query.Where(t => t.Comment.Contains(search));
        }

        switch (filterEnum)
        {
            case TransactionFilterEnum.Date:
                if (filterValue is not null &&
                    DateOnly.TryParse(filterValue, out var date))
                {
                    Query.Where(t => t.Date == date);
                }
                break;

            case TransactionFilterEnum.Category:
                if (!string.IsNullOrWhiteSpace(filterValue))
                {
                    Query.Where(t => t.Category != null && t.Category.Name.Contains(filterValue));
                }
                break;

            case TransactionFilterEnum.Type:
                if (!string.IsNullOrWhiteSpace(filterValue) &&
                    Enum.TryParse<CategoryType>(filterValue, ignoreCase: true, out var parsedType))
                {
                    Query.Where(t => t.Category != null && t.Category.Type == parsedType);
                }
                break;


            case TransactionFilterEnum.None:
            default:
                break;
        }

        switch (sortEnum)
        {
            case TransactionsSortEnum.Id:
                if (sortDirection == SortDirection.Ascending)
                    Query.OrderBy(t => t.Id);
                else
                    Query.OrderByDescending(t => t.Id);
                break;

            case TransactionsSortEnum.Date:
                if (sortDirection == SortDirection.Ascending)
                    Query.OrderBy(t => t.Date);
                else
                    Query.OrderByDescending(t => t.Date);
                break;

            case TransactionsSortEnum.Amount:
                if (sortDirection == SortDirection.Ascending)
                    Query.OrderBy(t => t.Amount);
                else
                    Query.OrderByDescending(t => t.Amount);
                break;
        }

        Query.Skip(skip).Take(take);
    }
}
