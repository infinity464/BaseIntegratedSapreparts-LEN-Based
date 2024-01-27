using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobDescription.Validators
{
    public class CreateJobDescriptionDtoValidator : AbstractValidator<CreateJobDescriptionDto>
    {
        public CreateJobDescriptionDtoValidator()
        {
            Include(new IJobDescriptionDtoValidator());
        }
    }
}
 