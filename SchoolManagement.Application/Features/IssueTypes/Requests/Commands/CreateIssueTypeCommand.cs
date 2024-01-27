using MediatR;
using SchoolManagement.Application.DTOs.IssueType;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.IssueTypes.Requests.Commands
{
    public class CreateIssueTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateIssueTypeDto IssueTypeDto { get; set; }
    }
}
