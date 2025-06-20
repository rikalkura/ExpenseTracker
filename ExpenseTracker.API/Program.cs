using ExpenseTracker.API;
using ExpenseTracker.API.Endpoints;
using ExpenseTracker.API.Middlewares;
using ExpenseTracker.Application;
using ExpenseTracker.Infrastructure;

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CurrentUserMiddleware>();

app.MapAuthEndpoints();
app.MapUserEndpoints();
app.MapCategoryEndpoints();

app.Run();
