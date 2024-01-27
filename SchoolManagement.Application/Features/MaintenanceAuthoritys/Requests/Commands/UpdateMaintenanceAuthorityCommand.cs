using MediatR;
using SchoolManagement.Application.DTOs.MaintenanceAuthority;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Commands
{
    public class UpdateMaintenanceAuthorityCommand : IRequest<Unit>
    { 
        public MaintenanceAuthorityDto MaintenanceAuthorityDto { get; set; }
    }
}
 