using FluentValidation;

namespace SchoolManagement.Application.DTOs.PmsNumberEntry.Validators
{
    public class UpdatePmsNumberEntryDtoValidator : AbstractValidator<PmsNumberEntryDto>
    {
        public UpdatePmsNumberEntryDtoValidator() 
        {
            Include(new IPmsNumberEntryDtoValidator());

            RuleFor(b => b.PmsNumberEntryId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
