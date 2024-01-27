using FluentValidation;

namespace SchoolManagement.Application.DTOs.Progres.Validators
{
    public class UpdateProgresDtoValidator : AbstractValidator<ProgresDto>
    {
        public UpdateProgresDtoValidator() 
        {
            Include(new IProgresDtoValidator());

            RuleFor(b => b.ProgresId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
