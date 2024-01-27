using MediatR;
using SchoolManagement.Application.DTOs.MaintenanceAuthority;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Commands
{
    public class CreateMaintenanceAuthorityCommand : IRequest<BaseCommandResponse>
    {
        public CreateMaintenanceAuthorityDto MaintenanceAuthorityDto { get; set; }
    }
}
