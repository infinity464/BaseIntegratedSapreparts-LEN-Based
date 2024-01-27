using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.EquipmentPrioritys;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Queries
{
    public class GetEquipmentPriorityListRequest : IRequest<PagedResult<EquipmentPriorityDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
