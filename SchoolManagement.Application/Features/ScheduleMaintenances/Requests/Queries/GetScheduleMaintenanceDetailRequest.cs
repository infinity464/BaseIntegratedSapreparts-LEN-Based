using MediatR;
using SchoolManagement.Application.DTOs.ScheduleMaintenances;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Queries
{
    public class GetScheduleMaintenanceDetailRequest : IRequest<ScheduleMaintenanceDto>
    {
        public int ScheduleMaintenanceId { get; set; }
    }
}
