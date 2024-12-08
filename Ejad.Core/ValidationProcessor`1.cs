using FluentValidation;
using MediatR.Pipeline;

namespace Ejad.Core;

public class ValidationProcessor<TRequest>(IEnumerable<IValidator<TRequest>> validators)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(
                validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}