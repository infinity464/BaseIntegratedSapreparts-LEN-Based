using MediatR;
using SchoolManagement.Application.DTOs.EquipmentPrioritys;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Commands
{
    public class UpdateEquipmentPriorityCommand : IRequest<Unit>
    { 
        public EquipmentPriorityDto EquipmentPriorityDto { get; set; }
    }
}
 