using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.JobDescription;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.JobDescriptions.Requests.Queries
{
    public class GetJobDescriptionListRequest : IRequest<PagedResult<JobDescriptionDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
