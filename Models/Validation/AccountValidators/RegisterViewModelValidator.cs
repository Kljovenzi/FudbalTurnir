using FluentValidation;
using FudbalskiTurnir_FilipNikolic.Models.ViewModel;

namespace FudbalskiTurnir_FilipNikolic.Models.Validation.AccountValidators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Minimum Length for password is 6");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Password is required.")
                .Equal(x => x.Password).WithMessage("Confirm Password do not match the Password.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User Name must not be empty.")
                .MinimumLength(4).WithMessage("UserName mus be at least 4 letters long.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Please enter proper email adress.");
        }
    }
}
