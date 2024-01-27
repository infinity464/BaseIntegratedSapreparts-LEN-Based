using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.SurveyItem.Validators
{
    public class CreateSurveyItemDtoValidator : AbstractValidator<CreateSurveyItemDto>
    {
        public CreateSurveyItemDtoValidator()  
        {
            Include(new ISurveyItemDtoValidator()); 
        }
    }
}
