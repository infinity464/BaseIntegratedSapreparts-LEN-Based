
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReturnLogs.Validators
{
    public class IReturnLogDtoValidator : AbstractValidator<IReturnLogDto>
    {
        public IReturnLogDtoValidator()
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
