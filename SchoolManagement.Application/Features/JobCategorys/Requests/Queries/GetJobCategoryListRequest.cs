using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.JobCategory;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.JobCategorys.Requests.Queries
{
    public class GetJobCategoryListRequest : IRequest<PagedResult<JobCategoryDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
