using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Queries
{
    public class GetSelectedEquipmentPriorityRequest : IRequest<List<SelectedModel>>
    {
    }
} 
