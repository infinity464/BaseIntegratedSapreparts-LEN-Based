using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReturnLogs.Validators
{
    public class CreateReturnLogDtoValidator : AbstractValidator<CreateReturnLogDto>
    {
        public CreateReturnLogDtoValidator()
        {
            Include(new IReturnLogDtoValidator());
        }
    }
}
 