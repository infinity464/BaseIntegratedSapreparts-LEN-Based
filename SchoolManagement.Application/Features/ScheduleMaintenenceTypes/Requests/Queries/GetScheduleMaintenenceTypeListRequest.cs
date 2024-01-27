using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Queries
{
    public class GetScheduleMaintenenceTypeListRequest : IRequest<PagedResult<ScheduleMaintenenceTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
