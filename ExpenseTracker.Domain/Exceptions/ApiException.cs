using System.Net;

namespace ExpenseTracker.Domain.Exceptions;

public class ApiException : Exception
{
    public ApiException(
        HttpStatusCode statusCode,
        string title,
        string message)
        : base(message)
    {
        StatusCode = statusCode;
        Title = title;
    }

    public ApiException()
        : this(
            HttpStatusCode.InternalServerError,
            "Server error.",
            "Unexpected server error occurred. Try again later.")
    { }

    public HttpStatusCode StatusCode { get; }

    public string Title { get; }
}
