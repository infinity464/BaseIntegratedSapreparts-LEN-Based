using MediatR;
using SchoolManagement.Application.DTOs.IssueType;

namespace SchoolManagement.Application.Features.IssueTypes.Requests.Commands
{
    public class UpdateIssueTypeCommand : IRequest<Unit>
    { 
        public IssueTypeDto IssueTypeDto { get; set; }
    }
}
 