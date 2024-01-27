using FluentValidation;

namespace SchoolManagement.Application.DTOs.EquipmentPrioritys.Validators
{
    public class CreateEquipmentPriorityDtoValidator : AbstractValidator<CreateEquipmentPriorityDto>
    {
        public CreateEquipmentPriorityDtoValidator()
        {
            Include(new IEquipmentPriorityDtoValidator());
        }
    }
}
 