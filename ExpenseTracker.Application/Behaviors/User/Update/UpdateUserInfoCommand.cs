using ExpenseTracker.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Application.Behaviors.User.Update;

public class UpdateUserInfoCommand : IRequest
{
    [JsonIgnore]
    public string Id { get; set; }
    public Guid GuidUserId => Guid.Parse(Id);
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
}