using System.Net;

namespace ExpenseTracker.Domain.Exceptions;

public class NotAuthorizedException : ApiException
{
    public NotAuthorizedException(string message)
        : base(
            HttpStatusCode.Unauthorized,
             "Unauthorized.",
             message)
    { }
}
