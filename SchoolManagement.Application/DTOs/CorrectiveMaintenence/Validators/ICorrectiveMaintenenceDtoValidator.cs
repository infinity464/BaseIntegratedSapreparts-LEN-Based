using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.CorrectiveMaintenence.Validators
{
    public class ICorrectiveMaintenenceDtoValidator : AbstractValidator<ICorrectiveMaintenenceDto>
    {
        public ICorrectiveMaintenenceDtoValidator() 
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
