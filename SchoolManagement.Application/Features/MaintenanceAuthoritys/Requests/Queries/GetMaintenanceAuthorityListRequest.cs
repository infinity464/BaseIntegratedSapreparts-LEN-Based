using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.MaintenanceAuthority;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Queries
{
    public class GetMaintenanceAuthorityListRequest : IRequest<PagedResult<MaintenanceAuthorityDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
