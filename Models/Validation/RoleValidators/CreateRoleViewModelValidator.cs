using FluentValidation;
using FudbalskiTurnir_FilipNikolic.Models.ViewModel;

namespace FudbalskiTurnir_FilipNikolic.Models.Validation.RoleValidators
{
    public class CreateRoleViewModelValidator : AbstractValidator<CreateRoleViewModel>
    {
        public CreateRoleViewModelValidator()
        {
            RuleFor(p => p.RoleName)
                .NotEmpty().WithMessage("Role Name can not be empty.");
        }
    }
}
