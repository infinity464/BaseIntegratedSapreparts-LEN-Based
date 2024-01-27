using MediatR;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Commands
{
    public class DeleteMaintenanceAuthorityCommand : IRequest
    {
        public int MaintenanceAuthorityId { get; set; }
    }
} 
