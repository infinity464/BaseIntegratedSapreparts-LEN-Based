
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Progres.Validators
{
    public class IProgresDtoValidator : AbstractValidator<IProgresDto>
    {
        public IProgresDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
