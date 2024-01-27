using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Queries
{
    public class GetJobCategoryMaintenenceListRequest : IRequest<PagedResult<JobCategoryMaintenenceDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
