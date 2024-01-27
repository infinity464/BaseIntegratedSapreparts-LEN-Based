using FluentValidation;

namespace SchoolManagement.Application.DTOs.StatusOfJob.Validators
{
    public class CreateStatusOfJobDtoValidator : AbstractValidator<CreateStatusOfJobDto>
    {
        public CreateStatusOfJobDtoValidator()
        {
            Include(new IStatusOfJobDtoValidator());
        }
    }
}
 