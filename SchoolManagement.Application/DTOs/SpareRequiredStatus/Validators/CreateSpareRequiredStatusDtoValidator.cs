using FluentValidation;

namespace SchoolManagement.Application.DTOs.SpareRequiredStatus.Validators
{
    public class CreateSpareRequiredStatusDtoValidator : AbstractValidator<CreateSpareRequiredStatusDto>
    {
        public CreateSpareRequiredStatusDtoValidator()
        {
            Include(new ISpareRequiredStatusDtoValidator());
        }
    }
}
 