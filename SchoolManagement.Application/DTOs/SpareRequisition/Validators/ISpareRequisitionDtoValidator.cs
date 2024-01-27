
using FluentValidation;

namespace SchoolManagement.Application.DTOs.SpareRequisition.Validators
{
    public class ISpareRequisitionDtoValidator : AbstractValidator<ISpareRequisitionDto>
    {
        public ISpareRequisitionDtoValidator()
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
