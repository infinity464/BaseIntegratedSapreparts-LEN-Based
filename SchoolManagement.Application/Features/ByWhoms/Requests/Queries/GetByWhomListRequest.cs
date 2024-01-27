using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ByWhom;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ByWhoms.Requests.Queries
{
    public class GetByWhomListRequest : IRequest<PagedResult<ByWhomDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
