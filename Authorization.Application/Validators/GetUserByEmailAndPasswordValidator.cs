using System.Linq;
using Authorization.Application.Models.Commands;
using FluentValidation;

namespace Authorization.Application.Validators
{
    public class GetUserByEmailAndPasswordValidator : AbstractValidator<EnterUserCommand>
    {
        public GetUserByEmailAndPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).Must(password => password.All(c => c != ' '));
        }
    }
}
