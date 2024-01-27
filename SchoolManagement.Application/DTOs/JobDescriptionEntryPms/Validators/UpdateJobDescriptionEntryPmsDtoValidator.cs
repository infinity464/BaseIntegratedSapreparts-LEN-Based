using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobDescriptionEntryPms.Validators
{
    public class UpdateJobDescriptionEntryPmsDtoValidator : AbstractValidator<JobDescriptionEntryPmsDto>
    {
        public UpdateJobDescriptionEntryPmsDtoValidator() 
        {
            Include(new IJobDescriptionEntryPmsDtoValidator());

            RuleFor(b => b.JobDescriptionEntryPmsId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
