using MediatR;

namespace SchoolManagement.Application.Features.AirCraftNames.Requests.Queries
{
    public class GetSpareStockByParameterSpRequest : IRequest<object>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int EquipmentNameId { get; set; } 
    }
}
 