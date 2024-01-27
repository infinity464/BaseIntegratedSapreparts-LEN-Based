using FluentValidation;

namespace SchoolManagement.Application.DTOs.Opinion.Validators
{
    public class CreateOpinionDtoValidator : AbstractValidator<CreateOpinionDto>
    {
        public CreateOpinionDtoValidator()
        {
            Include(new IOpinionDtoValidator());
        }
    }
}
 