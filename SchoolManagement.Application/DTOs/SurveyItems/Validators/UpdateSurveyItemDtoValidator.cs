using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.SurveyItem.Validators
{
    public class UpdateSurveyItemDtoValidator : AbstractValidator<SurveyItemDto>
    {
        public UpdateSurveyItemDtoValidator()
        {
            Include(new ISurveyItemDtoValidator());

            RuleFor(b => b.SurveyItemId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

