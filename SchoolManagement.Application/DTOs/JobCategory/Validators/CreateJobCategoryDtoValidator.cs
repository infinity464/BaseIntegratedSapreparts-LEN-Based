using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobCategory.Validators
{
    public class CreateJobCategoryDtoValidator : AbstractValidator<CreateJobCategoryDto>
    {
        public CreateJobCategoryDtoValidator()
        {
            Include(new IJobCategoryDtoValidator());
        }
    }
}
 