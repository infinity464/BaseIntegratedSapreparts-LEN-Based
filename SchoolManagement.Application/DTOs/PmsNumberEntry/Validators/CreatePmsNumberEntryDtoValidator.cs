using FluentValidation;

namespace SchoolManagement.Application.DTOs.PmsNumberEntry.Validators
{
    public class CreatePmsNumberEntryDtoValidator : AbstractValidator<CreatePmsNumberEntryDto>
    {
        public CreatePmsNumberEntryDtoValidator()
        {
            Include(new IPmsNumberEntryDtoValidator());
        }
    }
}
 