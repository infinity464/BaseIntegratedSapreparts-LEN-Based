
using FluentValidation;

namespace SchoolManagement.Application.DTOs.IssueType.Validators
{
    public class IIssueTypeDtoValidator : AbstractValidator<IIssueTypeDto>
    {
        public IIssueTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
