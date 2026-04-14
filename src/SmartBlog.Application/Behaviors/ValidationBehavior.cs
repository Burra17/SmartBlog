using FluentValidation;
using MediatR;

namespace SmartBlog.Application.Behaviors;

// Generisk pipeline behavior som kör validering innan MediatR-handlern anropas
// Flödet: Request -> ValidationBehavior -> Handler
// Om valideringen misslyckas kastas ValidationException som fångas av ErrorHandlingMiddleware
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    // Alla registrerade validators för den aktuella request-typen injiceras här
    // T.ex. för CreateBlogPostCommand hamnar CreateBlogPostCommandValidator här
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Kolla om det finns några validators registrerade för denna request-typ
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            // Kör alla validators parallellt och samla ihop resultaten
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Plocka ut alla fel från alla validators
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // Om det finns valideringsfel - kasta exception (fångas av ErrorHandlingMiddleware)
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
        }

        // Ingen validator hittade fel - gå vidare till nästa steg (handlern)
        return await next(cancellationToken);
    }
}
