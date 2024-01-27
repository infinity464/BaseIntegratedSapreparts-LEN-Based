using FluentValidation;

namespace SchoolManagement.Application.DTOs.PreventiveMaintenance.Validators
{
    public class UpdatePreventiveMaintenanceDtoValidator : AbstractValidator<PreventiveMaintenanceDto>
    {
        public UpdatePreventiveMaintenanceDtoValidator() 
        {
            Include(new IPreventiveMaintenanceDtoValidator());

            RuleFor(b => b.PreventiveMaintenanceId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
