
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ScheduleMaintenances.Validators
{
    public class IScheduleMaintenanceDtoValidator : AbstractValidator<IScheduleMaintenanceDto>
    {
        //public IScheduleMaintenanceDtoValidator()
        //{
        //    RuleFor(b => b.Name)
        //        .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        //}
    }
}
