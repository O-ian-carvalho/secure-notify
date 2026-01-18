using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UseCases.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
               .NotEmpty()
               .MinimumLength(8)
                   .WithMessage("Password must be at least 8 characters.")
               .Matches("[0-9]")
                   .WithMessage("Password must contain at least one digit.")
               .Matches("[^a-zA-Z0-9]")
                   .WithMessage("Password must contain at least one non-alphanumeric character.");
                
        }
    }
}
