using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobDescriptionEntryPms.Validators
{
    public class CreateJobDescriptionEntryPmsDtoValidator : AbstractValidator<CreateJobDescriptionEntryPmsDto>
    {
        public CreateJobDescriptionEntryPmsDtoValidator()
        {
            Include(new IJobDescriptionEntryPmsDtoValidator());
        }
    }
}
 