using FluentValidation;
using System.Text.RegularExpressions;

namespace Techtalk.FM.Domain.DTOs.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            #region "  Not Null / Not Empty  "

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, informe seu e-mail")
                .WithErrorCode("400");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, informe sua senha")
                .WithErrorCode("400");

            #endregion

            #region "  Custom Validators  "

            RuleFor(x => x.Email).Must(predicate: (user, email) =>
            {
                if (email == null)
                    return false;

                Match match = Regex.Match(email, @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$", RegexOptions.IgnoreCase);

                return match.Success;
            })
            .WithMessage("Por favor, informe um e-mail válido.")
            .WithErrorCode("400");

            #endregion
        }
    }
}
