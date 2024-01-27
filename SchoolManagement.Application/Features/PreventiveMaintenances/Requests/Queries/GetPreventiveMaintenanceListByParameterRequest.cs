using MediatR;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetPreventiveMaintenanceListByParameterRequest : IRequest<List<PreventiveMaintenanceDto>>
    {
        public int UnitId { get; set; } 
        public int DepartmentNameId { get; set; }
        public int MaintenanceTypeId { get; set; }
        public int MaintenanceCategoryId { get; set; }
        public int ItemDetailId { get; set; }
    }
}   
       