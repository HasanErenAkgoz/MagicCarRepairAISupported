using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace MagicCarRepairAISupported.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            // Some DI scenarios can pass null for IEnumerable<>; Any() on null throws NRE.
            _validators = validators ?? Array.Empty<IValidator<TRequest>>();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context, cancellationToken))
            );

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (failures.Any())
            {
                var errorDictionary = failures
                    .GroupBy(failure => failure.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(failure => failure.ErrorMessage).Distinct().ToArray()
                    );

                var errorMessages = errorDictionary
                    .SelectMany(kvp => kvp.Value.Select(message => new ValidationFailure(kvp.Key, message)))
                    .ToList();

                throw new ValidationException(errorMessages);
            }

            return await next();
        }
    }

}

