using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences.Validators
{
    public class UpdateJobDescriptionScheduleMaintenenceDtoValidator : AbstractValidator<JobDescriptionScheduleMaintenenceDto>
    {
        public UpdateJobDescriptionScheduleMaintenenceDtoValidator() 
        {
            Include(new IJobDescriptionScheduleMaintenenceDtoValidator());

            RuleFor(b => b.JobDescriptionScheduleMaintenenceId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
