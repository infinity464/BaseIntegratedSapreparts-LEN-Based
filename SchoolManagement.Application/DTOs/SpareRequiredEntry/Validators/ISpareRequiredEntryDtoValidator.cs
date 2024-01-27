
using FluentValidation;

namespace SchoolManagement.Application.DTOs.SpareRequiredEntry.Validators
{
    public class ISpareRequiredEntryDtoValidator : AbstractValidator<ISpareRequiredEntryDto>
    {
        public ISpareRequiredEntryDtoValidator()
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
