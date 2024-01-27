using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetSelectSubCategoryByUnitAndDepartmentAndItemDetailAndCategoryIdRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemDetailId { get; set; }
        public int MaintenanceTypeId { get; set; }
    }
}  
  