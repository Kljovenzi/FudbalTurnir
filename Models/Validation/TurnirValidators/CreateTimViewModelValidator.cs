using FluentValidation;
using FudbalskiTurnir_FilipNikolic.Models.ViewModel;

namespace FudbalskiTurnir_FilipNikolic.Models.Validation.TurnirValidators
{
	public class CreateTimViewModelValidator : AbstractValidator<CreateTimViewModel>
	{
		public CreateTimViewModelValidator()
		{
			RuleFor(p => p.Ime)
				.NotNull().WithMessage("Ime mora biti popunjeno");
		}
	}
}
