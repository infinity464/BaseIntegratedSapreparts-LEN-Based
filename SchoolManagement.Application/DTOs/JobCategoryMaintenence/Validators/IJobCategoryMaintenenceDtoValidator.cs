
using FluentValidation;

namespace SchoolManagement.Application.DTOs.JobCategoryMaintenences.Validators
{
    public class IJobCategoryMaintenenceDtoValidator : AbstractValidator<IJobCategoryMaintenenceDto>
    {
        //public IJobCategoryMaintenenceDtoValidator()
        //{
        //    RuleFor(b => b.Name)
        //        .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        //}
    }
}
