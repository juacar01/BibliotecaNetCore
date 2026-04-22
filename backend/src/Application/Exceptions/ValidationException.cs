
using FluentValidation.Results;

namespace Biblioteca.Application.Exceptions;


public class ValidationException : ApplicationException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException() : base("Se presentaron uno o mas errores")
    {
        Errors = new Dictionary<string, string[]>();
    }


    public ValidationException(IEnumerable<ValidationFailure> failures)
          : this()
    {
        var failureGroups = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

        foreach (var failureGroup in failureGroups)
        {
            var propertyName = failureGroup.Key;
            var propertyFailures = failureGroup.ToArray();
            Errors.Add(propertyName, propertyFailures);
        }
    }
}