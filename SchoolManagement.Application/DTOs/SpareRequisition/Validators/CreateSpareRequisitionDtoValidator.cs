using FluentValidation;

namespace SchoolManagement.Application.DTOs.SpareRequisition.Validators
{
    public class CreateSpareRequisitionDtoValidator : AbstractValidator<CreateSpareRequisitionDto>
    {
        public CreateSpareRequisitionDtoValidator()
        {
            Include(new ISpareRequisitionDtoValidator());
        }
    }
}
 