using FluentValidation;

namespace ExpenseTracker.Application.Behaviors.User.Update;

public class UpdateUserInfoValidator : AbstractValidator<UpdateUserInfoCommand>
{
    public UpdateUserInfoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number is not valid.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Gender must be a valid enum value.");
    }
}
