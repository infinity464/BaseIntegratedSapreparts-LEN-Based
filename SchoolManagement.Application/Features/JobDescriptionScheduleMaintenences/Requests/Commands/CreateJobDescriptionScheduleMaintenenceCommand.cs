using MediatR;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Commands
{
    public class CreateJobDescriptionScheduleMaintenenceCommand : IRequest<BaseCommandResponse>
    {
        public CreateJobDescriptionScheduleMaintenenceDto JobDescriptionScheduleMaintenenceDto { get; set; }
    }
}
