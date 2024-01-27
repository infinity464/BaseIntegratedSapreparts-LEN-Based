using FluentValidation;

namespace SchoolManagement.Application.DTOs.MaintenanceAuthority.Validators
{
    public class UpdateMaintenanceAuthorityDtoValidator : AbstractValidator<MaintenanceAuthorityDto>
    {
        public UpdateMaintenanceAuthorityDtoValidator() 
        {
            Include(new IMaintenanceAuthorityDtoValidator());

            RuleFor(b => b.MaintenanceAuthorityId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
