using MediatR;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Commands
{
    public class CreateScheduleMaintenenceTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateScheduleMaintenenceTypeDto ScheduleMaintenenceTypeDto { get; set; }
    }
}
