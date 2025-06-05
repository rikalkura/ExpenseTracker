using ExpenseTracker.API;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Application;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services
            .AddConfigurations(configuration, environment)
            .AddInfrastructure(configuration)
            .AddApplication();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
