using FluentValidation;

namespace SchoolManagement.Application.DTOs.IssueType.Validators
{
    public class UpdateIssueTypeDtoValidator : AbstractValidator<IssueTypeDto>
    {
        public UpdateIssueTypeDtoValidator() 
        {
            Include(new IIssueTypeDtoValidator());

            RuleFor(b => b.IssueTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
