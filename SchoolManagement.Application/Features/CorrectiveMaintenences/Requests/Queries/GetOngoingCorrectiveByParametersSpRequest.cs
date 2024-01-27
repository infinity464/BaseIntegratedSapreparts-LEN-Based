using MediatR;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries
{
    public class GetOngoingCorrectiveByParametersSpRequest : IRequest<object>
    { 
        public int? UnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? ItemNameId { get; set; }
    }
}
