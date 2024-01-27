using MediatR;
using SchoolManagement.Application.DTOs.IssueType;

namespace SchoolManagement.Application.Features.IssueTypes.Requests.Queries
{
    public class GetIssueTypeDetailRequest : IRequest<IssueTypeDto>
    {
        public int IssueTypeId { get; set; }
    }
}
