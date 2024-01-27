using FluentValidation;

namespace SchoolManagement.Application.DTOs.Group.Validators
{
    public class CreateGroupDtoValidator : AbstractValidator<CreateGroupDto>
    {
        public CreateGroupDtoValidator()
        {
            Include(new IGroupDtoValidator());
        }
    }
}
 