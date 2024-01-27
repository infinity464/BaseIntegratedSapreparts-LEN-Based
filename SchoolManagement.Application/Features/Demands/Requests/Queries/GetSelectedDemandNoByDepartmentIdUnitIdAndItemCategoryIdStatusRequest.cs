using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Requests.Queries
{
    public class GetSelectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; } 
        public int DepartmentNameId { get; set; }
        public int ItemCategoryId { get; set; }
        public int StoreStatus { get; set; }
        public int SftStatus { get; set; } 
        public int ItemStatusId { get; set; } 
    }
}   
      