
using FluentValidation;

namespace SchoolManagement.Application.DTOs.SurveyItemType.Validators
{
    public class ISurveyItemTypeDtoValidator : AbstractValidator<ISurveyItemTypeDto>
    {
        public ISurveyItemTypeDtoValidator()
        {
            RuleFor(b => b.SurveyItemTypeName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
