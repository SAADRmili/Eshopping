using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviour;

//this will collect fluent validator and run before handler 
public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            //this will run all the validation rules one by one and return the validation result 

            var validatorResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // now need to check for any failure 
            var failure = validatorResults.SelectMany(e => e.Errors).Where(f => f != null).ToList();

            if (failure.Count != 0)
            {
                throw new ValidationException(failure);
            }
        }

        //On success case
        return await next();
    }
}
