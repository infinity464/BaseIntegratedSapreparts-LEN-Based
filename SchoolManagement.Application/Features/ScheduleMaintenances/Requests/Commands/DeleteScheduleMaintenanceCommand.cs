using MediatR;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Commands
{
    public class DeleteScheduleMaintenanceCommand : IRequest
    {
        public int ScheduleMaintenanceId { get; set; }
    }
} 
