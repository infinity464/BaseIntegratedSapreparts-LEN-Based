using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.SpareRequiredStatus;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Queries
{
    public class GetSpareRequiredStatusListRequest : IRequest<PagedResult<SpareRequiredStatusDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
