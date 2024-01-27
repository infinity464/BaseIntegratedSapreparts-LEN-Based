using MediatR;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetPreventiveMaintenanceDepartmentRequest : IRequest<List<PreventiveMaintenanceDto>>
    {
        
        public int DepartmentNameId { get; set; }
    } 
}

 