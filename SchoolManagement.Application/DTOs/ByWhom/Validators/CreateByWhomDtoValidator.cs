using FluentValidation;

namespace SchoolManagement.Application.DTOs.ByWhom.Validators
{
    public class CreateByWhomDtoValidator : AbstractValidator<CreateByWhomDto>
    {
        public CreateByWhomDtoValidator()
        {
            Include(new IByWhomDtoValidator());
        }
    }
}
 