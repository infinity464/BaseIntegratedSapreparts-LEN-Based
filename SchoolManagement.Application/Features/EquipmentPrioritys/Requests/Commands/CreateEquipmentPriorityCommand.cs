using MediatR;
using SchoolManagement.Application.DTOs.EquipmentPrioritys;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Commands
{
    public class CreateEquipmentPriorityCommand : IRequest<BaseCommandResponse>
    {
        public CreateEquipmentPriorityDto EquipmentPriorityDto { get; set; }
    }
}
