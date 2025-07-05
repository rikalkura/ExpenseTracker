using ExpenseTracker.Application.Behaviors.Transaction.Services;
using ExpenseTracker.Application.Behaviors.UserPage.Services.Abstraction;
using ExpenseTracker.Application.Behaviors.UserPage.Services.Implementation;
using ExpenseTracker.Application.Pipelines;
using ExpenseTracker.Application.Services.Abstraction;
using ExpenseTracker.Application.Services.Implementation;
using FluentValidation;
using MediatR;
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

        services.AddTransient(
          typeof(IPipelineBehavior<,>),
          typeof(ValidationPipelineBehavior<,>));

        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ITransactionBalanceService, TransactionBalanceService>();
        services.AddScoped<IUserPageBalanceService, UserPageBalanceService>();

        return services;
    }
}
