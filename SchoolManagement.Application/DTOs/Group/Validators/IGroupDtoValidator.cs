
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Group.Validators
{
    public class IGroupDtoValidator : AbstractValidator<IGroupDto>
    {
        public IGroupDtoValidator()
        {
            RuleFor(b => b.GroupName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
