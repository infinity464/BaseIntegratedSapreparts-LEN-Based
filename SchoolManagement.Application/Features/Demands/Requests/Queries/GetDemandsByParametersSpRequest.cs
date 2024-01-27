using MediatR;

namespace SchoolManagement.Application.Features.Demands.Requests.Queries
{
    public class GetDemandsByParametersSpRequest : IRequest<object>
    {
        public int? UnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? ItemNameId { get; set; }
    }
}
