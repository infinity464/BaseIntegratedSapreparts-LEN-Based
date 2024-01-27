using MediatR;

namespace SchoolManagement.Application.Features.Procurements.Requests.Queries
{
    public class GetProcurementByParametersSpRequest : IRequest<object>
    {
        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
        public int ItemCategoryId { get; set; }
        public int EquipmentNameId { get; set; }
        public int DemandId { get; set; }
    }
}
