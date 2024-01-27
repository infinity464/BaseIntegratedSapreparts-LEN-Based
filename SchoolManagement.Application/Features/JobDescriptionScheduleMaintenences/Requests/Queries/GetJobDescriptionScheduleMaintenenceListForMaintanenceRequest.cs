using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Queries
{
    public class GetJobDescriptionScheduleMaintenenceListForMaintanenceRequest : IRequest<PagedResult<JobDescriptionScheduleMaintenenceDto>>
    { 
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int IssueRegisterId { get; set; }
        public int ScheduleMaintenenceTypeId { get; set; }
        public QueryParams QueryParams { get; set; } 
    }
}
