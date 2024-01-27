using FluentValidation;

namespace SchoolManagement.Application.DTOs.EquipmentPrioritys.Validators
{
    public class UpdateEquipmentPriorityDtoValidator : AbstractValidator<EquipmentPriorityDto>
    {
        public UpdateEquipmentPriorityDtoValidator() 
        {
            Include(new IEquipmentPriorityDtoValidator());

            RuleFor(b => b.EquipmentPriorityId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
