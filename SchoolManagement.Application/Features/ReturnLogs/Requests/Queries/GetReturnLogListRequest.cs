using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ReturnLogs;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ReturnLogs.Requests.Queries
{
    public class GetReturnLogListRequest : IRequest<PagedResult<ReturnLogDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
