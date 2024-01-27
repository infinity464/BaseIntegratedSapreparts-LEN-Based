using FluentValidation;

namespace SchoolManagement.Application.DTOs.MaintenanceAuthority.Validators
{
    public class CreateMaintenanceAuthorityDtoValidator : AbstractValidator<CreateMaintenanceAuthorityDto>
    {
        public CreateMaintenanceAuthorityDtoValidator()
        {
            Include(new IMaintenanceAuthorityDtoValidator());
        }
    }
}
 