using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Requests.Queries
{
    public class GetSelectedPartNoFromDemandByItemStatusAndProcureStatusRequest : IRequest<List<SelectedModel>>
    {
        public int ItemStatusId { get; set; } 
        public int ProcurementStatus { get; set; }
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
    }
}   
     