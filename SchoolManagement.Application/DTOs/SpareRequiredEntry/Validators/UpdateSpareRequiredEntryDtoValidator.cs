using FluentValidation;

namespace SchoolManagement.Application.DTOs.SpareRequiredEntry.Validators
{
    public class UpdateSpareRequiredEntryDtoValidator : AbstractValidator<SpareRequiredEntryDto>
    {
        public UpdateSpareRequiredEntryDtoValidator() 
        {
            Include(new ISpareRequiredEntryDtoValidator());

            RuleFor(b => b.SpareRequiredEntryId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
