using MediatR;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Queries
{
    public class GetJobCategoryMaintenenceDetailRequest : IRequest<JobCategoryMaintenenceDto>
    {
        public int JobCategoryMaintenenceId { get; set; }
    }
}
