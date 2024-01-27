using MediatR;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Queries
{
    public class GetScheduledMaintanenceByParametersSpRequest : IRequest<object>
    {
        public int UnitId { get; set; }
        public int DepartmentId { get; set; }
        public int IssueRegisterId { get; set; }
        public int ScheduleMaintenenceTypeId { get; set; }
        public int FiscalYearId { get; set; }        
    }
}
