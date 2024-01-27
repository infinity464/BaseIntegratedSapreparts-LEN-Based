using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReturnStatus.Validators 
{
    public class UpdateReturnStatusDtoValidator : AbstractValidator<ReturnStatusDto>
    {
        public UpdateReturnStatusDtoValidator() 
        {
            Include(new IReturnStatusDtoValidator());

            RuleFor(b => b.ReturnStatusId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
