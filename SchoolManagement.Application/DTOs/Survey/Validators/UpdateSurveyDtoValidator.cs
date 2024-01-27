using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace SchoolManagement.Application.DTOs.Survey.Validators
{
    public class UpdateSurveyDtoValidator : AbstractValidator<SurveyDto>
    {
        //public UpdateSurveyDtoValidator()
        //{
        //    Include(new ISurveyDtoValidator());

        //    RuleFor(p => p.ReligionId).NotNull().WithMessage("{PropertyName} must be present");

        //    RuleFor(p => p.SurveyId).NotNull().WithMessage("{PropertyName} must be present");
        //}
    }
}
