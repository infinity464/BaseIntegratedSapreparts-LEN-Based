using MediatR;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Commands
{
    public class DeletePreventiveMaintenanceCommand : IRequest
    {
        public int PreventiveMaintenanceId { get; set; }
    }
} 
