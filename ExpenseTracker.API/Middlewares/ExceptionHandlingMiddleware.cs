using ExpenseTracker.Domain.Exceptions;

namespace ExpenseTracker.API.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            if (ex is FluentValidation.ValidationException fvEx)
            {
                ex = new ValidationException(
                    "Validation failed.",
                    fvEx.Errors.ToList());
            }

            ApiException apiException = ex as ApiException ?? new ApiException();

            var response = apiException switch
            {
                ValidationException validationException => new ErrorResponse(
                    validationException.Title,
                    validationException.Message,
                    validationException.ValidationErrors?.Select(e => new ErrorDetail(e.Field, e.Error))),

                IdentityOperationException identityOperationException => new ErrorResponse(
                    identityOperationException.Title,
                    identityOperationException.Message,
                    identityOperationException.IdentityErrors?.Select(e => new ErrorDetail(e.Code, e.Description))),

                _ => new ErrorResponse(apiException.Title, apiException.Message)
            };

            context.Response.StatusCode = (int)apiException.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }

}
