using Microsoft.AspNetCore.Identity;
using System.Net;

namespace ExpenseTracker.Domain.Exceptions;

public class IdentityOperationException : ApiException
{
    public IdentityOperationException(string message)
        : base(
            HttpStatusCode.BadRequest,
            "Identity operation error.",
            message)
    { }

    public IdentityOperationException(HttpStatusCode statusCode, string message)
        : base(
            statusCode,
            "Identity operation error.",
            message)
    { }

    public IdentityOperationException(
        string message,
        IEnumerable<IdentityError>? errors)
        : this(message)
    {
        if (errors is null)
        {
            return;
        }

        IdentityErrors = errors.ToList();
    }

    public IdentityOperationException(
        HttpStatusCode statusCode,
        string message,
        IEnumerable<IdentityError>? errors)
        : this(statusCode, message)
    {
        if (errors is null)
        {
            return;
        }

        IdentityErrors = errors.ToList();
    }

    public List<IdentityError>? IdentityErrors { get; }
}
