using Bank.Application.common.Results;
using FluentValidation;
using MediatR;

public class ValidationBehavior<TRequest, T>
    : IPipelineBehavior<TRequest, Result<T>>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<Result<T>> Handle(
        TRequest request,
        RequestHandlerDelegate<Result<T>> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var results = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(
                        context,
                        cancellationToken)));

            var errors = results
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .Select(x => x.ErrorMessage)
                .ToList();

            if (errors.Any())
            {
                return Result<T>.Failure(
                    ResultStatus.BadRequest,
                    string.Join(", ", errors));
            }
        }

        return await next();
    }
}