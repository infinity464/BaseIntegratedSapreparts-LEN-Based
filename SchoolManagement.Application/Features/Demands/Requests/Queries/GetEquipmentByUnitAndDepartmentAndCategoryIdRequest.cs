using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Requests.Queries
{
    public class GetEquipmenteByUnitAndDepartmentAndCategoryIdRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemCategoryId { get; set; }
    }
}  
  