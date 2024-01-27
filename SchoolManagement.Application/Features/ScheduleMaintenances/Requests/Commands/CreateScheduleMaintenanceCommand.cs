using MediatR;
using SchoolManagement.Application.DTOs.ScheduleMaintenances;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Commands
{
    public class CreateScheduleMaintenanceCommand : IRequest<BaseCommandResponse>
    {
        public CreateScheduleMaintenanceDto ScheduleMaintenanceDto { get; set; }
    }
}
