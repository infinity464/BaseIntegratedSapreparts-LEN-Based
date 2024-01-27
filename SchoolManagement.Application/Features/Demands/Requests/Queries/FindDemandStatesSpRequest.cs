using MediatR;

namespace SchoolManagement.Application.Features.Demands.Requests.Queries
{
    public class FindDemandStatesSpRequest : IRequest<object>
    {
        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
    }
}
