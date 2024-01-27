
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ByWhom.Validators
{
    public class IByWhomDtoValidator : AbstractValidator<IByWhomDto>
    {
        public IByWhomDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
