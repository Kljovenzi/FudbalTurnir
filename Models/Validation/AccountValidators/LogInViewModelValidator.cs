using FluentValidation;
using FudbalskiTurnir_FilipNikolic.Models.ViewModel;

namespace FudbalskiTurnir_FilipNikolic.Models.Validation.AccountValidators
{
    public class LogInViewModelValidator : AbstractValidator<LogInViewModel>
    {
        public LogInViewModelValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("This is not an email adress.")
                 .NotEmpty().WithMessage("User Name must not be empty.");
            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("Password is required.");

        }
    }
}
