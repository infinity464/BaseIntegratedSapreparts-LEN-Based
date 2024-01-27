using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobCategory.Validators
{
    public class UpdateJobCategoryDtoValidator : AbstractValidator<JobCategoryDto>
    {
        public UpdateJobCategoryDtoValidator() 
        {
            Include(new IJobCategoryDtoValidator());

            RuleFor(b => b.JobCategoryId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
