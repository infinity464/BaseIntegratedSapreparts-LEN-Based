using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobCategoryMaintenences.Validators
{
    public class UpdateJobCategoryMaintenenceDtoValidator : AbstractValidator<JobCategoryMaintenenceDto>
    {
        public UpdateJobCategoryMaintenenceDtoValidator() 
        {
            Include(new IJobCategoryMaintenenceDtoValidator());

            RuleFor(b => b.JobCategoryMaintenenceId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
