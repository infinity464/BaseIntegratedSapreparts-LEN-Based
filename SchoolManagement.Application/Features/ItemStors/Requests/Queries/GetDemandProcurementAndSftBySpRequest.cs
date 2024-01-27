using MediatR;

namespace SchoolManagement.Application.Features.Demands.Requests.Queries
{
    public class GetDemandProcurementAndSftBySpRequest : IRequest<object>
    {
        //public int? UnitId { get; set; }
        //public int? DepartmentId { get; set; }
        //public int? ItemCategoryId { get; set; } 
        public int? DemandId { get; set; }
    }
}
