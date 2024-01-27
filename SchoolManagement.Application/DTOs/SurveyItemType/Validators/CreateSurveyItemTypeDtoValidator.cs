using FluentValidation;

namespace SchoolManagement.Application.DTOs.SurveyItemType.Validators
{
    public class CreateSurveyItemTypeDtoValidator : AbstractValidator<CreateSurveyItemTypeDto>
    {
        public CreateSurveyItemTypeDtoValidator()
        {
            Include(new ISurveyItemTypeDtoValidator());
        }
    }
}
 