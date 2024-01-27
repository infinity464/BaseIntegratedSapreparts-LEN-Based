using FluentValidation;

namespace SchoolManagement.Application.DTOs.StatusOfJob.Validators
{
    public class UpdateStatusOfJobDtoValidator : AbstractValidator<StatusOfJobDto>
    {
        public UpdateStatusOfJobDtoValidator() 
        {
            Include(new IStatusOfJobDtoValidator());

            RuleFor(b => b.StatusOfJobId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
