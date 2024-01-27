using MediatR;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetPreventiveListForMonitorintByParametersSpRequest : IRequest<object>
    {
        public int? UnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? MaintenanceCategoryId { get; set; }
        public int? MaintenenceTypeId { get; set; }

        //public int? EquipmentNameId { get; set; } 
    }
}
