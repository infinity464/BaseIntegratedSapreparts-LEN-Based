using MediatR;

namespace SchoolManagement.Application.Features.IssueTypes.Requests.Commands
{
    public class DeleteIssueTypeCommand : IRequest
    {
        public int IssueTypeId { get; set; }
    }
} 
