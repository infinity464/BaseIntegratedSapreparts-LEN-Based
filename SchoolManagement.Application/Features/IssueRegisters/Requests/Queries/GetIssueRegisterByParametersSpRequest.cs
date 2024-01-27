using MediatR;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Queries
{
    public class GetIssueRegisterByParametersSpRequest : IRequest<object>
    {
        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
        public int IssueTypeId { get; set; }
    }
}
