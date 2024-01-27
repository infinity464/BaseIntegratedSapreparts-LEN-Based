using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.DTOs.PreventiveMaintenances;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Commands
{ 
    public class PreventiveMaintenenceCompletedCommand : IRequest<Unit>
    {
        public PreventiveMaintenanceCompletedDto PreventiveMaintenenceCompletedDto { get; set; }
    }
}
     