using ExpenseTracker.Application.Behaviors.UserPage.Get;
using MediatR;

namespace ExpenseTracker.API.Endpoints;

public static class UserPageEndpoints
{
    public static void MapUserPageEndpoints(this WebApplication app)
    {
        var userPageEndpoints = app.MapGroup("/userpage")
            .AllowAnonymous()
            .WithTags("UserPage");

        userPageEndpoints.MapGet("{id}",
            async (
                Guid id,
                IMediator mediator) =>
            {
                var query = new GetUserPageForUserQuery(id);

                var result = await mediator.Send(query);

                return Results.Ok(result);
            });
    }
}
