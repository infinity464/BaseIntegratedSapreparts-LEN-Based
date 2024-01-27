using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Survey.Validators
{
    public class CreateSurveyDtoValidator : AbstractValidator<CreateSurveyDto>
    {
        public CreateSurveyDtoValidator()
        {
            Include(new ISurveyDtoValidator());
        }
    }
}
