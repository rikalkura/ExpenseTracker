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
            async (
                RegisterCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);

                return Results.Ok(result);
            });

        authEndpoints.MapPost("/login",
            async (
                LoginRequestDto request,
                IAuthService authService,
                CancellationToken ct) =>
            {

                var tokenResponse = await authService.LoginAsync(request, ct);

                return Results.Ok(tokenResponse);
            });

    }
}
