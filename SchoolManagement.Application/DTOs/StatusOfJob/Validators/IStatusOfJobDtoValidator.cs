
using FluentValidation;

namespace SchoolManagement.Application.DTOs.StatusOfJob.Validators
{
    public class IStatusOfJobDtoValidator : AbstractValidator<IStatusOfJobDto>
    {
        public IStatusOfJobDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
