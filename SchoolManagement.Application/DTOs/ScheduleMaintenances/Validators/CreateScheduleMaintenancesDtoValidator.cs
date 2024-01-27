using FluentValidation;

namespace SchoolManagement.Application.DTOs.ScheduleMaintenances.Validators
{
    public class CreateScheduleMaintenanceDtoValidator : AbstractValidator<CreateScheduleMaintenanceDto>
    {
        public CreateScheduleMaintenanceDtoValidator()
        {
            Include(new IScheduleMaintenanceDtoValidator());
        }
    }
}
 