
using FluentValidation;

namespace SchoolManagement.Application.DTOs.PmsNumberEntry.Validators
{
    public class IPmsNumberEntryDtoValidator : AbstractValidator<IPmsNumberEntryDto>
    {
        public IPmsNumberEntryDtoValidator()
        {
            RuleFor(b => b.PmsNo)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
