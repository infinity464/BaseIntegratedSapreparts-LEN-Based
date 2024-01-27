using FluentValidation;

namespace SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes.Validators
{
    public class UpdateScheduleMaintenenceTypeDtoValidator : AbstractValidator<ScheduleMaintenenceTypeDto>
    {
        public UpdateScheduleMaintenenceTypeDtoValidator() 
        {
            Include(new IScheduleMaintenenceTypeDtoValidator());

            RuleFor(b => b.ScheduleMaintenenceTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
