using ExpenseTracker.Application.Behaviors.User.Get;
using ExpenseTracker.Application.Behaviors.User.Login;
using ExpenseTracker.Application.Behaviors.User.Register;
using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Dto;
using MediatR;

namespace ExpenseTracker.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var authEndpoints = app.MapGroup("/auth")
            .AllowAnonymous()
            .WithTags("Auth");

        authEndpoints.MapPost("/register",
            async (RegisterCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);
                return Results.Ok();
            });

        authEndpoints.MapPost("/login",
            async (
                LoginRequestDto request,
                IMediator mediator,
                CancellationToken ct) =>
            {
                var command = new LoginCommand(request.Email, request.Password);
                var tokenResponse = await mediator.Send(command, ct);

                return Results.Ok(tokenResponse);
            });
    }
}
