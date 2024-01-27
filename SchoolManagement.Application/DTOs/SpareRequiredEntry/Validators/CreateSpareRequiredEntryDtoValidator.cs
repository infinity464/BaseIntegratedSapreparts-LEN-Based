using FluentValidation;

namespace SchoolManagement.Application.DTOs.SpareRequiredEntry.Validators
{
    public class CreateSpareRequiredEntryDtoValidator : AbstractValidator<CreateSpareRequiredEntryDto>
    {
        public CreateSpareRequiredEntryDtoValidator()
        {
            Include(new ISpareRequiredEntryDtoValidator());
        }
    }
}
 