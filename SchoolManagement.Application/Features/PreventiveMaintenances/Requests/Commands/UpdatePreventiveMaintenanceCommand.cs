using MediatR;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Commands
{
    public class UpdatePreventiveMaintenanceCommand : IRequest<Unit>
    { 
        public PreventiveMaintenanceDto PreventiveMaintenanceDto { get; set; }
    }
}
 