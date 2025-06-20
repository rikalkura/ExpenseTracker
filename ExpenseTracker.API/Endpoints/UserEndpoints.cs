using ExpenseTracker.Application.Behaviors.User.Get;
using ExpenseTracker.Application.Behaviors.User.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var userEndpoints = app.MapGroup("/user")
            .AllowAnonymous()
            .WithTags("User");

        userEndpoints.MapGet("/get/{id}",
            async (
                string id,
                IMediator mediator) =>
            {
                var query = new GetUserInfoQuery(id);

                var result = await mediator.Send(query);

                return Results.Ok(result);
            });

        userEndpoints.MapPut("update/{id}",
            async (
                string id,
                [FromBody] UpdateUserInfoCommand command,
                IMediator mediator) =>
            {

                command.Id = id;
                await mediator.Send(command);

                return Results.Ok();
            });
    }
}
