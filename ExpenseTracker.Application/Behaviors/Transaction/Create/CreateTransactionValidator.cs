using FluentValidation;

namespace ExpenseTracker.Application.Behaviors.Transaction.Create;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.Amount)
           .NotEqual(0).WithMessage("Amount cannot be 0.");
    }
}
