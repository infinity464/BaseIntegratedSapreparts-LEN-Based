
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Opinion.Validators
{
    public class IOpinionDtoValidator : AbstractValidator<IOpinionDto>
    {
        public IOpinionDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
