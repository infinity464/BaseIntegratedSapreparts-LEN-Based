using MediatR;

namespace SchoolManagement.Application.Features.EquipmentNames.Requests.Queries
{
    public class GetToolsCountSpRequest : IRequest<object>
    { 
        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
        public int ItemCategoryId { get; set; } 
    } 
}
 