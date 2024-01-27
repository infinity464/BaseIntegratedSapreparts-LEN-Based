using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReturnLogs.Validators
{
    public class UpdateReturnLogDtoValidator : AbstractValidator<ReturnLogDto>
    {
        public UpdateReturnLogDtoValidator() 
        {
            Include(new IReturnLogDtoValidator());

            RuleFor(b => b.ReturnLogId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
