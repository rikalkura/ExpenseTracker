using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Domain.Exceptions;
using System.Security.Claims;

namespace ExpenseTracker.API.Middlewares;

public class CurrentUserMiddleware(
    ICurrentUserService currentUserService) 
    : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        var isAuthentificated = context.User?.Identity?.IsAuthenticated ?? false;
        if (!isAuthentificated)
        {
            await next(context);

            return;
        }

        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userEmail = context.User.FindFirst(ClaimTypes.Email)?.Value;

        if (userId is null || userEmail is null)
        {
            throw new NotAuthorizedException("Required authorization parameters missing.");
        }

        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            throw new NotAuthorizedException("Invalid user ID format.");
        }

        currentUserService.SetData(parsedUserId, userEmail);

        await next(context);
    }
}
