
using FluentValidation;

namespace SchoolManagement.Application.DTOs.EquipmentPrioritys.Validators
{
    public class IEquipmentPriorityDtoValidator : AbstractValidator<IEquipmentPriorityDto>
    {
        public IEquipmentPriorityDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
