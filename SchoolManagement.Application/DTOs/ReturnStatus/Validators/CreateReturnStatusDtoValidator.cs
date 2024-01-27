using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReturnStatus.Validators 
{
    public class CreateReturnStatusDtoValidator : AbstractValidator<CreateReturnStatusDto>
    {
        public CreateReturnStatusDtoValidator()
        {
            Include(new IReturnStatusDtoValidator());
        }
    }
}
 