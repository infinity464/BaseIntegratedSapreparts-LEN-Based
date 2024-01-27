using FluentValidation;

namespace SchoolManagement.Application.DTOs.SpareRequisition.Validators
{
    public class UpdateSpareRequisitionDtoValidator : AbstractValidator<SpareRequisitionDto>
    {
        public UpdateSpareRequisitionDtoValidator() 
        {
            Include(new ISpareRequisitionDtoValidator());

            RuleFor(b => b.SpareRequisitionId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
