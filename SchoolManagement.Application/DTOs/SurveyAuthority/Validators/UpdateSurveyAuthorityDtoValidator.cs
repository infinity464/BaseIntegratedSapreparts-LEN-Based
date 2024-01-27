using FluentValidation;

namespace SchoolManagement.Application.DTOs.SurveyAuthority.Validators
{
    public class UpdateSurveyAuthorityDtoValidator : AbstractValidator<SurveyAuthorityDto>
    {
        public UpdateSurveyAuthorityDtoValidator() 
        {
            Include(new ISurveyAuthorityDtoValidator());

            RuleFor(b => b.SurveyAuthorityId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
