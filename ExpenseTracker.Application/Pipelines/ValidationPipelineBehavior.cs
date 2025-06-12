using FluentValidation;
using MediatR;

namespace ExpenseTracker.Application.Pipelines;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public ValidationPipelineBehavior(
        IEnumerable<IValidator<TRequest>> validators)
    {
        Validators = validators;
    }

    private IEnumerable<IValidator<TRequest>> Validators { get; }

    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!Validators.Any())
            return next();

        var failures = Validators
            .Select(t => t.Validate(request))
            .SelectMany(t => t.Errors)
            .Where(t => t != null)
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(
                "One or more validation errors occured.",
                failures);
        }

        return next();
    }
}