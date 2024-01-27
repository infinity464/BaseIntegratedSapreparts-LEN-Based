using FluentValidation;

namespace SchoolManagement.Application.DTOs.SpareRequiredStatus.Validators
{
    public class UpdateSpareRequiredStatusDtoValidator : AbstractValidator<SpareRequiredStatusDto>
    {
        public UpdateSpareRequiredStatusDtoValidator() 
        {
            Include(new ISpareRequiredStatusDtoValidator());

            RuleFor(b => b.SpareRequiredStatusId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
