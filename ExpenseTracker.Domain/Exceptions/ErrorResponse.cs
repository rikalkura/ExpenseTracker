using System.Text.Json.Serialization;

namespace ExpenseTracker.Domain.Exceptions;

public record ErrorResponse(
    string Title,
    string Message,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    IEnumerable<ErrorDetail>? Details = null);

public record ErrorDetail(string Reason, string Message);