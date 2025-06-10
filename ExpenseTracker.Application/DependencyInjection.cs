using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Application.Services.Implementation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(c =>
            c.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
