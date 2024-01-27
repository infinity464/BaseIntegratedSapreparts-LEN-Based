using FluentValidation;

namespace SchoolManagement.Application.DTOs.ScheduleMaintenances.Validators
{
    public class UpdateScheduleMaintenanceDtoValidator : AbstractValidator<ScheduleMaintenanceDto>
    {
        public UpdateScheduleMaintenanceDtoValidator() 
        {
            Include(new IScheduleMaintenanceDtoValidator());

            RuleFor(b => b.ScheduleMaintenanceId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
