
using FluentValidation;

namespace SchoolManagement.Application.DTOs.SurveyAuthority.Validators
{
    public class ISurveyAuthorityDtoValidator : AbstractValidator<ISurveyAuthorityDto>
    {
        public ISurveyAuthorityDtoValidator()
        {
            RuleFor(b => b.SurveyAuthorityName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
