using MediatR;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Commands
{
    public class UpdateScheduleMaintenenceTypeCommand : IRequest<Unit>
    { 
        public ScheduleMaintenenceTypeDto ScheduleMaintenenceTypeDto { get; set; }
    }
}
 