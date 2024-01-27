using MediatR;

namespace SchoolManagement.Application.Features.Acceptances.Requests.Queries
{
    public class GetSftByParametersSpRequest : IRequest<object>
    {
        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
        public int DemandId { get; set; }
    }
}
