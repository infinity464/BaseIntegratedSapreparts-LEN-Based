using MediatR;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Queries
{
    public class GetScheduleMaintenenceTypeDetailRequest : IRequest<ScheduleMaintenenceTypeDto>
    {
        public int ScheduleMaintenenceTypeId { get; set; }
    }
}
