namespace ExpenseTracker.API;

public static class DependencyInjection
{
    public static IServiceCollection AddConfigurations(
       this IServiceCollection services,
       IConfigurationManager configuration,
       IWebHostEnvironment environment)
    {
        configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        return services;
    }
}
