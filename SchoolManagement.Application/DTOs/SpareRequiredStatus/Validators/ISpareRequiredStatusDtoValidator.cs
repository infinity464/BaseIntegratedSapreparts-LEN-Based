
using FluentValidation;

namespace SchoolManagement.Application.DTOs.SpareRequiredStatus.Validators
{
    public class ISpareRequiredStatusDtoValidator : AbstractValidator<ISpareRequiredStatusDto>
    {
        public ISpareRequiredStatusDtoValidator()
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
