using ExpenseTracker.API.Requests.Category;
using ExpenseTracker.Application.Behaviors.Category.Create;
using ExpenseTracker.Application.Behaviors.Category.Get;
using ExpenseTracker.Application.Behaviors.Category.Update;
using ExpenseTracker.Application.Behaviors.User.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var categoryEndpoints = app.MapGroup("/category")
            .AllowAnonymous()
            .WithTags("Category");

        categoryEndpoints.MapPost("", async(
            CreateCategoryCommand command,
            IMediator mediator) =>
        {
            await mediator.Send(command);

            return Results.Created();
        });

        categoryEndpoints.MapGet("", async (
            IMediator mediator) =>
        {
            var categories = await mediator.Send(new GetCategoriesForUserQuery());

            return Results.Ok(categories);
        });

        categoryEndpoints.MapPut("/{id}",
           async (
               Guid id,
               [FromBody] UpdateRequest request,
               IMediator mediator) =>
           {

               var command = new UpdateCategoryCommand(id, request.Name);

               await mediator.Send(command);

               return Results.Ok();
           });

    }
}
