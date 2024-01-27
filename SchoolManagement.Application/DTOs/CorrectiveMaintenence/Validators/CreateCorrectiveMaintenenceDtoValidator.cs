using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CorrectiveMaintenence.Validators
{
    public class CreateCorrectiveMaintenenceDtoValidator : AbstractValidator<CreateCorrectiveMaintenenceDto>
    {
        public CreateCorrectiveMaintenenceDtoValidator()  
        {
            Include(new ICorrectiveMaintenenceDtoValidator()); 
        }
    }
}
