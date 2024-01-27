
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReturnStatus.Validators
{
    public class IReturnStatusDtoValidator : AbstractValidator<IReturnStatusDto>
    {
        public IReturnStatusDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
