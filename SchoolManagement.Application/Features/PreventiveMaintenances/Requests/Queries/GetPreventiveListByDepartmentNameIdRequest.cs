using MediatR;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetPreventiveListByDepartmentNameIdRequest : IRequest<List<PreventiveMaintenanceDto>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        //public int ItemCategoryId { get; set; }
    } 
}
 
 