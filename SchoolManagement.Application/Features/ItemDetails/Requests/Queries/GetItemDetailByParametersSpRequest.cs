using MediatR;

namespace SchoolManagement.Application.Features.ItemDetails.Requests.Queries
{
    public class GetItemDetailByParametersSpRequest : IRequest<object>
    {
        public int? UnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? EquipmentNameId { get; set; }
    }
}
