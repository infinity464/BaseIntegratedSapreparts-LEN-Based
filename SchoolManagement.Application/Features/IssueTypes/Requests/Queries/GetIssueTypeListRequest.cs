using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.IssueType;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.IssueTypes.Requests.Queries
{
    public class GetIssueTypeListRequest : IRequest<PagedResult<IssueTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
