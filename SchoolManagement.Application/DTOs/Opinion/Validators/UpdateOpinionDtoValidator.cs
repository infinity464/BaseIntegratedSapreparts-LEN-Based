using FluentValidation;

namespace SchoolManagement.Application.DTOs.Opinion.Validators
{
    public class UpdateOpinionDtoValidator : AbstractValidator<OpinionDto>
    {
        public UpdateOpinionDtoValidator() 
        {
            Include(new IOpinionDtoValidator());

            RuleFor(b => b.OpinionId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
