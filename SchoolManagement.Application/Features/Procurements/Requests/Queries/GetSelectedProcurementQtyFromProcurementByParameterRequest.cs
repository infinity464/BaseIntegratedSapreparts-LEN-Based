using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Requests.Queries
{
    public class GetSelectedProcurementQtyFromProcurementByParameterRequest : IRequest<List<SelectedModel>>
    {
        public int DemandId { get; set; }
        public int UnitId { get; set; } 
        public int DepartmentNameId { get; set; } 
        public int ItemCategoryId { get; set; }  
    }
}   
       