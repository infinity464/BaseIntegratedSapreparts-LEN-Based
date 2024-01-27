using FluentValidation;

namespace SchoolManagement.Application.DTOs.Group.Validators
{
    public class UpdateGroupDtoValidator : AbstractValidator<GroupDto>
    {
        public UpdateGroupDtoValidator() 
        {
            Include(new IGroupDtoValidator());

            RuleFor(b => b.GroupId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
