using MediatR;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Queries
{
    public class GetJobDescriptionScheduleMaintenenceDetailRequest : IRequest<JobDescriptionScheduleMaintenenceDto>
    {
        public int JobDescriptionScheduleMaintenenceId { get; set; }
    }
}
