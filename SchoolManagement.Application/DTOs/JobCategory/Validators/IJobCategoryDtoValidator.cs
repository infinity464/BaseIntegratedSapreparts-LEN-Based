
using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobCategory.Validators
{
    public class IJobCategoryDtoValidator : AbstractValidator<IJobCategoryDto>
    {
        public IJobCategoryDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
