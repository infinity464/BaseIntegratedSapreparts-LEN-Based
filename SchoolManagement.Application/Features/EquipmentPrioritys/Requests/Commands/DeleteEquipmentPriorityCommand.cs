using MediatR;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Commands
{
    public class DeleteEquipmentPriorityCommand : IRequest
    {
        public int EquipmentPriorityId { get; set; }
    }
} 
