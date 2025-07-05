using ExpenseTracker.API.Requests.Transaction;
using ExpenseTracker.Application.Behaviors.Transaction.Create;
using ExpenseTracker.Application.Behaviors.Transaction.Delete;
using ExpenseTracker.Application.Behaviors.Transaction.Get;
using ExpenseTracker.Application.Behaviors.Transaction.Update;
using ExpenseTracker.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this WebApplication app)
    {
        var transactionEndpoints = app.MapGroup("/transaction")
            .WithTags("Transaction");

        transactionEndpoints.MapGet("",
           async (
               IMediator mediator,
               TransactionFilterEnum filterEnum,
               TransactionsSortEnum sortEnum,
               SortDirection direction,
               int currentPage,
               int pageSize,
               string? searchValue = null,
               string? filterValue = null) =>
           {
               var query = new GetTransactionsQuery(
                   filterEnum,
                   sortEnum,
                   direction,
                   currentPage,
                   pageSize,
                   searchValue,
                   filterValue);

               var transactions = await mediator.Send(query);

               return Results.Ok(transactions);
           });
               /*
                   Request: 
                       Filter: TransactionFilterEnum {}
                       Sort: TransactionSortEnum {}
                       SortDirection: SortDirectionEnum {}
                       Pagination: class - int CurrentPage, int PageSize
                   Response:
                       List<items>, int TotalPages, int TotalItems, int CurrentPage, int PageSize, bool HasNextPage, boolHasPreviouspage
                */

               transactionEndpoints.MapPost("", 
            async (
                CreateTransactionCommand command,
                IMediator mediator) =>
            {
                await mediator.Send(command);

                return Results.Created();
            });

        /*
         BalanceBeforeTransaction - AdjustBalances(DateTime Date, Guid UserPageId)
            - не викликаєм коли Create та дата сьогоднішня
            TransactionBalanceService{}
         */



        /*
           Request: 
                decimal Amount, DateTime Date, string Comment, Guid CategoryId
           Response:
                Results.Created();

            Flow: 
            public decimal Amount - request.Amount, negative or positive, based on category type(income, outcome)
            public decimal BalanceBeforeTransaction - current balance of UserPage/ gotta take the last transaction from a Date that user inputs. Then apply an Amount to the BalanceBeforeTransaction of this exact Transaction.
            public DateTime Date - request.Date
            public string Comment - request.Comment
            public Guid CategoryId - request.CategoryId
            public Guid UserPageId - Get by currentUserService.UserId From db 

            !Attention!
                Case: if user inserts a transaction with a date in the past, the  BalanceBeforeTransaction for the next transaction shoul be configured properly. +-Amount - based on type
         
            Update UserPage aswell
         */

        transactionEndpoints.MapPut("/{id}",
            async (
                int id,
                [FromBody] UpdateRequest request,
                IMediator mediator) =>
            {
                var command = new UpdateTransactionCommand(
                    id,
                    request.Amount,
                    request.Date,
                    request.Comment);

                await mediator.Send(command);

                return Results.Ok(command);
            });
        /*
           Request: 
                Guid Id, decimal Amount, DateTime Date, string Comment
           Response:
                Results.Ok();

            Flow: 
            public decimal Amount - request.Amount, negative or positive, based on category type(income, outcome)
            public decimal BalanceBeforeTransaction - current balance of UserPage/ gotta take the last transaction from a Date that user inputs. Then apply an Amount to the BalanceBeforeTransaction of this exact Transaction.
            public DateTime Date - request.Date
            public string Comment - request.Comment
            public Guid CategoryId - from db
            public Guid UserPageId - Get by currentUserService.UserId From db 

            !Attention!
                Case - if date has changed: adjust BalanceBeforeTransaction accordingly
                Case: if user inserts a transaction with a date in the past, the  BalanceBeforeTransaction for the next transaction shoul be configured properly. +-Amount - based on type
         
            Update UserPage aswell
         */
        transactionEndpoints.MapDelete("/",
            async (
                [FromQuery] int[] ids,
                IMediator mediator) =>
            {
                var command = new DeleteTransactionCommand(ids.ToList());

                await mediator.Send(command);

                return Results.Ok();
            });

        /*
           Request: 
                Guid Id
           Response:
                Results.Ok();

            Flow: 
            public decimal Amount - request.Amount, negative or positive, based on category type(income, outcome)
            public decimal BalanceBeforeTransaction - current balance of UserPage/ gotta take the last transaction from a Date that user inputs. Then apply an Amount to the BalanceBeforeTransaction of this exact Transaction.
            public DateTime Date - request.Date
            public string Comment - request.Comment
            public Guid CategoryId - from db
            public Guid UserPageId - Get by currentUserService.UserId From db 

            !Attention!
                Case - if date has changed: adjust BalanceBeforeTransaction accordingly
                Case: if user inserts a transaction with a date in the past, the  BalanceBeforeTransaction for the next transaction shoul be configured properly. +-Amount - based on type
         
            Update UserPage aswell
         */
    }
}
