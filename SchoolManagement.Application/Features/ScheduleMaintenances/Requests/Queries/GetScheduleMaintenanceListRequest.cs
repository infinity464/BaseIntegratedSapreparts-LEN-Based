using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ScheduleMaintenances;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Queries
{
    public class GetScheduleMaintenanceListRequest : IRequest<PagedResult<ScheduleMaintenanceDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int IssueRegisterId { get; set; }
    }
}
