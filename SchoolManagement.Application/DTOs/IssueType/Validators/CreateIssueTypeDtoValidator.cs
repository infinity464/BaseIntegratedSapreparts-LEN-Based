using FluentValidation;

namespace SchoolManagement.Application.DTOs.IssueType.Validators
{
    public class CreateIssueTypeDtoValidator : AbstractValidator<CreateIssueTypeDto>
    {
        public CreateIssueTypeDtoValidator()
        {
            Include(new IIssueTypeDtoValidator());
        }
    }
}
 