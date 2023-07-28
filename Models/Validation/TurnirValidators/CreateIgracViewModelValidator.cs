using FluentValidation;
using FudbalskiTurnir_FilipNikolic.Models.ViewModel;

namespace FudbalskiTurnir_FilipNikolic.Models.Validation.TurnirValidators
{
	public class CreateIgracViewModelValidator : AbstractValidator<CreateIgracViewModel>
	{
		public CreateIgracViewModelValidator()
		{
			RuleFor(p => p.Ime)
				.NotEmpty().WithMessage("Ime ne moze biti prazno")
				.MinimumLength(3).WithMessage("Mora imati bar 3 slova");
            RuleFor(p => p.Prezime)
                .NotEmpty().WithMessage("Ime ne moze biti prazno")
                .MinimumLength(3).WithMessage("Mora imati bar 3 slova");

        }
	}
}
