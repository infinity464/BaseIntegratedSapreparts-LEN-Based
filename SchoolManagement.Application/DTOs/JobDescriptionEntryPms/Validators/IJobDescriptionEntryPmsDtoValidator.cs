
using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobDescriptionEntryPms.Validators
{
    public class IJobDescriptionEntryPmsDtoValidator : AbstractValidator<IJobDescriptionEntryPmsDto>
    {
        public IJobDescriptionEntryPmsDtoValidator()
        {
            RuleFor(b => b.Jic)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
