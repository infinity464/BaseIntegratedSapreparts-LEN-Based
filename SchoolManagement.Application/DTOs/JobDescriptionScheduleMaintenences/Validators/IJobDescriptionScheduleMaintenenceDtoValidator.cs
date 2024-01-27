
using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences.Validators
{
    public class IJobDescriptionScheduleMaintenenceDtoValidator : AbstractValidator<IJobDescriptionScheduleMaintenenceDto>
    {
        //public IJobDescriptionScheduleMaintenenceDtoValidator()
        //{
        //    RuleFor(b => b.Name)
        //        .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        //}
    }
}
