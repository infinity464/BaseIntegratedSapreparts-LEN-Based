using MediatR;
using SchoolManagement.Application.DTOs.JobCategory;

namespace SchoolManagement.Application.Features.JobCategorys.Requests.Queries
{
    public class GetJobCategoryDetailRequest : IRequest<JobCategoryDto>
    {
        public int JobCategoryId { get; set; }
    }
}
