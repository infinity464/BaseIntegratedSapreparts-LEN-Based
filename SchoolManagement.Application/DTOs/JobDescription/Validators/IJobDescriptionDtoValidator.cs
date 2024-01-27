
using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobDescription.Validators
{
    public class IJobDescriptionDtoValidator : AbstractValidator<IJobDescriptionDto>
    {
        public IJobDescriptionDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
