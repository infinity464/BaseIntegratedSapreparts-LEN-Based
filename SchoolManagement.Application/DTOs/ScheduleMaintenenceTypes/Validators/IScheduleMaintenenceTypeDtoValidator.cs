
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes.Validators
{
    public class IScheduleMaintenenceTypeDtoValidator : AbstractValidator<IScheduleMaintenenceTypeDto>
    {
        //public IScheduleMaintenenceTypeDtoValidator()
        //{
        //    RuleFor(b => b.Name)
        //        .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        //}
    }
}
