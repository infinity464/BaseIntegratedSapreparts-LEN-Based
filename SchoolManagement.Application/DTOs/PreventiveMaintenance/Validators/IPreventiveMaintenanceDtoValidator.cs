
using FluentValidation;

namespace SchoolManagement.Application.DTOs.PreventiveMaintenance.Validators
{
    public class IPreventiveMaintenanceDtoValidator : AbstractValidator<IPreventiveMaintenanceDto>
    {
        public IPreventiveMaintenanceDtoValidator()
        {
            //RuleFor(b => b.Pmsno)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
