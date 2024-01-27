using FluentValidation;

namespace SchoolManagement.Application.DTOs.Progres.Validators
{
    public class CreateProgresDtoValidator : AbstractValidator<CreateProgresDto>
    {
        public CreateProgresDtoValidator()
        {
            Include(new IProgresDtoValidator());
        }
    }
}
 