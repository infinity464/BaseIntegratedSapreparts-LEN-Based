using MediatR;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Commands
{
    public class UpdateJobDescriptionScheduleMaintenenceCommand : IRequest<Unit>
    { 
        public JobDescriptionScheduleMaintenenceDto JobDescriptionScheduleMaintenenceDto { get; set; }
    }
}
  