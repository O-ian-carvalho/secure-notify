

using FluentValidation.Results;

namespace Shared.Application.Exceptions
{
    public class ValidationException : Exception
    {

        public ValidationException(List<ValidationFailure> errors) : base(string.Join("; ", errors.Select(e => e.ErrorMessage)))
        {
        }        
    }
}
