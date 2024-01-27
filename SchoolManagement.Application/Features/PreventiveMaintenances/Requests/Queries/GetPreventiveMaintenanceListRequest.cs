using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetPreventiveMaintenanceListRequest : IRequest<PagedResult<PreventiveMaintenanceDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
