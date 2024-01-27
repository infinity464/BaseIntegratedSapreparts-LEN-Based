using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Survey.Validators
{
    public class ISurveyDtoValidator : AbstractValidator<ISurveyDto>
    {
        public ISurveyDtoValidator()
        {
            //RuleFor(p => p.CastName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            //RuleFor(p => p.ReligionId)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .GreaterThan(0).WithMessage("{PropertyName} must be at least 1.");

            
        }
    }
}
