using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.StatusOfJob;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.StatusOfJobs.Requests.Queries
{
    public class GetStatusOfJobListRequest : IRequest<PagedResult<StatusOfJobDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
