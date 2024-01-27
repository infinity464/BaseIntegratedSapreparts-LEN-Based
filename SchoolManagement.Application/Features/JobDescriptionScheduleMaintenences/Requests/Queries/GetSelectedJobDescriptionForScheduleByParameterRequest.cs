using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Queries
{
    public class GetSelectedJobDescriptionForScheduleByParameterRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int IssueRegisterId { get; set; }
        public int ScheduleMaintenenceTypeId { get; set; }
        public int FiscalYearId { get; set; }
    }
} 
 