using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobCategoryMaintenences.Validators
{
    public class CreateJobCategoryMaintenenceDtoValidator : AbstractValidator<CreateJobCategoryMaintenenceDto>
    {
        public CreateJobCategoryMaintenenceDtoValidator()
        {
            Include(new IJobCategoryMaintenenceDtoValidator());
        }
    }
}
 