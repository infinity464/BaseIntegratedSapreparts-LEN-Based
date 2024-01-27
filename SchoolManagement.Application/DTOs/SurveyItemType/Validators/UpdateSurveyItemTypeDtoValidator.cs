using FluentValidation;

namespace SchoolManagement.Application.DTOs.SurveyItemType.Validators
{
    public class UpdateSurveyItemTypeDtoValidator : AbstractValidator<SurveyItemTypeDto>
    {
        public UpdateSurveyItemTypeDtoValidator() 
        {
            Include(new ISurveyItemTypeDtoValidator());

            RuleFor(b => b.SurveyItemTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
