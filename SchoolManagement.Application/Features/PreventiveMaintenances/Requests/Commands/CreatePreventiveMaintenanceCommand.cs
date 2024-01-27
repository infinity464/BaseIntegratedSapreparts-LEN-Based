using MediatR;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Commands
{
    public class CreatePreventiveMaintenanceCommand : IRequest<BaseCommandResponse>
    {
        public CreatePreventiveMaintenanceDto PreventiveMaintenanceDto { get; set; }
    }
}
