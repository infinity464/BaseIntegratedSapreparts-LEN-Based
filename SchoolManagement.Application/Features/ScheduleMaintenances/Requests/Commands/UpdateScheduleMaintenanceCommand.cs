using MediatR;
using SchoolManagement.Application.DTOs.ScheduleMaintenances;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Commands
{
    public class UpdateScheduleMaintenanceCommand : IRequest<Unit>
    { 
        public ScheduleMaintenanceDto ScheduleMaintenanceDto { get; set; }
    }
}
 