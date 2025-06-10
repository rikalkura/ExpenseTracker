using FluentValidation.Results;
using System.Net;

namespace ExpenseTracker.Domain.Exceptions;

public class ValidationException : ApiException
{
    public ValidationException(string message)
       : base(
           HttpStatusCode.BadRequest,
           "Validation error.",
           message)
    { }

    public ValidationException(
        string message,
        List<ValidationFailure>? errors)
        : this(message)
    {
        if (errors is null)
        {
            return;
        }

        ValidationErrors = errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage)).ToList();
    }

    public List<ValidationError>? ValidationErrors { get; }
}

public record ValidationError(string Field, string Error);