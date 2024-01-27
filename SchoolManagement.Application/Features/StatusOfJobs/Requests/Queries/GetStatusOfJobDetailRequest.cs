using MediatR;
using SchoolManagement.Application.DTOs.StatusOfJob;

namespace SchoolManagement.Application.Features.StatusOfJobs.Requests.Queries
{
    public class GetStatusOfJobDetailRequest : IRequest<StatusOfJobDto>
    {
        public int StatusOfJobId { get; set; }
    }
}
