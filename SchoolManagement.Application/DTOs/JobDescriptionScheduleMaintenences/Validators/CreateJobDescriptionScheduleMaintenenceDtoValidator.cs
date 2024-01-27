using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences.Validators
{
    public class CreateJobDescriptionScheduleMaintenenceDtoValidator : AbstractValidator<CreateJobDescriptionScheduleMaintenenceDto>
    {
        public CreateJobDescriptionScheduleMaintenenceDtoValidator()
        {
            Include(new IJobDescriptionScheduleMaintenenceDtoValidator());
        }
    }
}
 