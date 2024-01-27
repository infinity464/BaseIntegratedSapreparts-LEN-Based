using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobDescription.Validators
{
    public class UpdateJobDescriptionDtoValidator : AbstractValidator<JobDescriptionDto>
    {
        public UpdateJobDescriptionDtoValidator() 
        {
            Include(new IJobDescriptionDtoValidator());

            RuleFor(b => b.JobDescriptionId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
