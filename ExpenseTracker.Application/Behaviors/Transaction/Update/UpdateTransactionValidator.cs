using FluentValidation;

namespace ExpenseTracker.Application.Behaviors.Transaction.Update;

public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionCommand>
{
    public UpdateTransactionValidator()
    {
        RuleFor(x => x.Amount)
           .NotEqual(0).WithMessage("Amount cannot be 0.");
    }
}
