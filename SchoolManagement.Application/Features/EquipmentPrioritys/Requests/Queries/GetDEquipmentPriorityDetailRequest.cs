using MediatR;
using SchoolManagement.Application.DTOs.EquipmentPrioritys;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Queries
{
    public class GetEquipmentPriorityDetailRequest : IRequest<EquipmentPriorityDto>
    {
        public int EquipmentPriorityId { get; set; }
    }
}
