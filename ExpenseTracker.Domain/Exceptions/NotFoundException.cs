using System.Net;

namespace ExpenseTracker.Domain.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string message)
         : base(
             HttpStatusCode.NotFound,
             "Not found.",
             message)
    { }
}
