using MediatR;
using SchoolManagement.Application.DTOs.MaintenanceAuthority;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Queries
{
    public class GetMaintenanceAuthorityDetailRequest : IRequest<MaintenanceAuthorityDto>
    {
        public int MaintenanceAuthorityId { get; set; }
    }
}
