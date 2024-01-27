using MediatR;
using SchoolManagement.Application.DTOs.EquipmentName;

namespace SchoolManagement.Application.Features.EquipmentNames.Requests.Queries
{
    public class GetEquipmentNameListByDepartmentNameIdRequest : IRequest<List<EquipmentNameDto>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemCategoryId { get; set; }
    } 
}
 
 