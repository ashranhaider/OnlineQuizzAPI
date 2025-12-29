using FluentValidation.Results;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineQuizz.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(ValidationResult result)
        {
            Errors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }
    }
}
