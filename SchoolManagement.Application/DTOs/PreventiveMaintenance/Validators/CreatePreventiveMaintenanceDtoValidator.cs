using FluentValidation;

namespace SchoolManagement.Application.DTOs.PreventiveMaintenance.Validators
{
    public class CreatePreventiveMaintenanceDtoValidator : AbstractValidator<CreatePreventiveMaintenanceDto>
    {
        public CreatePreventiveMaintenanceDtoValidator()
        {
            Include(new IPreventiveMaintenanceDtoValidator());
        }
    }
}
 