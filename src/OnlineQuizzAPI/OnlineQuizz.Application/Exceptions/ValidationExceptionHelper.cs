using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Exceptions
{
    public static class ValidationExceptionHelper
    {
        public static ValidationException CreateValidationError(string field, string message)
        {
            var validationResult = new ValidationResult(
                new List<ValidationFailure> { new(field, message) }
            );

            return new ValidationException(validationResult);
        }

        public static ValidationException Multiple(IDictionary<string, string> errors)
        {
            var failures = errors.Select(e => new ValidationFailure(e.Key, e.Value)).ToList();
            return new ValidationException(new ValidationResult(failures));
        }
    }
}
