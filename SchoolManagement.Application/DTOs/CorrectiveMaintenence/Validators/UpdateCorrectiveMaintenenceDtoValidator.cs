using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.CorrectiveMaintenence.Validators
{
    public class UpdateCorrectiveMaintenenceDtoValidator : AbstractValidator<CorrectiveMaintenenceDto>
    {
        public UpdateCorrectiveMaintenenceDtoValidator()
        {
            Include(new ICorrectiveMaintenenceDtoValidator());

            //RuleFor(b => b.CorrectiveMaintenenceId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

