using MediatR;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetPreventiveMaintenanceDetailRequest : IRequest<PreventiveMaintenanceDto>
    {
        public int PreventiveMaintenanceId { get; set; }
    }
}
