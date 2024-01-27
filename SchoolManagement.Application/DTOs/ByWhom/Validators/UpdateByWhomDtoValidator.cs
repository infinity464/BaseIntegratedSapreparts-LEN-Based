using FluentValidation;

namespace SchoolManagement.Application.DTOs.ByWhom.Validators
{
    public class UpdateByWhomDtoValidator : AbstractValidator<ByWhomDto>
    {
        public UpdateByWhomDtoValidator() 
        {
            Include(new IByWhomDtoValidator());

            RuleFor(b => b.ByWhomId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
