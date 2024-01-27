
using FluentValidation;

namespace SchoolManagement.Application.DTOs.MaintenanceAuthority.Validators
{
    public class IMaintenanceAuthorityDtoValidator : AbstractValidator<IMaintenanceAuthorityDto>
    {
        public IMaintenanceAuthorityDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
