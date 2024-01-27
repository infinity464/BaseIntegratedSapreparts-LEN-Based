using FluentValidation;

namespace SchoolManagement.Application.DTOs.SurveyAuthority.Validators
{
    public class CreateSurveyAuthorityDtoValidator : AbstractValidator<CreateSurveyAuthorityDto>
    {
        public CreateSurveyAuthorityDtoValidator()
        {
            Include(new ISurveyAuthorityDtoValidator());
        }
    }
}
 