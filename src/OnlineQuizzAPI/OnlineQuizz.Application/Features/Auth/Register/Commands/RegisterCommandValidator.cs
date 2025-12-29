using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Auth.Register.Commands
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress().WithMessage("Invalid email");

            RuleFor(x => x.UserName)
                .NotEmpty().MinimumLength(3).WithMessage("Username must be at least 3 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6).WithMessage("Min 6 characters")
                .Matches("[0-9]").WithMessage("Must contain a digit")
                .Matches("[A-Z]").WithMessage("Must contain uppercase")
                .Matches("[^a-zA-Z0-9]").WithMessage("Must contain special character");
        }
    }
}
